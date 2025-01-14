using System.Linq;
using CriticalCommonLib.Services;
using Dalamud.Interface.Colors;
using ImGuiNET;
using InventoryTools.Logic;

namespace InventoryTools.Sections
{
    public class FilterPage : IConfigPage
    {
        public string Name
        {
            get
            {
                return FilterConfiguration.Name;
            }
        }
        public FilterConfiguration FilterConfiguration;

        public FilterPage(FilterConfiguration filterConfiguration)
        {
            FilterConfiguration = filterConfiguration;
        }

        public void Draw()
        {
            var filterConfiguration = FilterConfiguration;
            var filterName = filterConfiguration.Name;
            var labelName = "##" + filterConfiguration.Key;
            if (ImGui.CollapsingHeader("通用", ImGuiTreeNodeFlags.DefaultOpen))
            {
                ImGui.SetNextItemWidth(100);
                ImGui.LabelText(labelName + "FilterNameLabel", "名称：");
                ImGui.SameLine();
                ImGui.InputText(labelName + "FilterName", ref filterName, 100);
                if (filterName != filterConfiguration.Name)
                {
                    filterConfiguration.Name = filterName;
                }
                
                ImGui.NewLine();
                if (ImGui.Button("导出配置到剪切板"))
                {
                    var base64 = filterConfiguration.ExportBase64();
                    ImGui.SetClipboardText(base64);
                    ChatUtilities.PrintClipboardMessage("[导出] ", "过滤器配置");
                }

                var filterType = filterConfiguration.FormattedFilterType;
                ImGui.SetNextItemWidth(100);
                ImGui.LabelText(labelName + "FilterTypeLabel", "过滤器类型：");
                ImGui.SameLine();
                ImGui.TextDisabled(filterType);

                ImGui.SetNextItemWidth(150);
                ImGui.LabelText(labelName + "DisplayInTabs", "在标签列表中显示：");
                ImGui.SameLine();
                var displayInTabs = filterConfiguration.DisplayInTabs;
                if (ImGui.Checkbox(labelName + "DisplayInTabsCheckbox", ref displayInTabs))
                {
                    if (displayInTabs != filterConfiguration.DisplayInTabs)
                    {
                        filterConfiguration.DisplayInTabs = displayInTabs;
                    }
                }
            }

            if (ImGui.BeginTabBar("###FilterConfigTabs", ImGuiTabBarFlags.FittingPolicyScroll))
            {
                foreach (var group in PluginService.PluginLogic.GroupedFilters)
                {
                    var hasValuesSet = false;
                    foreach (var filter in group.Value)
                    {
                        if (filter.HasValueSet(filterConfiguration))
                        {
                            hasValuesSet = true;
                            break;
                        }
                    }

                    if (hasValuesSet)
                    {
                        ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.HealerGreen);
                    }

                    var hasValues = group.Value.Any(filter =>
                        filter.AvailableIn.HasFlag(FilterType.SearchFilter) &&
                        filterConfiguration.FilterType.HasFlag(
                            FilterType.SearchFilter)
                        ||
                        (filter.AvailableIn.HasFlag(FilterType.SortingFilter) &&
                         filterConfiguration.FilterType.HasFlag(FilterType
                             .SortingFilter))
                        ||
                        (filter.AvailableIn.HasFlag(FilterType.CraftFilter) &&
                         filterConfiguration.FilterType.HasFlag(FilterType
                             .CraftFilter))
                        ||
                        (filter.AvailableIn.HasFlag(FilterType.GameItemFilter) &&
                         filterConfiguration.FilterType.HasFlag(FilterType
                             .GameItemFilter)));
                    if (hasValues && ImGui.BeginTabItem(group.Key.ToString()))
                    {
                        ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.DalamudWhite);
                        foreach (var filter in group.Value)
                        {
                            if ((filter.AvailableIn.HasFlag(FilterType.SearchFilter) &&
                                 filterConfiguration.FilterType.HasFlag(FilterType.SearchFilter)
                                 ||
                                 (filter.AvailableIn.HasFlag(FilterType.SortingFilter) &&
                                  filterConfiguration.FilterType.HasFlag(FilterType.SortingFilter))
                                 ||
                                 (filter.AvailableIn.HasFlag(FilterType.CraftFilter) &&
                                  filterConfiguration.FilterType.HasFlag(FilterType.CraftFilter))
                                 ||
                                 (filter.AvailableIn.HasFlag(FilterType.GameItemFilter) &&
                                  filterConfiguration.FilterType.HasFlag(FilterType.GameItemFilter))
                                ))
                            {
                                filter.Draw(filterConfiguration);
                            }
                        }
                        ImGui.PopStyleColor();
                        ImGui.EndTabItem();
                    }

                    if (hasValuesSet)
                    {
                        ImGui.PopStyleColor();
                    }
                }

                ImGui.EndTabBar();
            }
        }
    }
}