using CriticalCommonLib;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class IsCraftingItemFilter : BooleanFilter
    {
        public override string Key { get; set; } = "IsCrafting";
        public override string Name { get; set; } = "是制作物品吗？";
        public override string HelpText { get; set; } = "只显示与制作相关的物品。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Searching;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;

        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            var currentValue = this.CurrentValue(configuration);
            return currentValue switch
            {
                null => null,
                true => Service.ExcelCache.IsCraftItem(item.Item.RowId),
                _ =>  !Service.ExcelCache.IsCraftItem(item.Item.RowId)
            };
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = this.CurrentValue(configuration);
            
            return currentValue switch
            {
                null => null,
                true => Service.ExcelCache.IsCraftItem(item.RowId),
                _ => !Service.ExcelCache.IsCraftItem(item.RowId)
            };
        }
    }
}