using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class CanBeDesynthedFilter : BooleanFilter
    {
        public override string Key { get; set; } = "desynth";
        public override string Name { get; set; } = "是否可以分解";
        public override string HelpText { get; set; } = "这个物品可以分解吗？";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return FilterItem(configuration, item);
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            if (currentValue == null)
            {
                return null;
            }
            return currentValue.Value && item.CanBeDesynthed || !currentValue.Value && !item.CanBeDesynthed;
        }
    }
}