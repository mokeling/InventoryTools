using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class FilterORFilter : BooleanFilter
    {
        public override string Key { get; set; } = "ORFilter";
        public override string Name { get; set; } = "过滤物品时使用“或”。";

        public override string HelpText { get; set; } =
            "过滤物品时，每个过滤器将使用“与”缩小范围，不使用“与”，使用“或”。";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Advanced;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;

        public override bool? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.UseORFiltering;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
        {
            configuration.UseORFiltering = newValue;
        }

        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }
    }
}