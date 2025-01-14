using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class TableCraftFreezeRowsFilter : IntegerFilter
    {
        public override string Key { get; set; } = "TableCraftFreezeRows";
        public override string Name { get; set; } = "b）冻结栏目";

        public override string HelpText { get; set; } =
            "从1开始冻结的栏目数（滚动时始终显示）。";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.CraftColumns;

        public override FilterType AvailableIn { get; set; } =
            FilterType.CraftFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, int? newValue)
        {
            configuration.FreezeCraftColumns = newValue;
        }

        public override int? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.FreezeCraftColumns;
        }
    }
}