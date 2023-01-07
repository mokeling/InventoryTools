using CriticalCommonLib.MarketBoard;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;

namespace InventoryTools.Logic.Columns
{
    public class MarketBoardMinPriceColumn : MarketBoardPriceColumn
    {
        
        public override string HelpText { get; set; } =
            "显示物品NQ和HQ最低价格。此数据来自universalis。";
        
        public override (int,int)? CurrentValue(InventoryItem item)
        {
            if (!item.CanBeTraded)
            {
                return (Untradable, Untradable);
            }

            var marketBoardData = PluginService.MarketCache.GetPricing(item.ItemId, false);
            if (marketBoardData != null)
            {
                var nq = marketBoardData.minPriceNQ;
                var hq = marketBoardData.minPriceHQ;
                return ((int)nq, (int)hq);
            }

            return (Loading, Loading);
        }

        public override (int, int)? CurrentValue(ItemEx item)
        {
            if (!item.CanBeTraded)
            {
                return (Untradable, Untradable);
            }

            var marketBoardData = PluginService.MarketCache.GetPricing(item.RowId, false);
            if (marketBoardData != null)
            {
                var nq = marketBoardData.minPriceNQ;
                var hq = marketBoardData.minPriceHQ;
                return ((int)nq, (int)hq);
            }

            return (Loading, Loading);
        }

        public override (int,int)? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "市场板NQ/HQ最低价格";
    }
}