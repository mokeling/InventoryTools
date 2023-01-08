using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class InvertDestinationHighlightingSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.InvertDestinationHighlighting;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.InvertDestinationHighlighting = newValue;
        }

        public override string Key { get; set; } = "InvertDestinationHighlighting";
        public override string Name { get; set; } = "4）反转目标栏位高亮？";

        public override string HelpText { get; set; } =
            "突出显示目标栏位时，物品的颜色应该反转吗？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.DestinationHighlighting;

    }
}