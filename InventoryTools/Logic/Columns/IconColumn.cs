using System.Numerics;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using ImGuiNET;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class IconColumn : GameIconColumn
    {
        public override (ushort, bool)? CurrentValue(InventoryItem item)
        {
            return (item.Icon, item.IsHQ);
        }

        public override (ushort, bool)? CurrentValue(ItemEx item)
        {
            return (item.Icon, false);
        }

        public override (ushort, bool)? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override IColumnEvent? DoDraw((ushort, bool)? currentValue, int rowIndex,
            FilterConfiguration filterConfiguration)
        {
            ImGui.TableNextColumn();
            if (currentValue != null)
            {
                var textureWrap = PluginService.PluginLogic.GetIcon(currentValue.Value.Item1, currentValue.Value.Item2);
                if (textureWrap != null)
                {
                    ImGui.PushID("icon" + rowIndex);
                    if (ImGui.ImageButton(textureWrap.ImGuiHandle, new Vector2(filterConfiguration.TableHeight, filterConfiguration.TableHeight) * ImGui.GetIO().FontGlobalScale,new Vector2(0,0), new Vector2(1,1), 2))
                    {
                        ImGui.PopID();
                        return new ItemIconPressedColumnEvent();
                    }
                    ImGui.PopID();
                }
                else
                {
                    ImGui.Button("", new Vector2(filterConfiguration.TableHeight, filterConfiguration.TableHeight) * ImGui.GetIO().FontGlobalScale);
                }
            }
            return null;
            
        }


        public override string Name { get; set; } = "图标";
        public override float Width { get; set; } = 40.0f;
        public override string HelpText { get; set; } = "显示物品的图标，按下它将打开物品的更多信息窗口。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = false;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}