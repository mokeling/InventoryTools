using CriticalCommonLib;
using CriticalCommonLib.Crafting;
using CriticalCommonLib.MarketBoard;
using CriticalCommonLib.Services;
using CriticalCommonLib.Services.Ui;
using Dalamud.Interface.ImGuiFileDialog;
using InventoryTools.Logic;
using InventoryTools.Misc;

namespace InventoryTools
{
    public static class PluginService
    {
        private static OdrScanner OdrScanner { get; set; } = null!;
        public static InventoryMonitor InventoryMonitor { get; private set; } = null!;
        private static NetworkMonitor NetworkMonitor { get; set; } = null!;
        public static CharacterMonitor CharacterMonitor { get; private set; } = null!;
        public static PluginLogic PluginLogic { get; private set; } = null!;
        public static GameUiManager GameUi { get; private set; } = null!;
        public static TryOn TryOn { get; private set; } = null!;
        //public static PluginFont PluginFont { get; private set; } = null!;
        public static PluginCommands PluginCommands { get; private set; } = null!;
        public static PluginCommandManager<PluginCommands> CommandManager { get; private set; } = null!;
        public static FilterManager FilterManager { get; private set; } = null!;
        public static WotsitIpc WotsitIpc { get; private set; } = null!;
        public static FileDialogManager FileDialogManager { get; private set; } = null!;
        public static CraftMonitor CraftMonitor { get; private set; } = null!;
        public static FunTimeService FunTimeService { get; private set; } = null!;

        public static void Initialise()
        {
            Service.ExcelCache = new ExcelCache(Service.Data);
            ConfigurationManager.Load();
            Universalis.Initalise();
            GameInterface.Initialise(Service.Scanner);
            Cache.Initalise(Service.Interface.ConfigDirectory.FullName + "/universalis.json");
            NetworkMonitor = new NetworkMonitor();
            CharacterMonitor = new CharacterMonitor();
            OdrScanner = new OdrScanner( CharacterMonitor);
            GameUi = new GameUiManager();
            TryOn = new TryOn();
            CraftMonitor = new CraftMonitor(GameUi);
            InventoryMonitor = new InventoryMonitor(OdrScanner, CharacterMonitor, GameUi, CraftMonitor);
            FilterManager = new FilterManager();
            PluginLogic = new PluginLogic(  );
            WotsitIpc = new WotsitIpc(  );
            PluginCommands = new();
            CommandManager = new PluginCommandManager<PluginCommands>(PluginCommands);
            FileDialogManager = new FileDialogManager();
            FunTimeService = new FunTimeService();
        }

        public static void InitialiseTesting(CharacterMonitor characterMonitor, PluginLogic pluginLogic)
        {
            CharacterMonitor = characterMonitor;
            PluginLogic = pluginLogic;
        }

        public static void Dispose()
        {
            FunTimeService.Dispose();
            CommandManager.Dispose();
            WotsitIpc.Dispose();
            PluginLogic.Dispose();
            FilterManager.Dispose();
            InventoryMonitor.Dispose();
            CraftMonitor.Dispose();
            TryOn.Dispose();
            GameUi.Dispose();
            CharacterMonitor.Dispose();
            NetworkMonitor.Dispose();
            OdrScanner.Dispose();
            ConfigurationManager.Save();
            Service.ExcelCache.Destroy();
            Cache.Dispose();
            Universalis.Dispose();
            //PluginFont?.Dispose();
            GameInterface.Dispose();
            if (TetrisGame.HasInstance)
            {
                TetrisGame.Instance.Dispose();
            }
            Cache.SaveCache(true);
        }
    }
}