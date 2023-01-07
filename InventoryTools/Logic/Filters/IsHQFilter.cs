using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class IsHqFilter : BooleanFilter
    {
        public override string Key { get; set; } = "HQ";
        public override string Name { get; set; } = "是HQ吗？";
        public override string HelpText { get; set; } = "物品的品质是HQ吗？";
        public override FilterType AvailableIn { get; set; }  = FilterType.SearchFilter | FilterType.SortingFilter;
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {
            var currentValue = CurrentValue(configuration);
            if (currentValue == null) return true;
            
            if(currentValue.Value && item.IsHQ)
            {
                return true;
            }
                
            return !currentValue.Value && !item.IsHQ;

        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return true;
        }
    }
}