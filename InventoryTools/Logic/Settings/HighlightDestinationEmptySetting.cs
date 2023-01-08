using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class HighlightDestinationEmptySetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.HighlightDestinationEmpty;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.HighlightDestinationEmpty = newValue;
        }

        public override string Key { get; set; } = "HighlightEmptyDestination";
        public override string Name { get; set; } = "3）高亮空目标栏位？";

        public override string HelpText { get; set; } =
            "高亮目标栏位，应该突出显示空格还是仅突出显示目标栏位中已存在的物品？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.DestinationHighlighting;
    }
}