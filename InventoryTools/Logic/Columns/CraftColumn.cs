using CriticalCommonLib;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class CraftColumn : CheckboxColumn
    {
        public override bool? CurrentValue(InventoryItem item)
        {
            return Service.ExcelCache.CanCraftItem(item.ItemId);
        }

        public override bool? CurrentValue(ItemEx item)
        {
            return Service.ExcelCache.CanCraftItem(item.RowId);
        }

        public override bool? CurrentValue(SortingResult item)
        {
            return Service.ExcelCache.CanCraftItem(item.InventoryItem.ItemId);
        }
        

        public override string Name { get; set; } = "可制作";
        public override float Width { get; set; } = 125.0f;
        public override string HelpText { get; set; } = "这个物品可以制作吗？";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Boolean;
    }
}