using System;
using System.Linq;
using System.Numerics;
using CriticalCommonLib.Services;
using ImGuiNET;
using InventoryTools.Logic;

namespace InventoryTools.Sections
{
    public class FiltersPage : IConfigPage
    {
        public string Name { get; } = "过滤器";
        public void Draw()
        {
            var filterConfigurations = PluginService.FilterService.FiltersList.Where(c => c.FilterType != FilterType.CraftFilter).ToList();
            if (ImGui.CollapsingHeader("过滤器", ImGuiTreeNodeFlags.DefaultOpen))
            {
                ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(5, 5) * ImGui.GetIO().FontGlobalScale);
                if (ImGui.BeginTable("FilterConfigTable", 4, ImGuiTableFlags.Borders |
                                                             ImGuiTableFlags.Resizable |
                                                             ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("名称");
                    ImGui.TableSetupColumn("类型");
                    ImGui.TableSetupColumn("顺序");
                    ImGui.TableSetupColumn("");
                    ImGui.TableHeadersRow();
                    if (filterConfigurations.Count == 0)
                    {
                        ImGui.TableNextRow();
                        ImGui.TableNextColumn();
                        ImGui.Text("没有可用过滤器。");
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                    }

                    for (var index = 0; index < filterConfigurations.Count; index++)
                    {
                        ImGui.TableNextRow();
                        var filterConfiguration = filterConfigurations[index];
                        ImGui.TableNextColumn();
                        if (filterConfiguration.Name != "")
                        {
                            ImGui.Text(filterConfiguration.Name);
                            ImGui.SameLine();
                        }

                        ImGui.TableNextColumn();
                        ImGui.Text(filterConfiguration.FormattedFilterType);

                        ImGui.TableNextColumn();
                        ImGui.Text((filterConfiguration.Order + 1).ToString());
                        ImGui.SameLine();
                        if (ImGui.SmallButton("↑##" + index))
                        {
                            PluginService.FilterService.MoveFilterUp(filterConfiguration);
                        }
                        ImGui.SameLine();
                        if (ImGui.SmallButton("↓##" + index))
                        {
                            PluginService.FilterService.MoveFilterDown(filterConfiguration);
                        }
                        
                        ImGui.TableNextColumn();
                        if (ImGui.SmallButton("导出配置##" + index))
                        {
                            var base64 = filterConfiguration.ExportBase64();
                            ImGui.SetClipboardText(base64);
                            ChatUtilities.PrintClipboardMessage("[导出] ", "过滤器配置");
                        }

                        ImGui.SameLine();
                        if (ImGui.SmallButton("移除##" + index))
                        {
                            ImGui.OpenPopup("删除吗？##" + index);
                        }

                        ImGui.SameLine();
                        if (ImGui.SmallButton("作为窗口打开##" + index))
                        {
                            PluginService.WindowService.OpenFilterWindow(filterConfiguration.Key);
                        }

                        if (ImGui.BeginPopupModal("删除吗？##" + index))
                        {
                            ImGui.Text(
                                "您确定删除这个过滤器吗？\n这个选择不可逆！\n\n");
                            ImGui.Separator();

                            if (ImGui.Button("确定", new Vector2(120, 0) * ImGui.GetIO().FontGlobalScale))
                            {
                                PluginService.FilterService.RemoveFilter(filterConfiguration);
                                ImGui.CloseCurrentPopup();
                            }

                            ImGui.SetItemDefaultFocus();
                            ImGui.SameLine();
                            if (ImGui.Button("取消", new Vector2(120, 0) * ImGui.GetIO().FontGlobalScale))
                            {
                                ImGui.CloseCurrentPopup();
                            }

                            ImGui.EndPopup();
                        }
                    }

                    ImGui.EndTable();
                }

                ImGui.PopStyleVar();
            }

            if (ImGui.CollapsingHeader("创建过滤器", ImGuiTreeNodeFlags.DefaultOpen))
            {
                if (ImGui.Button("添加搜索过滤器"))
                {
                    PluginService.FilterService.AddFilter(new FilterConfiguration("新的搜索过滤器",
                        Guid.NewGuid().ToString("N"), FilterType.SearchFilter));
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这将创建一个新的过滤器，让您可以在您的角色和雇员库存中搜索特定物品。");

                if (ImGui.Button("添加分类过滤器"))
                {
                    PluginService.FilterService.AddFilter(new FilterConfiguration("新的分类过滤器",
                        Guid.NewGuid().ToString("N"), FilterType.SortingFilter));
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这将创建一个新过滤器，让您可以在角色和雇员库存中搜索特定物品，然后决定应将它们移动到哪里。");


                if (ImGui.Button("添加游戏物品过滤器"))
                {
                    PluginService.FilterService.AddFilter(new FilterConfiguration("新的游戏物品过滤器",
                        Guid.NewGuid().ToString("N"), FilterType.GameItemFilter));
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这将创建一个过滤器，让您可以搜索游戏中的所有物品。");
            }

            if (ImGui.CollapsingHeader("Sample Filters", ImGuiTreeNodeFlags.DefaultOpen))
            {
                ImGui.Text("过滤器样本：");
                if (ImGui.Button("100gil 以下可购买的物品"))
                {
                    PluginService.PluginLogic.AddSampleFilter100Gil();
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这将添加一个过滤器，显示所有可以从商店购买价格在100gil 以下的物品。它将同时查看角色和雇员库存。");

                if (ImGui.Button("清理多余材料"))
                {
                    PluginService.PluginLogic.AddSampleFilterMaterials();
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这将添加一个过滤器，该过滤器将被设置为快速清理任何多余的材料。它将自动添加所有材料类别。在计算放置物品的位置时，它会尝试优先考虑现有的物品堆。");

                if (ImGui.Button("跨角色/雇员的重复物品"))
                {
                    PluginService.PluginLogic.AddSampleFilterDuplicatedItems();
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这将添加一个过滤器，该过滤器将提供出现在2组库存中的所有不同堆叠物品的列表。 您可以使用它来确保只有一个固雇员拥有特定类型的物品。");
                
                ImGui.Text("默认过滤器：");
                if (ImGui.Button("全部"))
                {
                    PluginService.PluginLogic.AddAllFilter();
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这将添加默认“全部”过滤器的新副本。");
                if (ImGui.Button("雇员"))
                {
                    PluginService.PluginLogic.AddRetainerFilter();
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这会添加默认“雇员”过滤器的新副本。");
                
                if (ImGui.Button("玩家"))
                {
                    PluginService.PluginLogic.AddPlayerFilter();
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这会添加默认“玩家”过滤器的新副本。");

                if (ImGui.Button("所有游戏物品"))
                {
                    PluginService.PluginLogic.AddAllGameItemsFilter();
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这会添加默认“所有游戏物品”过滤器的新副本。");
            }
        }
    }
}