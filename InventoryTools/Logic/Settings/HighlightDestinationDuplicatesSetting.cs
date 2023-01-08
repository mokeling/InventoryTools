using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class HighlightDestinationSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.HighlightDestination;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.HighlightDestination = newValue;
        }

        public override string Key { get; set; } = "HighlightDestination";
        public override string Name { get; set; } = "2）目标栏位高亮?";

        public override string HelpText { get; set; } =
            "物品的目标栏位是否应该突出显示？ 这可以在过滤器配置中被覆盖。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.DestinationHighlighting;
    }
}