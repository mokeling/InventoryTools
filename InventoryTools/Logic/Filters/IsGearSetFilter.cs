using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class IsGearSetFilter : BooleanFilter
    {
        public override string Key { get; set; } = "IsGearSet";
        public override string Name { get; set; } = "是套装的一部分吗？";
        public override string HelpText { get; set; } = "该物品是套装的一部分吗？";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;
        public override FilterType AvailableIn { get; set; } = FilterType.SearchFilter | FilterType.SortingFilter;
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            var currentValue = CurrentValue(configuration);
            if (currentValue == null)
            {
                return null;
            }

            if (item.GearSets == null)
            {
                return !currentValue.Value;
            }
            switch (currentValue.Value)
            {
                case false:
                    return item.GearSets.Length == 0;
                case true:
                    return item.GearSets.Length != 0;
            }
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }
    }
}