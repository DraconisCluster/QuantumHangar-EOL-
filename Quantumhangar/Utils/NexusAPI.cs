using System.Collections.Generic;
using VRage.Game;
using VRageMath;

namespace QuantumHangar.Utils
{
    public class NexusAPI
    {
        public ushort CrossServerModID;

        /*  For recieving custom messages you have to register a message handler with a different unique ID then what you use server to client. (It should be the same as this class)
         *
         *  NexusAPI(5432){
         *  CrossServerModID = 5432
         *  }
         *
         *
         *  Register this somewhere in your comms code. (This will only be raised when it recieves a message from another server)
         *  MyAPIGateway.Multiplayer.RegisterMessageHandler(5432, MessageHandler);
         */


        public NexusAPI(ushort SocketID)
        {
            CrossServerModID = SocketID;
        }

        public static bool IsRunningNexus()
        {
            return false;
        }

        public static bool IsPlayerOnline(long IdentityID)
        {
            return false;
        }

        private static List<object[]> GetSectorsObject()
        {
            var APISectors = new List<object[]>();
            return APISectors;
        }

        private static List<object[]> GetAllOnlinePlayersObject()
        {
            var OnlinePlayers = new List<object[]>();
            return OnlinePlayers;
        }

        private static List<object[]> GetAllServersObject()
        {
            var Servers = new List<object[]>();
            return Servers;
        }

        private static List<object[]> GetAllOnlineServersObject()
        {
            var Servers = new List<object[]>();
            return Servers;
        }

        private static object[] GetThisServerObject()
        {
            var OnlinePlayers = new object[6];
            return OnlinePlayers;
        }


        public static Server GetThisServer()
        {
            var obj = GetThisServerObject();
            return new Server((string)obj[0], (int)obj[1], (short)obj[2], (int)obj[3], (int)obj[4],
                (List<ulong>)obj[5]);
        }

        public static List<Sector> GetSectors()
        {
            var Objs = GetSectorsObject();

            var Sectors = new List<Sector>();
            foreach (var obj in Objs)
                Sectors.Add(new Sector((string)obj[0], (string)obj[1], (int)obj[2], (bool)obj[3], (Vector3D)obj[4],
                    (double)obj[5], (int)obj[6]));
            return Sectors;
        }


        public static int GetServerIDFromPosition(Vector3D Position)
        {
            return 0;
        }


        public static List<Player> GetAllOnlinePlayers()
        {
            var Objs = GetAllOnlinePlayersObject();

            var Players = new List<Player>();
            foreach (var obj in Objs) Players.Add(new Player((string)obj[0], (ulong)obj[1], (long)obj[2], (int)obj[3]));
            return Players;
        }


        public static List<Server> GetAllServers()
        {
            var Objs = GetAllServersObject();

            var Servers = new List<Server>();
            foreach (var obj in Objs) Servers.Add(new Server((string)obj[0], (int)obj[1], (int)obj[2], (string)obj[3]));
            return Servers;
        }

        public static List<Server> GetAllOnlineServers()
        {
            var Objs = GetAllOnlineServersObject();

            var Servers = new List<Server>();
            foreach (var obj in Objs)
                Servers.Add(new Server((string)obj[0], (int)obj[1], (int)obj[2], (float)obj[3], (int)obj[4],
                    (List<ulong>)obj[5]));
            return Servers;
        }


        public static bool IsServerOnline(int ServerID)
        {
            return false;
        }

        public static void BackupGrid(List<MyObjectBuilder_CubeGrid> GridObjectBuilders, long OnwerIdentity)
        {
        }

        public static void SendChatMessageToDiscord(ulong ChannelID, string Author, string Message)
        {
        }

        public static void SendEmbedMessageToDiscord(ulong ChannelID, string EmbedTitle, string EmbedMsg,
            string EmbedFooter, string EmbedColor = null)
        {
        }

        public void SendMessageToServer(int ServerID, byte[] Message)
        {
        }

        public void SendMessageToAllServers(byte[] Message)
        {
        }


        public class Sector
        {
            public readonly Vector3D Center;

            public readonly string IPAddress;

            public readonly bool IsGeneralSpace;
            public readonly string Name;

            public readonly int Port;

            public readonly double Radius;

            public readonly int ServerID;

            public Sector(string Name, string IPAddress, int Port, bool IsGeneralSpace, Vector3D Center, double Radius,
                int ServerID)
            {
                this.Name = Name;
                this.IPAddress = IPAddress;
                this.Port = Port;
                this.IsGeneralSpace = IsGeneralSpace;
                this.Center = Center;
                this.Radius = Radius;
                this.ServerID = ServerID;
            }
        }

        public class Player
        {
            public readonly long IdentityID;

            public readonly int OnServer;

            public readonly string PlayerName;

            public readonly ulong SteamID;

            public Player(string PlayerName, ulong SteamID, long IdentityID, int OnServer)
            {
                this.PlayerName = PlayerName;
                this.SteamID = SteamID;
                this.IdentityID = IdentityID;
                this.OnServer = OnServer;
            }
        }

        public class Server
        {
            public readonly int MaxPlayers;
            public readonly string Name;
            public readonly List<ulong> ReservedPlayers;
            public readonly int ServerID;
            public readonly string ServerIP;
            public readonly float ServerSS;
            public readonly int ServerType;
            public readonly int TotalGrids;

            /*  Possible Server Types
             *
             *  0 - SyncedSectored
             *  1 - SyncedNon-Sectored
             *  2 - Non-Synced & Non-Sectored
             *
             */


            public Server(string Name, int ServerID, int ServerType, string IP)
            {
                this.Name = Name;
                this.ServerID = ServerID;
                this.ServerType = ServerType;
                ServerIP = IP;
            }


            //Online Server
            public Server(string Name, int ServerID, int MaxPlayers, float SimSpeed, int TotalGrids,
                List<ulong> ReservedPlayers)
            {
                this.Name = Name;
                this.ServerID = ServerID;
                this.MaxPlayers = MaxPlayers;
                ServerSS = SimSpeed;
                this.TotalGrids = TotalGrids;
                this.ReservedPlayers = ReservedPlayers;
            }
        }
    }
}