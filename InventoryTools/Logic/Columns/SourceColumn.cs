﻿using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class SourceColumn : TextColumn
    {
        public override string? CurrentValue(InventoryItem item)
        {
            return PluginService.CharacterMonitor.Characters.ContainsKey(item.RetainerId) ?  PluginService.CharacterMonitor.Characters[item.RetainerId].FormattedName : "Unknown (" + item.RetainerId + ")";
        }

        public override string? CurrentValue(ItemEx item)
        {
            return null;
        }

        public override string? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "来源";
        public override float Width { get; set; } = 100.0f;
        public override string HelpText { get; set; } = "显示物品所在的角色/雇员。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}