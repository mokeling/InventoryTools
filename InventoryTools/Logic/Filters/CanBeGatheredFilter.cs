using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class CanBeGatheredFilter : BooleanFilter
    {
        public override string Key { get; set; } = "Gatherable";
        public override string Name { get; set; } = "可以采集吗？";
        public override string HelpText { get; set; } = "这个物品可以通过采集获得吗？";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Acquisition;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {

            return FilterItem(configuration, item.Item);
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            var canBeGathered = item.CanBeGathered || item.ObtainedFishing;
            return currentValue == null || currentValue.Value && canBeGathered || !currentValue.Value && !canBeGathered;
        }
    }
}