using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class MarketRefreshTimeHoursSetting : IntegerSetting
    {
        public override int DefaultValue { get; set; } = 24;
        public override int CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.MarketRefreshTimeHours;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, int newValue)
        {
            configuration.MarketRefreshTimeHours = newValue;
        }

        public override string Key { get; set; } = "MarketRefreshTime";
        public override string Name { get; set; } = "2）保存市场价格X小时";
        public override string HelpText { get; set; } = "在从universalis刷新之前，我们应该将市场价格保存多长时间？";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.MarketBoard;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Market;

    }
}