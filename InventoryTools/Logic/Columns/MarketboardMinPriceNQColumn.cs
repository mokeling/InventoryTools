using CriticalCommonLib.MarketBoard;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using Dalamud.Interface.Colors;
using ImGuiNET;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class MarketBoardMinPriceNQColumn : GilColumn
    {
        protected static readonly string LoadingString = "加载中...";
        protected static readonly string UntradableString = "不可交易";
        protected static readonly int Loading = -1;
        protected static readonly int Untradable = -2;

        public override void Draw(FilterConfiguration configuration, InventoryItem item, int rowIndex)
        {
            var result = DoDraw(CurrentValue(item), rowIndex, configuration);
            result?.HandleEvent(configuration,item);
        }
        public override void Draw(FilterConfiguration configuration, SortingResult item, int rowIndex)
        {
            var result = DoDraw(CurrentValue(item), rowIndex, configuration);
            result?.HandleEvent(configuration,item);
        }
        public override void Draw(FilterConfiguration configuration, ItemEx item, int rowIndex)
        {
            var result = DoDraw(CurrentValue((ItemEx)item), rowIndex, configuration);
            result?.HandleEvent(configuration,item);
        }

        public override IColumnEvent? DoDraw(int? currentValue, int rowIndex, FilterConfiguration filterConfiguration)
        {
            if (currentValue.HasValue && currentValue.Value == Loading)
            {
                ImGui.TableNextColumn();
                ImGui.TextColored(ImGuiColors.DalamudYellow, LoadingString);
            }
            else if (currentValue.HasValue && currentValue.Value == Untradable)
            {
                ImGui.TableNextColumn();
                ImGui.TextColored(ImGuiColors.DalamudRed, UntradableString);
            }
            else if(currentValue.HasValue)
            {
                base.DoDraw(currentValue, rowIndex, filterConfiguration);
                ImGui.SameLine();
                if (ImGui.SmallButton("刷新##" + rowIndex))
                {
                    return new RefreshPricingEvent();
                }
            }
            else
            {
                base.DoDraw(currentValue, rowIndex, filterConfiguration);
            }
            return null;
        }

        public override int? CurrentValue(InventoryItem item)
        {
            if (!item.CanBeTraded)
            {
                return Untradable;
            }

            var marketBoardData = PluginService.MarketCache.GetPricing(item.ItemId, false);
            if (marketBoardData != null)
            {
                var hq = marketBoardData.minPriceNQ;
                return (int)hq;
            }

            return Loading;
        }

        public override int? CurrentValue(ItemEx item)
        {
            if (!item.CanBeTraded)
            {
                return Untradable;
            }

            var marketBoardData = PluginService.MarketCache.GetPricing(item.RowId, false);
            if (marketBoardData != null)
            {
                var hq = marketBoardData.minPriceNQ;
                return (int)hq;
            }

            return Loading;
        }

        public override int? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "市场板NQ最低价格";
        public override string HelpText { get; set; } =
            "显示物品NQ的最低价格。此数据来自 universalis。";
        public override float Width { get; set; } = 250.0f;
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}