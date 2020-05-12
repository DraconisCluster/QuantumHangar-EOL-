﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torch;

namespace QuantumHangar
{
    public class Settings : ViewModel
    {
        private bool _Enabled = false;
        public bool PluginEnabled { get => _Enabled; set => SetValue(ref _Enabled, value); }

        private string _FolderDirectory;
        public string FolderDirectory { get => _FolderDirectory; set => SetValue(ref _FolderDirectory, value); }

        private double _WaitTime = 60;
        public double WaitTime { get => _WaitTime; set => SetValue(ref _WaitTime, value); }

        private double _DistanceCheck = 30000;
        public double DistanceCheck { get => _DistanceCheck; set => SetValue(ref _DistanceCheck, value); }

        private int _ScripterHangarAmount = 6;
        public int ScripterHangarAmount { get => _ScripterHangarAmount; set => SetValue(ref _ScripterHangarAmount, value); }

        private int _NormalHangarAmount = 2;
        public int NormalHangarAmount { get => _NormalHangarAmount; set => SetValue(ref _NormalHangarAmount, value); }

        private bool _GridMarketEnabled = false;
        public bool GridMarketEnabled { get => _GridMarketEnabled; set => SetValue(ref _GridMarketEnabled, value); }

        private bool _EnableBlackListBlocks = false;
        public bool EnableBlackListBlocks { get => _EnableBlackListBlocks; set => SetValue(ref _EnableBlackListBlocks, value); }

        private bool _RequireCurrency = false;
        public bool RequireCurrency { get => _RequireCurrency; set => SetValue(ref _RequireCurrency, value); }


        private double _CustomLargeGridCurrency = 1;
        private double _CustomStaticGridCurrency = 1;
        private double _CustomSmallGridCurrency = 1;
        public double CustomLargeGridCurrency { get => _CustomLargeGridCurrency; set => SetValue(ref _CustomLargeGridCurrency, value); }
        public double CustomStaticGridCurrency { get => _CustomStaticGridCurrency; set => SetValue(ref _CustomStaticGridCurrency, value); }
        public double CustomSmallGridCurrency { get => _CustomSmallGridCurrency; set => SetValue(ref _CustomSmallGridCurrency, value); }



        private CostType _HangarSaveCostType = CostType.PerGrid;
        private bool _BlackListRadioButton = false;

        //Server BlockLimits (true)   Custom (false)
        public bool SBlockLimits { get => _BlackListRadioButton; set => SetValue(ref _BlackListRadioButton, value); }

        public CostType HangarSaveCostType { get => _HangarSaveCostType; set => SetValue(ref _HangarSaveCostType, value); }



        private bool _AutosellHangarGrids = false;
        public bool AutosellHangarGrids { get => _AutosellHangarGrids; set => SetValue(ref _AutosellHangarGrids, value); }


        //MarketValue Multipliers
        private double _StaticGridMarketMultiplier = 1;
        public double StaticGridMarketMultiplier { get => _StaticGridMarketMultiplier; set => SetValue(ref _StaticGridMarketMultiplier, value); }

        private double _LargeGridMarketMultiplier = 1;
        public double LargeGridMarketMultiplier { get => _LargeGridMarketMultiplier; set => SetValue(ref _LargeGridMarketMultiplier, value); }

        private double _SmallGridMarketMultiplier = 1;
        public double SmallGridMarketMultiplier { get => _SmallGridMarketMultiplier; set => SetValue(ref _SmallGridMarketMultiplier, value); }

        private double _AutoSellDiscountPricePercent = .75;
        public double AutoSellDiscountPricePercent { get => _AutoSellDiscountPricePercent; set => SetValue(ref _AutoSellDiscountPricePercent, value); }

        private int _SellAFKDayAmount = 30;
        public int SellAFKDayAmount { get => _SellAFKDayAmount; set => SetValue(ref _SellAFKDayAmount, value); }
        private byte[] _Workbook = null;
        public byte[] Workbook { get => _Workbook; set => SetValue(ref _Workbook, value); }

        private bool _AdvancedDebug = false;
        public bool AdvancedDebug { get => _AdvancedDebug; set => SetValue(ref _AdvancedDebug, value); }

        private bool _EnableSubGrids = false;
        public bool EnableSubGrids { get => _EnableSubGrids; set => SetValue(ref _EnableSubGrids, value); }

        private bool _LoadFromOriginalPos = false;
        public bool LoadFromOriginalPos { get => _LoadFromOriginalPos; set => SetValue(ref _LoadFromOriginalPos, value); }


        private bool _RequireRestockFee = false;
        public bool RequireRestockFee { get => _RequireRestockFee; set => SetValue(ref _RequireRestockFee, value); }

        private double _RestockAmount = 1000;
        public double RestockAmount { get => _RestockAmount; set => SetValue(ref _RestockAmount, value); }

        private int _MarketPort = 8910;
        public int MarketPort { get => _MarketPort; set => SetValue(ref _MarketPort, value); }

        private ObservableCollection<PublicOffers> _PublicOffers = new ObservableCollection<PublicOffers>();
        public ObservableCollection<PublicOffers> PublicOffers { get => _PublicOffers; set => SetValue(ref _PublicOffers, value); }



        private bool _CrossServerEcon = false;
        public bool CrossServerEcon { get => _CrossServerEcon; set => SetValue(ref _CrossServerEcon, value); }

        private bool _AutoHangarGrids = false;
        public bool AutoHangarGrids { get => _AutoHangarGrids; set => SetValue(ref _AutoHangarGrids, value); }

        private bool _AutoOrientateToSurface = false;
        public bool AutoOrientateToSurface { get => _AutoOrientateToSurface; set => SetValue(ref _AutoOrientateToSurface, value); }

        private bool _AutoDisconnectGearConnectors = false;
        public bool AutoDisconnectGearConnectors { get => _AutoDisconnectGearConnectors; set => SetValue(ref _AutoDisconnectGearConnectors, value); }

        private bool _DeleteRespawnPods = false;
        public bool DeleteRespawnPods { get => _DeleteRespawnPods; set => SetValue(ref _DeleteRespawnPods, value); }

        private int _AutoHangarDayAmount = 20;
        public int AutoHangarDayAmount { get => _AutoHangarDayAmount; set => SetValue(ref _AutoHangarDayAmount, value); }

        private bool _HangarGridsFallenInPlanet = false;
        public bool HangarGridsFallenInPlanet { get => _HangarGridsFallenInPlanet; set => SetValue(ref _HangarGridsFallenInPlanet, value); }

        private bool _KeepPlayersLargestGrid = false;
        public bool KeepPlayersLargestGrid { get => _KeepPlayersLargestGrid; set => SetValue(ref _KeepPlayersLargestGrid, value); }

        private bool _AutoHangarStaticGrids = true;
        public bool AutoHangarStaticGrids { get => _AutoHangarStaticGrids; set => SetValue(ref _AutoHangarStaticGrids, value); }

        private bool _AutoHangarLargeGrids = true;
        public bool AutoHangarLargeGrids { get => _AutoHangarLargeGrids; set => SetValue(ref _AutoHangarLargeGrids, value); }

        private bool _AutoHangarSmallGrids = true;
        public bool AutoHangarSmallGrids { get => _AutoHangarSmallGrids; set => SetValue(ref _AutoHangarSmallGrids, value); }


        private bool _OnLoadTransfer = false;
        public bool OnLoadTransfer { get => _OnLoadTransfer; set => SetValue(ref _OnLoadTransfer, value); }

        //ExtendedConfigs

        //Single slot
        private int _SingleMaxBlocks = 0;
        public int SingleMaxBlocks { get => _SingleMaxBlocks; set => SetValue(ref _SingleMaxBlocks, value); }

        private int _SingleMaxPCU = 0;
        public int SingleMaxPCU { get => _SingleMaxPCU; set => SetValue(ref _SingleMaxPCU, value); }

        private bool _AllowStaticGrids = true;
        public bool AllowStaticGrids { get => _AllowStaticGrids; set => SetValue(ref _AllowStaticGrids, value); }

        private int _SingleMaxStaticGrids = 0;
        public int SingleMaxStaticGrids { get => _SingleMaxStaticGrids; set => SetValue(ref _SingleMaxStaticGrids, value); }

        private bool _AllowLargeGrids = true;
        public bool AllowLargeGrids { get => _AllowLargeGrids; set => SetValue(ref _AllowLargeGrids, value); }

        private int _SingleMaxLargeGrids = 0;
        public int SingleMaxLargeGrids { get => _SingleMaxLargeGrids; set => SetValue(ref _SingleMaxLargeGrids, value); }

        private bool _AllowSmallGrids = true;
        public bool AllowSmallGrids { get => _AllowSmallGrids; set => SetValue(ref _AllowSmallGrids, value); }

        private int _SingleMaxSmallGrids = 0;
        public int SingleMaxSmallGrids { get => _SingleMaxSmallGrids; set => SetValue(ref _SingleMaxSmallGrids, value); }


        //Max Configs
        private int _TotalMaxBlocks = 0;
        public int TotalMaxBlocks { get => _TotalMaxBlocks; set => SetValue(ref _TotalMaxBlocks, value); }

        private int _TotalMaxPCU = 0;
        public int TotalMaxPCU { get => _TotalMaxPCU; set => SetValue(ref _TotalMaxPCU, value); }

        private int _TotalMaxStaticGrids = 0;
        public int TotalMaxStaticGrids { get => _TotalMaxStaticGrids; set => SetValue(ref _TotalMaxStaticGrids, value); }

        private int _TotalMaxLargeGrids = 0;
        public int TotalMaxLargeGrids { get => _TotalMaxLargeGrids; set => SetValue(ref _TotalMaxLargeGrids, value); }

        private int _TotalMaxSmallGrids = 0;
        public int TotalMaxSmallGrids { get => _TotalMaxSmallGrids; set => SetValue(ref _TotalMaxSmallGrids, value); }


        private bool _AllowInGravity = true;
        public bool AllowInGravity { get => _AllowInGravity; set => SetValue(ref _AllowInGravity, value); }


        private bool _HostServer = false;
        public bool HostServer { get => _HostServer; set => SetValue(ref _HostServer, value); }


    }
}
