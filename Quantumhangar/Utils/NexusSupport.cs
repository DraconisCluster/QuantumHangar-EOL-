using System;
using System.Linq;
using NLog;
using ProtoBuf;
using QuantumHangar.HangarChecks;
using Sandbox.Engine.Multiplayer;
using Sandbox.ModAPI;
using VRage.Game;
using VRageMath;
using static QuantumHangar.Utils.CharacterUtilities;

namespace QuantumHangar.Utils
{
    public static class NexusSupport
    {
        private const ushort QuantumHangarNexusModId = 0x24fc;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static readonly GpsSender GpsSender = new GpsSender();
        private static int _thisServerId = -1;
        private static bool _requireTransfer = true;

        public static NexusAPI Api { get; } = new NexusAPI(QuantumHangarNexusModId);
        public static bool RunningNexus { get; private set; }

        public static void Init()
        {
            // Only initialize if Nexus is running
            if (!NexusAPI.IsRunningNexus())
            {
                Log.Info("QuantumHangar -> Nexus not detected");
                RunningNexus = false;
                return;
            }

            try
            {
                _thisServerId = NexusAPI.GetThisServer().ServerID;
                Log.Info("QuantumHangar -> Nexus integration has been initialized with serverID " + _thisServerId);

                MyAPIGateway.Multiplayer.RegisterSecureMessageHandler(QuantumHangarNexusModId, ReceivePacket);
                Log.Info("Running Nexus!");

                RunningNexus = true;
                var thisServer = NexusAPI.GetAllServers().FirstOrDefault(x => x.ServerID == _thisServerId);

                if (thisServer != null && thisServer.ServerType >= 1)
                {
                    _requireTransfer = true;
                    Log.Info("QuantumHangar -> This server is Non-Sectored!");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to initialize Nexus integration");
                RunningNexus = false;
            }
        }

        public static void Dispose()
        {
            //MyAPIGateway.Multiplayer.UnregisterSecureMessageHandler(QuantumHangarNexusModId, ReceivePacket);
        }

        // RelayLoadIfNecessary relays the load grid command to another server if necessary.
        //
        // The conditions are:
        // - this server runs with the Nexus plugin and connected to a controller
        // - the spawn position belongs to another server linked with Nexus
        //
        // Relaying the command allows the load to run locally on the target server, and running
        // all the checks and extra work (e.g. digging voxels) that would otherwise not happen,
        // as the grid would simply be transferred to the other sector after being loaded.
        //
        // Returns true if the load grid command was relayed and there is nothing else to do,
        // false otherwise, meaning the load must happen locally.
        //
        // If the target server is offline, it will refuse to load the grid and the player
        // must try again later when the target server is online.
        public static bool RelayLoadIfNecessary(Vector3D spawnPos, int id, bool loadNearPlayer, Chat chat,
            ulong steamId, long identityId, Vector3D playerPosition)
        {
            //Don't continue if we aren't running nexus, or we don't require transfer due to non-Sectored instances
            if (!RunningNexus || !_requireTransfer)
                return false;

            var target = NexusAPI.GetServerIDFromPosition(spawnPos);
            if (target == _thisServerId)
                return false;

            if (!NexusAPI.IsServerOnline(target))
            {
                chat?.Respond(
                    "Sorry, this grid belongs to another server that is currently offline. Please try again later.");
                return true;
            }

            chat?.Respond("Sending hangar load command to the corresponding server, please wait...");
            var msg = new NexusHangarMessage
            {
                Type = NexusHangarMessageType.LoadGrid,
                SteamId = steamId,
                LoadGridId = id,
                LoadNearPlayer = loadNearPlayer,
                IdentityId = identityId,
                PlayerPosition = playerPosition,
                ServerId = _thisServerId
            };

            Api.SendMessageToServer(target, MyAPIGateway.Utilities.SerializeToBinary<NexusHangarMessage>(msg));
            return true;
        }

        private static void ReceivePacket(ushort handlerId, byte[] data, ulong steamId, bool fromServer)
        {
            // Only consider trusted server messages, i.e. from Nexus itself, not untrusted player messages.
            if (!fromServer) return;

            NexusHangarMessage msg;
            try
            {
                msg = MyAPIGateway.Utilities.SerializeFromBinary<NexusHangarMessage>(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Invalid Nexus cross-server message for Quantum Hangar");
                return;
            }

            switch (msg.Type)
            {
                case NexusHangarMessageType.Chat:
                    var chat = new ScriptedChatMsg()
                    {
                        Author = msg.Sender,
                        Text = msg.Response,
                        Font = MyFontEnum.White,
                        Color = msg.Color,
                        Target = msg.ChatIdentityId
                    };
                    MyMultiplayerBase.SendScriptedChatMessage(ref chat);
                    return;

                case NexusHangarMessageType.SendGps:
                    GpsSender.SendGps(msg.Position, msg.Name, msg.EntityId, msg.Time, msg.Color, msg.desc);
                    return;

                case NexusHangarMessageType.LoadGrid:
                    var chatOverNexus = new Chat((text, color, sender) =>
                    {
                        var m = new NexusHangarMessage
                        {
                            Type = NexusHangarMessageType.Chat,
                            IdentityId = msg.IdentityId,
                            Response = text,
                            Color = color,
                            Sender = sender
                        };
                        Api.SendMessageToServer(msg.ServerId,
                            MyAPIGateway.Utilities.SerializeToBinary<NexusHangarMessage>(m));
                    });

                    var gpsOverNexus = new GpsSender((position, name, entityId, time, color, desc) =>
                    {
                        var m = new NexusHangarMessage
                        {
                            Type = NexusHangarMessageType.SendGps,
                            Name = name,
                            Position = position,
                            EntityId = entityId,
                            Time = time,
                            Color = color,
                            desc = desc
                        };
                        Api.SendMessageToServer(msg.ServerId,
                            MyAPIGateway.Utilities.SerializeToBinary<NexusHangarMessage>(m));
                    });

                    var user = new PlayerChecks(chatOverNexus, gpsOverNexus, msg.SteamId, msg.IdentityId,
                        msg.PlayerPosition);


                    user.LoadGrid(msg.LoadGridId.ToString(), msg.LoadNearPlayer);
                    return;
            }

            Log.Error("Invalid Nexus cross-server message for Quantum Hangar (unrecognized type: " + msg.Type + ")");
        }
    }

    public enum NexusHangarMessageType
    {
        Unset,
        LoadGrid,
        Chat,
        SendGps
    }

    [ProtoContract]
    public class NexusHangarMessage
    {
        // Relay chat response back: Type == Chat
        [ProtoMember(8)] public long ChatIdentityId;
        [ProtoMember(10)] public Color Color;
        [ProtoMember(16)] public string desc;
        [ProtoMember(14)] public long EntityId;

        // Request to load grid: Type == LoadGrid
        [ProtoMember(2)] public long IdentityId;
        [ProtoMember(3)] public int LoadGridId;
        [ProtoMember(4)] public bool LoadNearPlayer;

        // Relay send GPS back: Type == SendGPS
        [ProtoMember(12)] public string Name;
        [ProtoMember(6)] public Vector3D PlayerPosition;
        [ProtoMember(13)] public Vector3D Position;
        [ProtoMember(9)] public string Response;
        [ProtoMember(11)] public string Sender;
        [ProtoMember(7)] public int ServerId;
        [ProtoMember(5)] public ulong SteamId;
        [ProtoMember(15)] public int Time;
        [ProtoMember(1)] public NexusHangarMessageType Type;
    }
}