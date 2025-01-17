using System;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class GearSetFilter : StringFilter
    {
        public override string Key { get; set; } = "GearSet";
        public override string Name { get; set; } = "套装职业";
        public override string HelpText { get; set; } = "按物品所在的套装过滤。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            var currentValue = CurrentValue(configuration);
            if (!string.IsNullOrEmpty(currentValue))
            {
                if (item.GearSetNames != null)
                {
                    var gearSetNames = String.Join(", ", item.GearSetNames);
                    if (!gearSetNames.ToLower().PassesFilter(currentValue.ToLower()))
                    {
                        return false;
                    }
                }
            }

            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }
    }
}