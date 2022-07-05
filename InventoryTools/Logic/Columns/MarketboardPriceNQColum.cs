using CriticalCommonLib.MarketBoard;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using Dalamud.Interface.Colors;
using ImGuiNET;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class MarketBoardPriceNQColumn : GilColumn
    {
        protected static readonly string LoadingString = "loading...";
        protected static readonly string UntradableString = "untradable";
        protected static readonly int Loading = -1;
        protected static readonly int Untradable = -2;

        public override void Draw(InventoryItem item, int rowIndex)
        {
            var result = DoDraw(CurrentValue(item), rowIndex);
            result?.HandleEvent(item);
        }
        public override void Draw(SortingResult item, int rowIndex)
        {
            var result = DoDraw(CurrentValue(item), rowIndex);
            result?.HandleEvent(item);
        }
        public override void Draw(ItemEx item, int rowIndex)
        {
            var result = DoDraw(CurrentValue((ItemEx)item), rowIndex);
            result?.HandleEvent(item);
        }

        public override IColumnEvent? DoDraw(int? currentValue, int rowIndex)
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
                base.DoDraw(currentValue, rowIndex);
                ImGui.SameLine();
                if (ImGui.SmallButton("R##" + rowIndex))
                {
                    return new RefreshPricingEvent();
                }
            }
            else
            {
                base.DoDraw(currentValue, rowIndex);
            }
            return null;
        }

        public override int? CurrentValue(InventoryItem item)
        {
            if (!item.CanBeTraded)
            {
                return Untradable;
            }

            var marketBoardData = Cache.GetPricing(item.ItemId, false);
            if (marketBoardData != null)
            {
                var nq = marketBoardData.averagePriceNQ;
                return (int)nq;
            }

            return Loading;
        }

        public override int? CurrentValue(ItemEx item)
        {
            if (!item.CanBeTraded)
            {
                return Untradable;
            }

            var marketBoardData = Cache.GetPricing(item.RowId, false);
            if (marketBoardData != null)
            {
                var nq = marketBoardData.averagePriceNQ;
                return (int)nq;
            }

            return Loading;
        }

        public override int? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "MB Average Price NQ";
        public override float Width { get; set; } = 250.0f;
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}