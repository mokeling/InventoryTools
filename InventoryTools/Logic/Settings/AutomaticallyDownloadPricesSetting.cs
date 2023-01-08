using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class AutomaticallyDownloadPricesSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.AutomaticallyDownloadMarketPrices;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.AutomaticallyDownloadMarketPrices = newValue;
        }

        public override string Key { get; set; } = "AutomaticallyDownloadPrices";
        public override string Name { get; set; } = "1）自动下载价格？";
        public override string HelpText { get; set; } = "在过滤器的物品列表中查看价格数据时是否应该自动下载？";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.MarketBoard;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Market;
    }
}