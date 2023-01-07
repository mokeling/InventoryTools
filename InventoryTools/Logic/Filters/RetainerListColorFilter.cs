using System.Numerics;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class RetainerListColorFilter : ColorFilter
    {
        public override string Key { get; set; } = "RetainerColor";
        public override string Name { get; set; } = "雇员列表颜色";

        public override string HelpText { get; set; } =
            "在雇员列表中为这个特定过滤器设置雇员的颜色。";

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
            return configuration.RetainerListColor;
        }

        public override bool HasValueSet(FilterConfiguration configuration)
        {
            return configuration.RetainerListColor != null;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, Vector4? newValue)
        {
            configuration.RetainerListColor = newValue;
        }
    }
}