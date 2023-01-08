using System.Numerics;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class TabHighlightColourSetting : ColorSetting
    {
        public override Vector4 DefaultValue { get; set; } = new(0f, 0f,
            0f, 0.18f);
        
        public override Vector4 CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TabHighlightColor;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, Vector4 newValue)
        {
            configuration.TabHighlightColor = newValue;
        }

        public override string Key { get; set; } = "TabHighlightColour";
        public override string Name { get; set; } = "8）标签高亮颜色";
        public override string HelpText { get; set; } = "将突出显示的标签（包含筛选项）设置为的颜色。";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Highlighting;

    }
}