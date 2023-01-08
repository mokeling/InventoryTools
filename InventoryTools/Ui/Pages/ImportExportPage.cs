using System.Numerics;
using CriticalCommonLib.Services;
using Dalamud.Interface.Colors;
using ImGuiNET;
using InventoryTools.Logic;

namespace InventoryTools.Sections
{
    public class ImportExportPage : IConfigPage
    {
        public string Name { get; } = "导入/导出";
        public void Draw()
        {
            ImGui.PushID("ImportSection");
            if (ImGui.CollapsingHeader("导出", ImGuiTreeNodeFlags.DefaultOpen))
            {
                var filterConfigurations = PluginService.FilterService.FiltersList;
                ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(5, 5) * ImGui.GetIO().FontGlobalScale);
                if (ImGui.BeginTable("FilterConfigTable", 3, ImGuiTableFlags.Borders |
                                                             ImGuiTableFlags.Resizable |
                                                             ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("名称");
                    ImGui.TableSetupColumn("类型");
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

                        /*if (PluginFont.AppIcons.HasValue && filterConfiguration.Icon != null)
                        {
                            ImGui.PushFont(PluginFont.AppIcons.Value);
                            ImGui.Text(filterConfiguration.Icon);
                            ImGui.PopFont();
                        }*/

                        ImGui.TableNextColumn();
                        ImGui.Text(filterConfiguration.FormattedFilterType);
                        ImGui.TableNextColumn();
                        if (ImGui.SmallButton("导出配置##" + index))
                        {
                            var base64 = filterConfiguration.ExportBase64();
                            ImGui.SetClipboardText(base64);
                            ChatUtilities.PrintClipboardMessage("[导出] ", "过滤器配置");
                        }
                    }

                    ImGui.EndTable();
                }

                ImGui.PopStyleVar();
            }

            if (ImGui.CollapsingHeader("导入", ImGuiTreeNodeFlags.DefaultOpen))
            {
                var importData = ImportData;
                if (ImGui.InputTextMultiline("在此处粘贴过滤器", ref importData, 5000, new Vector2(400, 200) * ImGui.GetIO().FontGlobalScale))
                {
                    ImportData = importData;
                    ImportFailed = false;
                }

                if (ImGui.Button("导入##ImportBtn"))
                {
                    if (ImportData == "")
                    {
                        ImportFailed = true;
                        FailedReason =
                            "在按导入之前，您必须粘贴通过导出功能生成的过滤器。";
                    }
                    else
                    {
                        if (FilterConfiguration.FromBase64(ImportData, out FilterConfiguration newFilter))
                        {
                            PluginService.PluginLogic.AddFilter(newFilter);
                        }
                        else
                        {
                            ImportFailed = true;
                            FailedReason =
                                "在导入字符串中检测到无效数据。 请确保此字符串有效。";
                        }
                    }
                }

                if (ImportFailed)
                {
                    ImGui.TextColored(ImGuiColors.DalamudRed, FailedReason);
                }
            }
            ImGui.PopID();                
        }

        public string FailedReason { get; set; } = "";

        public bool ImportFailed { get; set; } = false;

        public string ImportData { get; set; } = "";
    }
}