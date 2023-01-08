using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CriticalCommonLib;
using CriticalCommonLib.Interfaces;
using CriticalCommonLib.Models;
using CriticalCommonLib.Services;
using CriticalCommonLib.Sheets;
using Dalamud.Utility;
using ImGuiNET;
using InventoryTools.Extensions;
using InventoryTools.Logic;
using OtterGui;
using InventoryItem = FFXIVClientStructs.FFXIV.Client.Game.InventoryItem;

namespace InventoryTools.Ui
{
    class ItemWindow : Window
    {
        public override bool SaveState => false;
        public static string AsKey(uint itemId)
        {
            return "item_" + itemId;
        }
        private uint _itemId;
        private ItemEx? Item => Service.ExcelCache.GetItemExSheet().GetRow(_itemId); 
        public ItemWindow(uint itemId, string name = "Allagan Tools - 无效物品") : base(name)
        {
            _itemId = itemId;
            if (Item != null)
            {
                WindowName = "Allagan Tools - " + Item.Name;
                RetainerTasks = Item.RetainerTasks.ToArray();
                RecipesResult = Item.RecipesAsResult.ToArray();
                RecipesAsRequirement = Item.RecipesAsRequirement.ToArray();
                Vendors = Item.Vendors.SelectMany(shop => shop.ENpcs.SelectMany(npc => npc.Locations.Select(location => (shop, npc, location)))).ToList();
                GatheringSources = Item.GetGatheringSources().ToList();
                SharedModels = Item.GetSharedModels();
            }
            else
            {
                RetainerTasks = Array.Empty<RetainerTaskNormalEx>();
                RecipesResult = Array.Empty<RecipeEx>();
                RecipesAsRequirement = Array.Empty<RecipeEx>();
                GatheringSources = new();
                Vendors = new();
                SharedModels = new();
            }
        }

        public List<ItemEx> SharedModels { get; }

        private List<GatheringSource> GatheringSources { get; }

        private List<(IShop shop, ENpc npc, ILocation location)> Vendors { get; }

        private RecipeEx[] RecipesAsRequirement { get;  }

        private RecipeEx[] RecipesResult { get; }

        private RetainerTaskNormalEx[] RetainerTasks { get; }

        public override string Key => AsKey(_itemId);
        public override bool DestroyOnClose => true;
        public override void Draw()
        {
            if (Item == null)
            {
                ImGui.Text("找不到 ID 为 " + _itemId + " 的物品。");   
            }
            else
            {
                ImGui.Text("物品品级 " + Item.LevelItem.Row.ToString());
                ImGui.TextWrapped(Item.Description.ToDalamudString().ToString());
                var itemIcon = PluginService.IconStorage[Item.Icon];
                if (itemIcon != null)
                {
                    ImGui.Image(itemIcon.ImGuiHandle, new Vector2(100, 100) * ImGui.GetIO().FontGlobalScale);
                    if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled & ImGuiHoveredFlags.AllowWhenOverlapped & ImGuiHoveredFlags.AllowWhenBlockedByPopup & ImGuiHoveredFlags.AllowWhenBlockedByActiveItem & ImGuiHoveredFlags.AnyWindow) && ImGui.IsMouseReleased(ImGuiMouseButton.Right)) 
                    {
                        ImGui.OpenPopup("RightClick" + _itemId);
                    }
                    
                    if (ImGui.BeginPopup("RightClick" + _itemId))
                    {
                        Item.DrawRightClickPopup();
                        ImGui.EndPopup();
                    }
                }
                
                var garlandIcon = PluginService.IconStorage[65090];
                if (ImGui.ImageButton(garlandIcon.ImGuiHandle,
                        new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale))
                {
                    $"https://www.garlandtools.cn/db/#item/{_itemId}".OpenBrowser();
                }
                ImGuiUtil.HoverTooltip("在Garland Tools（国服）打开");
                ImGui.SameLine();
                var tcIcon = PluginService.IconStorage[60046];
                if (ImGui.ImageButton(tcIcon.ImGuiHandle,
                        new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale))
                {
                    $"https://ffxivteamcraft.com/db/zh/item/{_itemId}".OpenBrowser();
                }
                ImGuiUtil.HoverTooltip("在Teamcraft打开");
                if (Item.CanOpenCraftLog)
                {
                    ImGui.SameLine();
                    var craftableIcon = PluginService.IconStorage[66456];
                    if (ImGui.ImageButton(craftableIcon.ImGuiHandle,
                            new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale))
                    {
                        PluginService.GameInterface.OpenCraftingLog(_itemId);
                    }

                    ImGuiUtil.HoverTooltip("可制作 - 打开制作笔记");
                }
                if (Item.CanBeCrafted)
                {
                    ImGui.SameLine();
                    var craftableIcon = PluginService.IconStorage[60858];
                    if (ImGui.ImageButton(craftableIcon.ImGuiHandle,
                            new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale))
                    {
                        ImGui.OpenPopup("AddCraftList" + _itemId);
                    }
                    
                    if (ImGui.BeginPopup("AddCraftList" + _itemId))
                    {
                        var craftFilters =
                            PluginService.FilterService.FiltersList.Where(c =>
                                c.FilterType == Logic.FilterType.CraftFilter && !c.CraftListDefault);
                        foreach (var filter in craftFilters)
                        {
                            if (ImGui.Selectable("添加物品到制作清单 - " + filter.Name))
                            {
                                filter.CraftList.AddCraftItem(_itemId, 1, InventoryItem.ItemFlags.None);
                                PluginService.WindowService.OpenCraftsWindow();
                                PluginService.WindowService.GetCraftsWindow().FocusFilter(filter);
                            }
                        }
                        ImGui.EndPopup();
                    }

                    ImGuiUtil.HoverTooltip("可制作 - 添加到制作清单");
                }
                if (Item.CanOpenGatheringLog)
                {
                    ImGui.SameLine();
                    var gatherableIcon = PluginService.IconStorage[66457];
                    if (ImGui.ImageButton(gatherableIcon.ImGuiHandle,
                            new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale))
                    {
                        PluginService.GameInterface.OpenGatheringLog(_itemId);
                    }

                    ImGuiUtil.HoverTooltip("可采集 - 打开采集笔记");
                    
                    ImGui.SameLine();
                    var gbIcon = PluginService.IconStorage[63900];
                    if (ImGui.ImageButton(gbIcon.ImGuiHandle,
                            new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale))
                    {
                        Service.Commands.ProcessCommand("/gather " + Item.NameString);
                    }

                    ImGuiUtil.HoverTooltip("可采集 - 用Gather Buddy采集");
                }

                ImGui.Separator();
                if (ImGui.CollapsingHeader("来源 (" + Item.Sources.Count + ")", ImGuiTreeNodeFlags.DefaultOpen))
                {
                    ImGuiStylePtr style = ImGui.GetStyle();
                    float windowVisibleX2 = ImGui.GetWindowPos().X + ImGui.GetWindowContentRegionMax().X;
                    var sources = Item.Sources;
                    for (var index = 0; index < sources.Count; index++)
                    {
                        ImGui.PushID("Source"+index);
                        var source = sources[index];
                        var sourceIcon = PluginService.IconStorage[source.Icon];
                        if (sourceIcon != null)
                        {
                            if (source.ItemId != null)
                            {
                                if (ImGui.ImageButton(sourceIcon.ImGuiHandle,
                                        new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale, new(0, 0), new(1, 1), 0))
                                {
                                    PluginService.WindowService.OpenItemWindow(source.ItemId.Value);
                                }
                                if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled & ImGuiHoveredFlags.AllowWhenOverlapped & ImGuiHoveredFlags.AllowWhenBlockedByPopup & ImGuiHoveredFlags.AllowWhenBlockedByActiveItem & ImGuiHoveredFlags.AnyWindow) && ImGui.IsMouseReleased(ImGuiMouseButton.Right)) 
                                {
                                    ImGui.OpenPopup("RightClickSource" + source.ItemId);
                                }
                    
                                if (ImGui.BeginPopup("RightClickSource"+ source.ItemId))
                                {
                                    var itemEx = Service.ExcelCache.GetItemExSheet().GetRow(source.ItemId.Value);
                                    if (itemEx != null)
                                    {
                                        itemEx.DrawRightClickPopup();
                                    }

                                    ImGui.EndPopup();
                                }
                            }
                            else
                            {
                                ImGui.Image(sourceIcon.ImGuiHandle,
                                    new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale);
                            }

                            float lastButtonX2 = ImGui.GetItemRectMax().X;
                            float nextButtonX2 = lastButtonX2 + style.ItemSpacing.X + 32;
                            ImGuiUtil.HoverTooltip(source.FormattedName);
                            if (index + 1 < sources.Count && nextButtonX2 < windowVisibleX2)
                            {
                                ImGui.SameLine();
                            }
                        }

                        ImGui.PopID();
                    }
                }
                if (ImGui.CollapsingHeader("使用/奖励 (" + Item.Uses.Count + ")", ImGuiTreeNodeFlags.DefaultOpen))
                {
                    ImGuiStylePtr style = ImGui.GetStyle();
                    float windowVisibleX2 = ImGui.GetWindowPos().X + ImGui.GetWindowContentRegionMax().X;
                    var uses = Item.Uses;
                    for (var index = 0; index < uses.Count; index++)
                    {
                        ImGui.PushID("Use"+index);
                        var use = uses[index];
                        var useIcon = PluginService.IconStorage[use.Icon];
                        if (useIcon != null)
                        {
                            if (use.ItemId != null)
                            {
                                if (ImGui.ImageButton(useIcon.ImGuiHandle,
                                        new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale, new(0, 0), new(1, 1), 0))
                                {
                                    PluginService.WindowService.OpenItemWindow(use.ItemId.Value);
                                }
                                if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled & ImGuiHoveredFlags.AllowWhenOverlapped & ImGuiHoveredFlags.AllowWhenBlockedByPopup & ImGuiHoveredFlags.AllowWhenBlockedByActiveItem & ImGuiHoveredFlags.AnyWindow) && ImGui.IsMouseReleased(ImGuiMouseButton.Right)) 
                                {
                                    ImGui.OpenPopup("RightClickUse" + use.ItemId);
                                }
                    
                                if (ImGui.BeginPopup("RightClickUse"+ use.ItemId))
                                {
                                    var itemEx = Service.ExcelCache.GetItemExSheet().GetRow(use.ItemId.Value);
                                    if (itemEx != null)
                                    {
                                        itemEx.DrawRightClickPopup();
                                    }

                                    ImGui.EndPopup();
                                }
                            }
                            else
                            {
                                ImGui.Image(useIcon.ImGuiHandle,
                                    new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale);
                            }

                            float lastButtonX2 = ImGui.GetItemRectMax().X;
                            float nextButtonX2 = lastButtonX2 + style.ItemSpacing.X + 32;
                            ImGuiUtil.HoverTooltip(use.FormattedName);
                            if (index + 1 < uses.Count && nextButtonX2 < windowVisibleX2)
                            {
                                ImGui.SameLine();
                            }
                        }

                        ImGui.PopID();
                    }
                }

                void DrawSupplierRow((IShop shop, ENpc npc, ILocation location) tuple)
                {
                    ImGui.TableNextColumn();
                    ImGui.Text( tuple.npc.Resident?.Singular ?? "未知");
                    ImGui.TableNextColumn();
                    ImGui.Text(tuple.shop.Name);     
                    ImGui.TableNextColumn();
                    ImGui.TextWrapped(tuple.location + " ( " + Math.Round(tuple.location.MapX,2) + "/" + Math.Round(tuple.location.MapY,2) + ")");
                    ImGui.TableNextColumn();
                    if (ImGui.Button("打开地图链接##" + tuple.shop.RowId + "_" + tuple.npc.Key + "_" + tuple.location.MapEx.Row))
                    {
                        ChatUtilities.PrintFullMapLink(tuple.location, Item.NameString);
                    }


                }

                bool hasInformation = false;
                if (Vendors.Count != 0)
                {
                    hasInformation = true;
                    if (ImGui.CollapsingHeader("商店 (" + Vendors.Count + ")"))
                    {
                        ImGui.Text("商店：");
                        ImGuiTable.DrawTable("VendorsText", Vendors, DrawSupplierRow, ImGuiTableFlags.None,
                            new[] { "NPC", "商店名称", "位置", "" });
                    }
                }
                if (RetainerTasks.Length != 0)
                {
                    hasInformation = true;
                    if (ImGui.CollapsingHeader("雇员探险 (" + RetainerTasks.Count() + ")"))
                    {
                        ImGuiTable.DrawTable("雇员探险", RetainerTasks, DrawRetainerRow, ImGuiTableFlags.None,
                            new[] { "名称", "时间", "可获得数量" });
                    }
                }
                if (GatheringSources.Count != 0)
                {
                    hasInformation = true;
                    if (ImGui.CollapsingHeader("采集 (" + GatheringSources.Count + ")"))
                    {
                        ImGuiTable.DrawTable("采集", GatheringSources, DrawGatheringRow,
                            ImGuiTableFlags.None, new[] { "", "等级", "地点", "" });
                    }
                }
                if (RecipesAsRequirement.Length != 0)
                {
                    hasInformation = true;
                    if (ImGui.CollapsingHeader("配方 (" + RecipesAsRequirement.Length + ")"))
                    {
                        ImGuiStylePtr style = ImGui.GetStyle();
                        float windowVisibleX2 = ImGui.GetWindowPos().X + ImGui.GetWindowContentRegionMax().X;
                        for (var index = 0; index < RecipesAsRequirement.Length; index++)
                        {
                            ImGui.PushID(index);
                            var recipe = RecipesAsRequirement[index];
                            if (recipe.ItemResultEx.Value != null)
                            {
                                var icon = PluginService.IconStorage.LoadIcon(recipe.ItemResultEx.Value.Icon);
                                if (ImGui.ImageButton(icon.ImGuiHandle, new(32, 32)))
                                {
                                    PluginService.GameInterface.OpenCraftingLog(recipe.ItemResult.Row, recipe.RowId);
                                }
                                if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled & ImGuiHoveredFlags.AllowWhenOverlapped & ImGuiHoveredFlags.AllowWhenBlockedByPopup & ImGuiHoveredFlags.AllowWhenBlockedByActiveItem & ImGuiHoveredFlags.AnyWindow) && ImGui.IsMouseReleased(ImGuiMouseButton.Right)) 
                                {
                                    ImGui.OpenPopup("RightClick" + recipe.RowId);
                                }
                    
                                if (ImGui.BeginPopup("RightClick"+ recipe.RowId))
                                {
                                    if (recipe.ItemResultEx.Value != null)
                                    {
                                        recipe.ItemResultEx.Value.DrawRightClickPopup();
                                    }

                                    ImGui.EndPopup();
                                }

                                float lastButtonX2 = ImGui.GetItemRectMax().X;
                                float nextButtonX2 = lastButtonX2 + style.ItemSpacing.X + 32;
                                ImGuiUtil.HoverTooltip(recipe.ItemResultEx.Value!.NameString + " - " +
                                                       (recipe.CraftType.Value?.Name ?? "未知"));
                                if (index + 1 < RecipesAsRequirement.Length && nextButtonX2 < windowVisibleX2)
                                {
                                    ImGui.SameLine();
                                }
                            }

                            ImGui.PopID();
                        }
                    }
                }

                if (SharedModels.Count != 0)
                {
                    hasInformation = true;
                    if (ImGui.CollapsingHeader("同模 (" + SharedModels.Count + ")"))
                    {
                        ImGuiStylePtr style = ImGui.GetStyle();
                        float windowVisibleX2 = ImGui.GetWindowPos().X + ImGui.GetWindowContentRegionMax().X;
                        for (var index = 0; index < SharedModels.Count; index++)
                        {
                            ImGui.PushID(index);
                            var sharedModel = SharedModels[index];
                            var icon = PluginService.IconStorage.LoadIcon(sharedModel.Icon);
                            if (ImGui.ImageButton(icon.ImGuiHandle, new(32, 32)))
                            {
                                PluginService.WindowService.OpenItemWindow(sharedModel.RowId);
                            }
                            if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled & ImGuiHoveredFlags.AllowWhenOverlapped & ImGuiHoveredFlags.AllowWhenBlockedByPopup & ImGuiHoveredFlags.AllowWhenBlockedByActiveItem & ImGuiHoveredFlags.AnyWindow) && ImGui.IsMouseReleased(ImGuiMouseButton.Right)) 
                            {
                                ImGui.OpenPopup("RightClick" + sharedModel.RowId);
                            }
                
                            if (ImGui.BeginPopup("RightClick"+ sharedModel.RowId))
                            {
                                sharedModel.DrawRightClickPopup();
                                ImGui.EndPopup();
                            }

                            float lastButtonX2 = ImGui.GetItemRectMax().X;
                            float nextButtonX2 = lastButtonX2 + style.ItemSpacing.X + 32;
                            ImGuiUtil.HoverTooltip(sharedModel.NameString);
                            if (index + 1 < SharedModels.Count && nextButtonX2 < windowVisibleX2)
                            {
                                ImGui.SameLine();
                            }

                            ImGui.PopID();
                        }
                    }
                }
                if (!hasInformation)
                {
                    ImGui.Text("没有可用信息。");
                }
                
                #if DEBUG
                if (ImGui.CollapsingHeader("调试"))
                {
                    ImGui.Text("物品 ID: " + _itemId);
                    Utils.PrintOutObject(Item, 0, new List<string>());
                }
                #endif

            }
        }

        private void DrawGatheringRow(GatheringSource obj)
        {
            ImGui.TableNextColumn();
            ImGui.PushID(obj.GetHashCode());
            var source = obj.Source;
            var icon = PluginService.IconStorage[source.Icon];
            if (ImGui.ImageButton(icon.ImGuiHandle, new(32, 32)))
            {
                PluginService.GameInterface.OpenGatheringLog(_itemId);
            }
            ImGuiUtil.HoverTooltip(source.Name + " - 打开采集笔记");
            ImGui.TableNextColumn();
            ImGui.Text(obj.Level.GatheringItemLevel.ToString());     
            ImGui.TableNextColumn();
            ImGui.TextWrapped(obj.PlaceName.Name + " - " + (obj.TerritoryType.PlaceName.Value?.Name ?? "未知"));
            ImGui.PopID();
        }

        private void DrawRecipeResultRow(RecipeEx obj)
        {

        }

        private void DrawRetainerRow(RetainerTaskNormalEx obj)
        {
            ImGui.TableNextColumn();
            ImGui.Text( obj.TaskName);
            ImGui.TableNextColumn();
            ImGui.Text(obj.TaskTime + " 分钟");     
            ImGui.TableNextColumn();
            ImGui.TextWrapped(obj.Quantities);
        }

        public override void Invalidate()
        {
            
        }
        public override FilterConfiguration? SelectedConfiguration => null;
        public override Vector2 DefaultSize { get; } = new Vector2(500, 800);
        public override Vector2 MaxSize => new (800, 1500);
        public override Vector2 MinSize => new (100, 100);
    }
}