using System.Collections.Generic;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class HighlightWhenSetting : ChoiceSetting<string>
    {
        public static Dictionary<string, string> StaticChoices = new Dictionary<string, string>()
        {
            {"总是", "总是"}, {"搜索时", "搜索时"}
        };
        
        public override string DefaultValue { get; set; } = "搜索时";
        
        public override string CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.HighlightWhen;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, string newValue)
        {
            configuration.HighlightWhen = newValue;
        }

        public override string Key { get; set; } = "HighlightWhen";
        public override string Name { get; set; } = "3）什么时候高亮？";
        public override string HelpText { get; set; } = "当为过滤器打开高亮显示时，它应该始终显示还是只在项目中搜索时显示？";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Highlighting;

        public override Dictionary<string, string> Choices
        {
            get
            {
                return StaticChoices;
            }
        }
    }
}