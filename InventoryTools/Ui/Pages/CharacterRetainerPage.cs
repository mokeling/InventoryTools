using System.Linq;
using System.Numerics;
using CriticalCommonLib;
using ImGuiNET;
using InventoryTools.Logic;

namespace InventoryTools.Sections
{
    public class CharacterRetainerPage : IConfigPage
    {
        public string Name { get; } = "角色/雇员";
        
        public void Draw()
        {
            if (ImGui.CollapsingHeader("角色", ImGuiTreeNodeFlags.DefaultOpen))
            {
                ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(5, 5) * ImGui.GetIO().FontGlobalScale);
                if (ImGui.BeginTable("CharacterTable", 3, ImGuiTableFlags.Borders |
                                                          ImGuiTableFlags.Resizable |
                                                          ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("名称");
                    ImGui.TableSetupColumn("显示名称");
                    ImGui.TableSetupColumn("");
                    ImGui.TableHeadersRow();
                    var characters = PluginService.CharacterMonitor.GetPlayerCharacters();
                    if (characters.Length == 0)
                    {
                        ImGui.TableNextRow();
                        ImGui.Text("没有可用角色。");
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                    }

                    for (var index = 0; index < characters.Length; index++)
                    {
                        ImGui.TableNextRow();
                        var character = characters[index].Value;
                        ImGui.TableNextColumn();
                        if (character.Name != "")
                        {
                            ImGui.Text(character.Name);
                            ImGui.SameLine();
                        }

                        ImGui.TableNextColumn();
                        var value = character.AlternativeName ?? "";
                        if (ImGui.InputText("##"+index+"Input", ref value, 150))
                        {
                            if (value == "")
                            {
                                character.AlternativeName = null;
                                PluginService.CharacterMonitor.UpdateCharacter(character);
                            }
                            else
                            {
                                character.AlternativeName = value;
                                PluginService.CharacterMonitor.UpdateCharacter(character);
                            }
                        }
                        ImGui.TableNextColumn();
                        if (character.CharacterId != Service.ClientState.LocalContentId)
                        {
                            if (ImGui.SmallButton("移除##" + index))
                            {
                                PluginService.CharacterMonitor.RemoveCharacter(character.CharacterId);
                            }

                            ImGui.SameLine();
                        }

                        if (ImGui.SmallButton("清理所有背包##" + index))
                        {
                            ImGui.OpenPopup("您确定吗？##" + index);
                        }
                        if (ImGui.BeginPopupModal("您确定吗？##" + index))
                        {
                            ImGui.Text(
                                "您确定要清除该角色的所有库存吗？\n这个选择不可逆！\n\n");
                            ImGui.Separator();

                            if (ImGui.Button("确定", new Vector2(120, 0) * ImGui.GetIO().FontGlobalScale))
                            {
                                PluginService.InventoryMonitor.ClearCharacterInventories(character.CharacterId);
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
            if (ImGui.CollapsingHeader("雇员", ImGuiTreeNodeFlags.DefaultOpen))
            {
                ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(5, 5) * ImGui.GetIO().FontGlobalScale);
                if (ImGui.BeginTable("RetainerTable", 7, ImGuiTableFlags.Borders |
                                                         ImGuiTableFlags.Resizable |
                                                         ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("雇佣顺序");
                    ImGui.TableSetupColumn("名称");
                    ImGui.TableSetupColumn("金币");
                    ImGui.TableSetupColumn("等级");
                    ImGui.TableSetupColumn("所有者");
                    ImGui.TableSetupColumn("显示名称");
                    ImGui.TableSetupColumn("");
                    ImGui.TableHeadersRow();
                    var retainers = PluginService.CharacterMonitor.GetRetainerCharacters().OrderBy(c => c.Value.HireOrder).ToList();
                    if (retainers.Count == 0)
                    {
                        ImGui.TableNextRow();
                        ImGui.Text("没有可用雇员。");
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                        ImGui.TableNextColumn();
                    }

                    for (var index = 0; index < retainers.Count; index++)
                    {
                        ImGui.TableNextRow();
                        var character = retainers[index].Value;
                        
                        ImGui.TableNextColumn();
                        {
                            ImGui.Text((character.HireOrder + 1).ToString());
                            ImGui.SameLine();
                        }
                        
                        ImGui.TableNextColumn();
                        ImGui.Text(character.Name);
                        ImGui.SameLine();
                        
                        ImGui.TableNextColumn();
                        ImGui.Text(character.Gil.ToString());
                        ImGui.SameLine();
                        
                        ImGui.TableNextColumn();
                        ImGui.Text(character.Level.ToString());
                        ImGui.SameLine();
                        
                        ImGui.TableNextColumn();
                        var characterName = "未知";
                        if (PluginService.CharacterMonitor.Characters.ContainsKey(character.OwnerId))
                        {
                            var owner = PluginService.CharacterMonitor.Characters[character.OwnerId];
                            characterName = owner.FormattedName;
                        }

                        ImGui.Text(characterName);
                        
                        ImGui.TableNextColumn();
                        var value = character.AlternativeName ?? "";
                        if (ImGui.InputText("##"+index+"Input", ref value, 150))
                        {
                            if (value == "")
                            {
                                character.AlternativeName = null;
                                PluginService.CharacterMonitor.UpdateCharacter(character);
                            }
                            else
                            {
                                character.AlternativeName = value;
                                PluginService.CharacterMonitor.UpdateCharacter(character);
                            }
                        }
                        ImGui.TableNextColumn();
                        if (character.CharacterId != Service.ClientState.LocalContentId)
                        {
                            if (ImGui.SmallButton("移除##" + index))
                            {
                                PluginService.CharacterMonitor.RemoveCharacter(character.CharacterId);
                            }

                            ImGui.SameLine();
                        }

                        if (ImGui.SmallButton("清理所有背包##" + index))
                        {
                            ImGui.OpenPopup("您确定吗？##" + index);
                        }
                        if (ImGui.BeginPopupModal("您确定吗？##" + index))
                        {
                            ImGui.Text(
                                "您确定要清除该雇员的所有库存吗？\n这个选择不可逆！\n\n");
                            ImGui.Separator();

                            if (ImGui.Button("确定", new Vector2(120, 0) * ImGui.GetIO().FontGlobalScale))
                            {
                                PluginService.InventoryMonitor.ClearCharacterInventories(character.CharacterId);
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
        }
    }
}