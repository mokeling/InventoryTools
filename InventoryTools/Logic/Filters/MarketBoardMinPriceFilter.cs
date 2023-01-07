using CriticalCommonLib.MarketBoard;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;

namespace InventoryTools.Logic.Filters
{
    public class MarketBoardMinPriceFilter : MarketBoardPriceFilter
    {
        public override string Key { get; set; } = "MBMinPrice";
        public override string Name { get; set; } = "市场板最低价格";
        public override string HelpText { get; set; } = "物品的市场板最低价格。为此，您需要启用自动价格，并注意在刷新库存之前，不会评估任何后台价格更新（这种情况经常发生）。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Market;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {
            var currentValue = CurrentValue(configuration);
            if (!string.IsNullOrEmpty(currentValue))
            {
                if (!item.CanBeTraded)
                {
                    return false;
                }
                var marketBoardData = PluginService.MarketCache.GetPricing(item.ItemId, false);
                if (marketBoardData != null)
                {
                    float price;
                    if (item.IsHQ)
                    {
                        price = marketBoardData.minPriceHQ;
                    }
                    else
                    {
                        price = marketBoardData.minPriceNQ;
                    }
                    return price.PassesFilter(currentValue.ToLower());
                }

                return false;
            }

            return true;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            if (!string.IsNullOrEmpty(currentValue))
            {
                if (!item.CanBeTraded)
                {
                    return false;
                }
                var marketBoardData = PluginService.MarketCache.GetPricing(item.RowId, false);
                if (marketBoardData != null)
                {
                    float price = marketBoardData.minPriceNQ;
                    return price.PassesFilter(currentValue.ToLower());
                }

                return false;
            }

            return true;
        }
    }
}