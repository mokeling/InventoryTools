using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class RetainerMarketPriceColumn : GilColumn
    {
        public override int? CurrentValue(InventoryItem item)
        {
            return (int)item.RetainerMarketPrice;
        }

        public override int? CurrentValue(ItemEx item)
        {
            return null;
        }

        public override int? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "雇员单价";
        public override float Width { get; set; } = 100;

        public override string HelpText { get; set; } =
            "如果该物品在市场上出售，这就是它的标价。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}