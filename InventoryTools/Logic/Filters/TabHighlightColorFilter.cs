using System.Numerics;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class TabHighlightColorFilter : ColorFilter
    {
        public override string Key { get; set; } = "TabHighlightColor";
        public override string Name { get; set; } = "高亮标签颜色";

        public override string HelpText { get; set; } =
            "为这个特定过滤器设置突出显示的标签（包含过滤的物品）的颜色。";

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

        public override Vector4? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.TabHighlightColor;
        }

        public override bool HasValueSet(FilterConfiguration configuration)
        {
            return configuration.TabHighlightColor != null;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, Vector4? newValue)
        {
            configuration.TabHighlightColor = newValue;
        }
    }
}