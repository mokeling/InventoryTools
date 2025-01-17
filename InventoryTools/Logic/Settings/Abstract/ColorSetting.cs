using System.Numerics;
using Dalamud.Interface.Colors;
using ImGuiNET;

namespace InventoryTools.Logic.Settings.Abstract
{
    public abstract class ColorSetting : Setting<Vector4>
    {
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
            if (ImGui.ColorEdit4("##" + Key + "Color", ref value, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel))
            {
                UpdateFilterConfiguration(configuration, value);
            }
            ImGui.SameLine();
            if (HasValueSet(configuration) && value.W == 0)
            {
                ImGui.SameLine();
                ImGui.TextColored(ImGuiColors.DalamudRed, "alpha通道当前设置为 0，这将是不可见的。");
            }
            ImGui.SameLine();
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