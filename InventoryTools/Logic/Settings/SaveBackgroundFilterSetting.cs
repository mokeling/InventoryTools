using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class SaveBackgroundFilterSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.SaveBackgroundFilter;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.SaveBackgroundFilter = newValue;
        }

        public override string Key { get; set; } = "SaveBackgroundFilter";
        public override string Name { get; set; } = "3）保存背景过滤器吗?";

        public override string HelpText { get; set; } =
            "退出游戏或禁用/重新启用插件时是否保存活动背景过滤器？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;

        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.FilterSettings;
    }
}