using System;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class MarketBoardSaleCountLimitSetting : IntegerSetting
    {
        public override int DefaultValue { get; set; } = 7;
        public override int CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.MarketSaleHistoryLimit;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, int newValue)
        {
            newValue = Math.Min(30, newValue);
            newValue = Math.Max(1, newValue);
            configuration.MarketSaleHistoryLimit = newValue;
        }

        public override string Key { get; set; } = "MBSaleCountLimit";
        public override string Name { get; set; } = "3）市场板销售历史天数";

        public override string HelpText { get; set; } =
            "在计算某项物品的总销售量时，应检查多少天前的销售数据以计算该数量。 如果您更改此项，现有数据将不会被擦除，您将需要手动请求刷新市场板价格或等待市场板自动刷新。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.MarketBoard;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Market;
    }
}