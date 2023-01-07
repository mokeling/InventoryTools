using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class DebugColumn : TextColumn
    {
        public override string? CurrentValue(InventoryItem item)
        {
            return CurrentValue(item.Item);
        }

        public override string? CurrentValue(ItemEx item)
        {
            return "物品搜索：" + item.ItemSearchCategory.Row + " - UI类型：" + item.ItemUICategory.Row + " - 分类类型：" + item.ItemSortCategory.Row + " - 装备类型：" + item.EquipSlotCategory.Row + " - 职业类型：" + item.ClassJobCategory.Row + " - 购买：" + item.PriceMid + " - 未知：" + item.Unknown19;
        }

        public override string? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "调试 - 一般信息";
        public override float Width { get; set; } = 200;
        public override string HelpText { get; set; } = "显示基本调试信息。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override bool IsDebug { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}