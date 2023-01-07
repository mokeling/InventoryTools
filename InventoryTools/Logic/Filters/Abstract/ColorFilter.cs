using System.Numerics;
using Dalamud.Interface.Colors;
using ImGuiNET;

namespace InventoryTools.Logic.Filters.Abstract
{
    public abstract class ColorFilter : Filter<Vector4?>
    {
        public override bool HasValueSet(FilterConfiguration configuration)
        {
            return CurrentValue(configuration) != null;
        }
        
        public override Vector4? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.GetColorFilter(Key);
        }

        public override void Draw(FilterConfiguration configuration)
        {
            var value = CurrentValue(configuration) ?? Vector4.Zero;
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
            if (HasValueSet(configuration) && ImGui.Button("清除颜色"))
            {
                UpdateFilterConfiguration(configuration, null);
            }
            if (HasValueSet(configuration) && value.W == 0)
            {
                ImGui.SameLine();
                ImGui.TextColored(ImGuiColors.DalamudRed, "Alpha通道当前设置为 0，这将是不可见的。");
            }
            ImGui.SameLine();
            UiHelpers.HelpMarker(HelpText);
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, Vector4? newValue)
        {
            if (newValue.HasValue)
            {
                configuration.UpdateColorFilter(Key, newValue.Value);
            }
            else
            {
                configuration.RemoveColorFilter(Key);
            }
        }
    }
}