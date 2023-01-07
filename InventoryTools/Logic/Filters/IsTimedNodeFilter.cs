using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class IsTimedNodeFilter : BooleanFilter
    {
        public override string Key { get; set; } = "TimedNode";
        public override string Name { get; set; } = "限时？";
        public override string HelpText { get; set; } = "这个物品在限时可用/采集？";
        public override FilterType AvailableIn { get; set; }  = FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Gathering;

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {
            var currentValue = CurrentValue(configuration);
            if (currentValue == null) return true;
            
            if(currentValue.Value && item.IsItemAvailableAtTimedNode)
            {
                return true;
            }
                
            return !currentValue.Value && !item.IsItemAvailableAtTimedNode;

        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            if (currentValue == null) return true;
            
            if(currentValue.Value && item.IsCollectable)
            {
                return true;
            }
                
            return !currentValue.Value && !item.IsCollectable;
        }
    }
}