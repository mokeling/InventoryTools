using System;
using System.Collections.Generic;
using CriticalCommonLib.Extensions;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using Dalamud.Interface.Colors;
using ImGuiNET;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class DestinationsFilter : DisplayFilter
    {
        public override int Order { get; set; } = 2;
        public override string Key { get; set; } = "Destinations";
        public override string Name { get; set; } = "目标栏位";

        public override string HelpText { get; set; } =
            "这列出了所有适用于上面选择的目标栏位。";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Inventories;
        
        public override bool HasValueSet(FilterConfiguration configuration)
        {
            return false;
        }

        public override FilterType AvailableIn { get; set; } = FilterType.SortingFilter | FilterType.CraftFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }

        public override void Draw(FilterConfiguration configuration)
        {
            ImGui.NewLine();
            ImGui.Text("目标栏位信息：");
            ImGui.SameLine();
            UiHelpers.HelpMarker(HelpText);
            var allCharacters = PluginService.CharacterMonitor.Characters;
            List<string> destinations = new();
            foreach (var retainerCategories in configuration.DestinationRetainerCategories)
            {
                foreach (var retainerCategory in retainerCategories.Value)
                {
                    if (allCharacters.ContainsKey(retainerCategories.Key) &&
                        retainerCategories.Key.ToString().StartsWith("3"))
                    {
                        var formattedName = allCharacters[retainerCategories.Key].FormattedName + " - " +
                                            retainerCategory.FormattedName();
                        destinations.Add(formattedName);
                    }
                }
            }
            ImGui.SetNextItemWidth(LabelSize);
            if (destinations.Count != 0)
            {
                ImGui.PushStyleColor(ImGuiCol.Text,ImGuiColors.HealerGreen);
            }
            ImGui.LabelText("##" + Key + "RetainerLabel", "雇员" + ":");
            if (destinations.Count != 0)
            {
                ImGui.PopStyleColor();
            }
            ImGui.SameLine();
            ImGui.Text(String.Join(", ", destinations));
            destinations = new();
            foreach (var characterCategories in configuration.DestinationCharacterCategories)
            {
                foreach (var characterCategory in characterCategories.Value)
                {
                    if (allCharacters.ContainsKey(characterCategories.Key) &&
                        characterCategories.Key.ToString().StartsWith("1"))
                    {
                        var formattedName = allCharacters[characterCategories.Key].FormattedName + " - " +
                                            characterCategory.FormattedName();
                        destinations.Add(formattedName);
                    }
                }
            }
            ImGui.SetNextItemWidth(LabelSize);
            if (destinations.Count != 0)
            {
                ImGui.PushStyleColor(ImGuiCol.Text,ImGuiColors.HealerGreen);
            }
            ImGui.LabelText("##" + Key + "CharacterLabel", "角色" + ":");
            if (destinations.Count != 0)
            {
                ImGui.PopStyleColor();
            }
            ImGui.SameLine();
            ImGui.Text(String.Join(", ", destinations));
        }
    }
}