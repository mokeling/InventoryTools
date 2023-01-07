using CriticalCommonLib.MarketBoard;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class MarketBoardSaleCountFilter : StringFilter
    {
        public override string Key { get; set; } = "MBSaleCount";
        public override string Name { get; set; } = "市场板前 " + ConfigurationManager.Config.MarketSaleHistoryLimit + " 天销售数量";

        public override string HelpText { get; set; } = "显示物品前 " +
                                                        ConfigurationManager.Config.MarketSaleHistoryLimit + " 天销售数量。";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Market;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return FilterItem(configuration, item.Item);
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            if (HasValueSet(configuration))
            {
                if (!item.CanBeTraded)
                {
                    return false;
                }
                var marketBoardData = PluginService.MarketCache.GetPricing(item.RowId, false);
                if (marketBoardData != null)
                {
                    return marketBoardData.sevenDaySellCount.PassesFilter(currentValue.ToLower());
                }

                return false;
            }

            return null;
        }
    }
}