using System.Numerics;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class HighlightDestinationColourFilter : ColorFilter
    {
        public override string Key { get; set; } = "HighlightDestinationColor";
        public override string Name { get; set; } = "高亮目标栏位颜色";

        public override string HelpText { get; set; } =
            "设置目标栏位中与源栏位匹配的物品颜色（假设高亮目标重复项已打开）。";

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
            return configuration.HighlightColor;
        }

        public override bool HasValueSet(FilterConfiguration configuration)
        {
            return configuration.HighlightColor != null;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, Vector4? newValue)
        {
            configuration.HighlightColor = newValue;
        }
    }
}