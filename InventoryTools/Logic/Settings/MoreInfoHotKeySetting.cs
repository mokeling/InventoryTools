using Dalamud.Game.ClientState.Keys;
using InventoryTools.Logic.Settings.Abstract;
using OtterGui.Classes;

namespace InventoryTools.Logic.Settings
{
    public class MoreInfoHotKeySetting : HotKeySetting
    {
        public override ModifiableHotkey DefaultValue { get; set; } = new(VirtualKey.M, ModifierHotkey.Control); 
        
        public override ModifiableHotkey CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.MoreInformationHotKey ?? new ModifiableHotkey();
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, ModifiableHotkey newValue)
        {
            configuration.MoreInformationHotKey = newValue;
        }

        public override string Key { get; set; } = "MoreInformationHotKey";
        public override string Name { get; set; } = "更多信息热键";

        public override string HelpText { get; set; } =
            "悬停时打开物品更多信息窗口的热键。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Hotkeys;
    }
}