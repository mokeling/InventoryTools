using System.Linq;
using System.Numerics;
using CriticalCommonLib.Services;
using ImGuiNET;
using InventoryTools.Logic;

namespace InventoryTools.Sections
{
    public class CraftFiltersPage : IConfigPage
    {
        public string Name { get; } = "制作清单";
        public void Draw()
        {
            var filterConfigurations = PluginService.FilterService.FiltersList.Where(c => c.FilterType == FilterType.CraftFilter && !c.CraftListDefault).ToList();
            if (ImGui.CollapsingHeader("过滤器", ImGuiTreeNodeFlags.DefaultOpen))
            {
                ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(5, 5) * ImGui.GetIO().FontGlobalScale);
                if (ImGui.BeginTable("FilterConfigTable", 3, ImGuiTableFlags.Borders |
     //                                                        ImGuiTableFlags.Resizable |
                                                             ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("名称");
                    ImGui.TableSetupColumn("顺序");
                    ImGui.TableSetupColumn("");
                    ImGui.TableHeadersRow();
                    if (filterConfigurations.Count == 0)
                    {
                        ImGui.TableNextRow();
                        ImGui.TableNextColumn();
                        ImGui.Text("没有可用清单。");
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
                if (ImGui.Button("创建一个新的制作清单"))
                {
                    PluginService.FilterService.AddNewCraftFilter();
                }

                ImGui.SameLine();
                UiHelpers.HelpMarker(
                    "这将创建一个新清单，可以从制作窗口访问，向您显示所需材料的明细。");
            }

        }
    }
}