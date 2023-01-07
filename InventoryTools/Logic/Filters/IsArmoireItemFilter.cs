using CriticalCommonLib;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class IsArmoireItemFilter : BooleanFilter
    {
        public override string Key { get; set; } = "IsArmoire";
        public override string Name { get; set; } = "是收藏柜物品吗？";
        public override string HelpText { get; set; } = "只显示可以放入收藏柜的物品。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Searching;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;

        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            var currentValue = this.CurrentValue(configuration);
            return currentValue switch
            {
                null => true,
                true => Service.ExcelCache.IsArmoireItem(item.Item.RowId),
                _ => !Service.ExcelCache.IsArmoireItem(item.Item.RowId)
            };
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = this.CurrentValue(configuration);
            return currentValue switch
            {
                null => true,
                true => Service.ExcelCache.IsArmoireItem(item.RowId),
                _ => !Service.ExcelCache.IsArmoireItem(item.RowId)
            };
        }
    }
}