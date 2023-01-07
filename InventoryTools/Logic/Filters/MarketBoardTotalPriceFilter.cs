using CriticalCommonLib.MarketBoard;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class MarketBoardTotalPriceFilter : StringFilter
    {
        public override string Key { get; set; } = "MBTotalPrice";
        public override string Name { get; set; } = "市场板总价";
        public override string HelpText { get; set; } = "物品的市场总价（价格 * 数量）。为此，您需要启用自动价格，并注意在刷新库存之前，不会评估任何后台价格更新（这种情况经常发生）。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Market;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter;

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
                        price = marketBoardData.averagePriceHQ;
                    }
                    else
                    {
                        price = marketBoardData.averagePriceNQ;
                    }

                    price *= item.Quantity;
                    return price.PassesFilter(currentValue.ToLower());
                }

                return false;
            }

            return true;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return true;
        }
    }
}