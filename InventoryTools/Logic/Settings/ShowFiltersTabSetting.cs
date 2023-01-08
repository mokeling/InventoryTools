using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class ShowFiltersTabSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ShowFilterTab;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.ShowFilterTab = newValue;
        }

        public override string Key { get; set; } = "ShowFiltersTab";
        public override string Name { get; set; } = "4）显示过滤器标签?";

        public override string HelpText { get; set; } =
            "主窗口是否应显示名为“过滤器”的标签以在一个屏幕中列出所有可用的过滤器？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.FilterSettings;

    }
}