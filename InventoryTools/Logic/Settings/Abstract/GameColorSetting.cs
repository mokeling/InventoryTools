using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CriticalCommonLib;
using CriticalCommonLib.Comparer;
using Dalamud.Interface.Colors;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace InventoryTools.Logic.Settings.Abstract
{
    public abstract class GameColorSetting : Setting<uint?>
    {
        private readonly Dictionary<uint, UIColor> uiColors;

        public GameColorSetting()
        {
            var list = new List<UIColor>(Service.ExcelCache.GetUIColorSheet().Distinct(new UIColorComparer()));
            list.Sort((a, b) =>
            {
                var colorA = Utils.ConvertUIColorToColor(a);
                var colorB = Utils.ConvertUIColorToColor(b);
                ImGui.ColorConvertRGBtoHSV(colorA.X, colorA.Y, colorA.Z, out var aH, out var aS, out var aV);
                ImGui.ColorConvertRGBtoHSV(colorB.X, colorB.Y, colorB.Z, out var bH, out var bS, out var bV);

                var hue = aH.CompareTo(bH);
                if (hue != 0)
                {
                    return hue;
                }

                var saturation = aS.CompareTo(bS);
                if (saturation != 0)
                {
                    return saturation;
                }

                var value = aV.CompareTo(bV);
                return value != 0 ? value : 0;
            });
            uiColors = list.ToDictionary(c => c.RowId, c => c);
        }

        public override void Draw(InventoryToolsConfiguration configuration)
        {
            var value = CurrentValue(configuration);
            ImGui.SetNextItemWidth(LabelSize);
            if (HasValueSet(configuration))
            {
                ImGui.PushStyleColor(ImGuiCol.Text,ImGuiColors.HealerGreen);
                ImGui.LabelText("##" + Key + "Label", Name + ":");
                ImGui.PopStyleColor();
            }
            else
            {
                ImGui.LabelText("##" + Key + "Label", Name + ":");
            }
            ImGui.SameLine();
            var currentColour = new Vector4(255, 255, 255, 255);
            if (value != null && uiColors.ContainsKey(value.Value))
            {
                currentColour = Utils.ConvertUIColorToColor(uiColors[value.Value]);
            }
            if (ImGui.ColorButton("##" + Key + "CurrentVal", currentColour))
            {
            }

            var index = 0;
            foreach(var uiColor in uiColors)
            {
                var z = uiColor.Value;
                if (z.UIForeground is 0 or 255)
                {
                    continue;
                }

                var color = Utils.ConvertUIColorToColor(z);
                var id = z.RowId.ToString();
                var imGuiColorEditFlags = ImGuiColorEditFlags.NoBorder;
                if (value == z.RowId)
                {
                    imGuiColorEditFlags = ImGuiColorEditFlags.None;
                }

                if (ImGui.ColorButton(id, color, imGuiColorEditFlags))
                {
                    UpdateFilterConfiguration(configuration, id == "" ? null : UInt32.Parse(id));
                }
                index++;
                if (index % 10 != 0)
                {
                    ImGui.SameLine();
                }

                
            }

            UiHelpers.HelpMarker(HelpText);
            if (HasValueSet(configuration))
            {
                ImGui.SameLine();
                if (ImGui.Button("重置##" + Key + "Reset"))
                {
                    Reset(configuration);
                }
            }

        }
    }
}