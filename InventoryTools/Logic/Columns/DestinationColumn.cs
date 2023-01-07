using CriticalCommonLib.Models;
using CriticalCommonLib.Extensions;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class DestinationColumn : TextColumn
    {
        public override string? CurrentValue(InventoryItem item)
        {
            return null;
        }

        public override string? CurrentValue(ItemEx item)
        {
            return null;
        }

        public override string? CurrentValue(SortingResult item)
        {
            var destination = item.DestinationRetainerId.HasValue
                ? PluginService.CharacterMonitor.Characters[item.DestinationRetainerId.Value]?.FormattedName ?? ""
                : "未知";
            var destinationBag = item.DestinationBag?.ToInventoryCategory().FormattedName() ?? "";
            return destination + " - " + destinationBag;
        }

        public override string Name { get; set; } = "目标栏位";
        public override float Width { get; set; } = 100.0f;
        public override string HelpText { get; set; } = "显示应将物品移动到的位置。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
        public override FilterType AvailableIn => Logic.FilterType.SortingFilter | Logic.FilterType.CraftFilter;
    }
}