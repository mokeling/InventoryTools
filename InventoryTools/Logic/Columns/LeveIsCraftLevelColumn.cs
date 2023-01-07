using CriticalCommonLib;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class LeveIsCraftLevelColumn : CheckboxColumn
    {
        public override bool? CurrentValue(InventoryItem item)
        {
            return Service.ExcelCache.IsItemCraftLeve(item.ItemId);
        }

        public override bool? CurrentValue(ItemEx item)
        {
            return Service.ExcelCache.IsItemCraftLeve(item.RowId);
        }

        public override bool? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "理符：用于生产理符？";
        public override float Width { get; set; } = 100.0f;
        public override string HelpText { get; set; } = "这个物品用于生产理符吗？";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Boolean;
    }
}