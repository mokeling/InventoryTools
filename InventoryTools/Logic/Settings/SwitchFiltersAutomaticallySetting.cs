using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class SwitchFiltersAutomaticallySetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.SwitchFiltersAutomatically;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.SwitchFiltersAutomatically = newValue;
        }

        public override string Key { get; set; } = "SwitchFiltersAutomatically";
        public override string Name { get; set; } = "5）自动切换过滤器?";

        public override string HelpText { get; set; } =
            "在每个过滤器选项卡之间移动时，激活窗口过滤器是否应该自动更改？ 仅当已选择激活过滤器时，激活过滤器才会更改。";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.FilterSettings;

    }
}