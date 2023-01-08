using System.Collections.Generic;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class BackgroundFilterSetting : ChoiceSetting<string>
    {
        public override string DefaultValue { get; set; } = "";
        public override string CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ActiveBackgroundFilter ?? "";
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, string newValue)
        {
            if (newValue == "")
            {
                PluginService.FilterService.ClearActiveBackgroundFilter();
            }
            else
            {
                PluginService.FilterService.SetActiveBackgroundFilterByKey(newValue);
            }
        }

        public override string Key { get; set; } = "BackgroundFilter";
        public override string Name { get; set; } = "1）活动背景过滤器";

        public override string HelpText { get; set; } =
            "这是当Allagan Tools窗口不可见时激活的过滤器。 可以使用相关的斜杠指令切换此过滤器。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.FilterSettings;

        public override Dictionary<string, string> Choices
        {
            get
            {
                var filterItems = new Dictionary<string, string> {{"", "无"}};
                foreach (var config in PluginService.FilterService.FiltersList)
                {
                    filterItems.Add(config.Key, config.Name);
                }
                return filterItems;
            }
        }
    }
}