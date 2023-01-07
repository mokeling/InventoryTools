using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class DuplicatesOnlyFilter : BooleanFilter
    {
        public override string Key { get; set; } = "DuplicatesOnly";
        public override string Name { get; set; } = "仅显示重复？";

        public override string HelpText { get; set; } =
            "过滤掉所有未同时出现在源栏位和目标栏位中的物品？";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Searching;
        public override FilterType AvailableIn { get; set; } = FilterType.SortingFilter | FilterType.SearchFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }

        public override bool? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.DuplicatesOnly;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
        {
            configuration.DuplicatesOnly = newValue;
        }
    }
}