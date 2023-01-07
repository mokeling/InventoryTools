using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class InvertTabHighlightingFilter : BooleanFilter
    {
        public override string Key { get; set; } = "InvertTabHighlighting";
        public override string Name { get; set; } = "反转标签高亮？";
        public override string HelpText { get; set; } = "是否应该突出显示所有与过滤器不匹配的物品？如果设置为 N/A 将使用常规配置中的“反转高亮”设置。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Display;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        
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
            return configuration.InvertTabHighlighting;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
        {
            configuration.InvertTabHighlighting = newValue;
        }

        public override bool HasValueSet(FilterConfiguration configuration)
        {
            return configuration.InvertTabHighlighting != null;
        }
    }
}