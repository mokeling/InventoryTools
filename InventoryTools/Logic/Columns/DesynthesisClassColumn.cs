using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns;

public class DesynthesisClassColumn : TextColumn
{
    public override string? CurrentValue(InventoryItem item)
    {
        return CurrentValue(item.Item);
    }

    public override string? CurrentValue(ItemEx item)
    {
        if (!item.CanBeDesynthed || item.ClassJobRepair.Row == 0)
        {
            return null;
        }

        return item.ClassJobRepair.Value?.Name.ToString().ToTitleCase() ?? "未知";
    }

    public override string? CurrentValue(SortingResult item)
    {
        return CurrentValue(item.InventoryItem);
    }

    public override string Name { get; set; } = "制作分解职业";
    public override float Width { get; set; } = 100;
    public override string HelpText { get; set; } = "这个物品需要什么职业来制作/分解？";
    public override string FilterText { get; set; } = "";
    public override bool HasFilter { get; set; } = true;
    public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
}