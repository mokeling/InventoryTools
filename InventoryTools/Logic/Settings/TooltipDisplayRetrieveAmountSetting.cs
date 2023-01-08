using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings;

public class TooltipDisplayRetrieveAmountSetting : BooleanSetting
{
    public override bool DefaultValue { get; set; }
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipDisplayRetrieveAmount;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.TooltipDisplayRetrieveAmount = newValue;
    }

    public override string Key { get; set; } = "DisplayRetrievalAmount";
    public override string Name { get; set; } = "5）显示取回数量？";

    public override string HelpText { get; set; } =
        "需要取回的数量是否应该显示在帮助提示中？";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Tooltips;
}