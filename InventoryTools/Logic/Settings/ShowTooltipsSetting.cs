using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class ShowTooltipsSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.DisplayTooltip;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.DisplayTooltip = newValue;
        }

        public override string Key { get; set; } = "ShowTooltips";
        public override string Name { get; set; } = "7）调整物品帮助提示？";

        public override string HelpText { get; set; } =
            "悬停物品时，会显示有关该物品的其他信息，包括它在库存中的位置和市场价格（如果有）。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Tooltips;

    }
}