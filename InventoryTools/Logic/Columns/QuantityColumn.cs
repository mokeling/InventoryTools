using System.Linq;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class QuantityColumn : IntegerColumn
    {

        public override int? CurrentValue(InventoryItem item)
        {
            return (int)item.Quantity;
        }

        public override int? CurrentValue(ItemEx item)
        {
            return PluginService.InventoryMonitor.ItemCounts.Where(c => c.Key == item.RowId).Sum(c => c.Value);
        }

        public override int? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "数量";
        public override float Width { get; set; } = 70.0f;

        public override string HelpText { get; set; } =
            "物品的数量。 如果从游戏物品或制作过滤器查看，这将显示所有库存中可用的物品总数。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}