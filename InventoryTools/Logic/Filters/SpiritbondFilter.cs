using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class SpiritBondFilter : StringFilter
    {
        public override string Key { get; set; } = "SB";
        public override string Name { get; set; } = "精炼度";
        public override string HelpText { get; set; } = "物品的精炼度。";
        public override FilterType AvailableIn { get; set; }  = FilterType.SearchFilter | FilterType.SortingFilter;
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {
            var currentValue = CurrentValue(configuration);
            if (!string.IsNullOrEmpty(currentValue))
            {
                if (item.IsCollectible)
                {
                    return false;
                }
                var spiritBond = item.Spiritbond/100;
                if (!spiritBond.PassesFilter(currentValue.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }
    }
}