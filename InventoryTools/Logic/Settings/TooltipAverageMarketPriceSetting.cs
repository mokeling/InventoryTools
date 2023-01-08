using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class TooltipAverageMarketPriceSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipDisplayMarketAveragePrice;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipDisplayMarketAveragePrice = newValue;
        }

        public override string Key { get; set; } = "TooltipDisplayMBAverage";
        public override string Name { get; set; } = "3）显示市场平均价格？";

        public override string HelpText { get; set; } =
            "悬停物品时，帮助提示是否应该包含 NQ/HQ 的市场平均价格？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Tooltips;

    }
}