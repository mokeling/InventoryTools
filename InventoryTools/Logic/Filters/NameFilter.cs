using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class NameFilter : StringFilter
    {
        public override string Key { get; set; } = "Name";
        public override string Name { get; set; } = "名称";
        public override string HelpText { get; set; } = "按物品名称搜索。";
        public override FilterType AvailableIn { get; set; }  = FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {
            return FilterItem(configuration, item.Item);
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            if (!string.IsNullOrEmpty(currentValue))
            {
                if (!item.NameString.ToString().ToLower().PassesFilter(currentValue.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}