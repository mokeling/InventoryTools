using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class AutoSaveSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.AutoSave;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.AutoSave = newValue;
            PluginService.PluginLogic.ClearAutoSave();
        }

        public override string Key { get; set; } = "AutoSave";
        public override string Name { get; set; } = "3）自动保存库存/配置？";

        public override string HelpText { get; set; } =
            "库存/配置是否应按定义的时间自动保存？ 虽然插件在游戏关闭和配置更改时会保存，但在崩溃的情况下不会，因此这可以缓解这种情况。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.AutoSave;
    }
}