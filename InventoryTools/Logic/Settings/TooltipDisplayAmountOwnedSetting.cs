using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class TooltipDisplayAmountOwnedSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipDisplayAmountOwned;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipDisplayAmountOwned = newValue;
        }

        public override string Key { get; set; } = "TooltipDisplayOwned";
        public override string Name { get; set; } = "2）显示拥有量？";

        public override string HelpText { get; set; } =
            "当悬停物品时，帮助提示是否应包含有关物品所在位置的信息。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Tooltips;
    }
}