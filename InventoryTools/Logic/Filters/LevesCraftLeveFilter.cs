using CriticalCommonLib;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class LeveIsCraftLeveFilter : BooleanFilter
    {
        public override string Key { get; set; } = "LeveIsCraftLeve";
        public override string Name { get; set; } = "是生产理符物品吗？";
        public override string HelpText { get; set; } = "这个物品是可制作并且可以上交给理符？";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Crafting;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            var currentValue = this.CurrentValue(configuration);
            return currentValue switch
            {
                null => true,
                true => Service.ExcelCache.IsItemCraftLeve(item.ItemId),
                _ => !Service.ExcelCache.IsItemCraftLeve(item.ItemId)
            };
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = this.CurrentValue(configuration);
            return currentValue switch
            {
                null => true,
                true => Service.ExcelCache.IsItemCraftLeve(item.RowId),
                _ => !Service.ExcelCache.IsItemCraftLeve(item.RowId)
            };
        }
    }
}