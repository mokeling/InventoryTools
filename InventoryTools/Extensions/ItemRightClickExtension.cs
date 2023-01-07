using System.Linq;
using CriticalCommonLib;
using CriticalCommonLib.Crafting;
using CriticalCommonLib.Services;
using CriticalCommonLib.Sheets;
using ImGuiNET;
using InventoryTools.Logic;
using InventoryItem = FFXIVClientStructs.FFXIV.Client.Game.InventoryItem;

namespace InventoryTools.Extensions
{
    public static class ItemRightClickExtension
    {
        public static void DrawRightClickPopup(this ItemEx item)
        {
            DrawMenuItems(item);
            bool firstItem = true;
            
            var craftFilters =
                PluginService.FilterService.FiltersList.Where(c =>
                    c.FilterType == Logic.FilterType.CraftFilter && !c.CraftListDefault).ToArray();
            foreach (var filter in craftFilters)
            {
                if (item.CanBeCrafted && !Service.ExcelCache.IsCompanyCraft(item.RowId))
                {
                    if (firstItem)
                    {
                        ImGui.Separator();
                        firstItem = false;
                    }
                    if (ImGui.Selectable("添加到制作清单 - " + filter.Name))
                    {
                        filter.CraftList.AddCraftItem(item.RowId);
                        PluginService.WindowService.OpenCraftsWindow();
                        PluginService.WindowService.GetCraftsWindow().FocusFilter(filter);
                        filter.NeedsRefresh = true;
                        filter.StartRefresh();
                    }
                }
                if (item.CanBeCrafted && Service.ExcelCache.IsCompanyCraft(item.RowId))
                {
                    //TODO: replace with a dynamic way of determining number of steps
                    for (uint i = 0; i < 3; i++)
                    {
                        if (firstItem)
                        {
                            ImGui.Separator();
                            firstItem = false;
                        }
                        if (ImGui.Selectable("添加阶段 " + i + " 到制作清单 - " + filter.Name))
                        {
                            filter.CraftList.AddCraftItem(item.RowId, 1, InventoryItem.ItemFlags.None, i);
                            PluginService.WindowService.OpenCraftsWindow();
                            PluginService.WindowService.GetCraftsWindow().FocusFilter(filter);
                            filter.NeedsRefresh = true;
                            filter.StartRefresh();
                        }
                    }
                }
            }

            if (item.CanBeCrafted && !Service.ExcelCache.IsCompanyCraft(item.RowId))
            {
                if (ImGui.Selectable("添加到新的制作清单"))
                {
                    Service.Framework.RunOnTick(() =>
                    {
                        var filter = PluginService.FilterService.AddNewCraftFilter();
                        filter.CraftList.AddCraftItem(item.RowId);
                        PluginService.WindowService.OpenCraftsWindow();
                        PluginService.WindowService.GetCraftsWindow().FocusFilter(filter);
                        filter.NeedsRefresh = true;
                        filter.StartRefresh();
                    });
                }
            }

            if (item.CanBeCrafted && Service.ExcelCache.IsCompanyCraft(item.RowId))
            {
                for (uint i = 0; i < 3; i++)
                {
                    if (ImGui.Selectable("添加阶段 " + i + " 到新的制作清单"))
                    {
                        Service.Framework.RunOnTick(() =>
                        {
                            var filter = PluginService.FilterService.AddNewCraftFilter();
                            filter.CraftList.AddCraftItem(item.RowId);
                            PluginService.WindowService.OpenCraftsWindow();
                            PluginService.WindowService.GetCraftsWindow().FocusFilter(filter);
                            filter.NeedsRefresh = true;
                            filter.StartRefresh();
                        });
                    }
                }
            }
        }
        public static void DrawRightClickPopup(this CraftItem item, FilterConfiguration configuration)
        {
            DrawMenuItems(item.Item);
            bool firstItem = true;
            if (item.Item.CanOpenCraftLog)
            {
                if (firstItem)
                {
                    ImGui.Separator();
                    firstItem = false;
                }
                if (ImGui.Selectable("在制作笔记打开"))
                {
                    PluginService.GameInterface.OpenCraftingLog(item.ItemId, item.RecipeId);
                }
            }

            if (item.Item.CanBeCrafted && item.IsOutputItem)
            {
                if (firstItem)
                {
                    ImGui.Separator();
                    firstItem = false;
                }
                if (ImGui.Selectable("从制作清单中移除"))
                {
                    configuration.CraftList.RemoveCraftItem(item.ItemId, item.Flags);
                    configuration.NeedsRefresh = true;
                    configuration.StartRefresh();
                }
            }

            if (item.Item.CanBeCrafted && item.IsOutputItem && item.Phase != null && Service.ExcelCache.IsCompanyCraft(item.ItemId))
            {
                for (uint i = 0; i < 3; i++)
                {
                    if (item.Phase != i)
                    {
                        if (firstItem)
                        {
                            ImGui.Separator();
                            firstItem = false;
                        }
                        if (ImGui.Selectable("切换到阶段 " + (i + 1)))
                        {
                            item.SwitchPhase(i);
                            configuration.StartRefresh();
                        }
                    }
                }
            }

            if (!item.IsOutputItem)
            {
                var craftFilters =
                    PluginService.FilterService.FiltersList.Where(c =>
                        c.FilterType == Logic.FilterType.CraftFilter && !c.CraftListDefault);
                if (item.Item.CanBeCrafted && !Service.ExcelCache.IsCompanyCraft(item.Item.RowId))
                {
                    foreach (var filter in craftFilters)
                    {

                        //TODO: replace with a dynamic way of determining number of steps
                        if (firstItem)
                        {
                            ImGui.Separator();
                            firstItem = false;
                        }

                        if (ImGui.Selectable("添加 " + item.QuantityNeeded + " 物品到制作清单 - " + filter.Name))
                        {
                            filter.CraftList.AddCraftItem(item.Item.RowId, item.QuantityNeeded,
                                InventoryItem.ItemFlags.None);
                            PluginService.WindowService.OpenCraftsWindow();
                            PluginService.WindowService.GetCraftsWindow().FocusFilter(filter);
                            configuration.NeedsRefresh = true;
                            configuration.StartRefresh();
                        }
                    }
                    if (ImGui.Selectable("添加 " + item.QuantityNeeded + " 物品到新制作清单"))
                    {
                        Service.Framework.RunOnTick(() =>
                        {
                            var filter = PluginService.FilterService.AddNewCraftFilter();
                            filter.CraftList.AddCraftItem(item.Item.RowId, item.QuantityNeeded,
                                InventoryItem.ItemFlags.None);
                            PluginService.WindowService.OpenCraftsWindow();
                            PluginService.WindowService.GetCraftsWindow().FocusFilter(filter);
                            configuration.NeedsRefresh = true;
                            configuration.StartRefresh();
                        });
                    }
                }
            }

        }
        
        public static void DrawMenuItems(ItemEx item)
        {
            ImGui.Text(item.NameString);
            ImGui.Separator();
            if (ImGui.Selectable("在Garland Tools（国服）打开"))
            {
                $"https://garlandtools.cn/db/#item/{item.RowId}".OpenBrowser();
            }
            if (ImGui.Selectable("在Teamcraft打开"))
            {
                $"https://ffxivteamcraft.com/db/en/item/{item.RowId}".OpenBrowser();
            }
            if (ImGui.Selectable("在Universalis打开"))
            {
                $"https://universalis.app/market/{item.RowId}".OpenBrowser();
            }
            if (ImGui.Selectable("复制名称"))
            {
                item.NameString.ToClipboard();
            }
            if (ImGui.Selectable("链接"))
            {
                ChatUtilities.LinkItem(item);
            }
            if (item.CanTryOn && ImGui.Selectable("尝试"))
            {
                if (PluginService.TryOn.CanUseTryOn)
                {
                    PluginService.TryOn.TryOnItem(item);
                }
            }

            if (item.CanOpenCraftLog && ImGui.Selectable("打开制作笔记"))
            {
                PluginService.GameInterface.OpenCraftingLog(item.RowId);
            }

            if (item.CanOpenGatheringLog && ImGui.Selectable("打开采集笔记"))
            {
                PluginService.GameInterface.OpenGatheringLog(item.RowId);
            }

            if (item.CanOpenGatheringLog && ImGui.Selectable("用Gather Buddy采集"))
            {
                Service.Commands.ProcessCommand("/gather " + item.NameString);
            }

            if (ImGui.Selectable("更多信息"))
            {
                PluginService.WindowService.OpenItemWindow(item.RowId);   
            }
            
        }
    }
}