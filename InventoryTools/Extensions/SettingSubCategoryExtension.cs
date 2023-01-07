using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Extensions
{
    public static class SettingSubCategoryExtensions
    {
        public static string FormattedName(this SettingSubCategory settingSubCategory)
        {
            switch (settingSubCategory)
            {
                case SettingSubCategory.Experimental:
                    return "实验性";
                case SettingSubCategory.Highlighting:
                    return "高亮";
                case SettingSubCategory.DestinationHighlighting:
                    return "目标栏位高亮";
                case SettingSubCategory.Market:
                    return "市场";
                case SettingSubCategory.Tooltips:
                    return "帮助提示";
                case SettingSubCategory.AutoSave:
                    return "自动保存";
                case SettingSubCategory.FilterSettings:
                    return "过滤器设置";
                case SettingSubCategory.ContextMenus:
                    return "上下文/右键菜单";
                case SettingSubCategory.Hotkeys:
                    return "热键";
            }
            return settingSubCategory.ToString();
        }
    }
}