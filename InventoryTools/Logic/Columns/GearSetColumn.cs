using System;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class GearSetColumn : TextColumn
    {
        public override string? CurrentValue(InventoryItem item)
        {
            if (item.GearSetNames == null)
            {
                return "";
            }
            return String.Join(", ", item.GearSetNames);
        }

        public override string? CurrentValue(ItemEx item)
        {
            return "";
        }

        public override string? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "套装";
        public override float Width { get; set; } = 100;
        public override string HelpText { get; set; } = "显示物品所属的套装。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}