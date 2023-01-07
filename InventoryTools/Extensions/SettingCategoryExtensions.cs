using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Extensions
{
    public static class SettingCategoryExtensions
    {
        public static string FormattedName(this SettingCategory settingCategory)
        {
            switch (settingCategory)
            {
                case SettingCategory.General:
                    return "通用";
                case SettingCategory.Visuals:
                    return "视效";
                case SettingCategory.MarketBoard:
                    return "市场板";
            }
            return settingCategory.ToString();
        }
    }
}