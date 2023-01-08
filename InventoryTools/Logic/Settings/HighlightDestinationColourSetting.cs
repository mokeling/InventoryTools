using System.Numerics;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class HighlightDestinationColourSetting : ColorSetting
    {
        public override Vector4 DefaultValue { get; set; } = new Vector4(0.321f, 0.239f, 0.03f, 1f);
        public override Vector4 CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.DestinationHighlightColor;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, Vector4 newValue)
        {
            configuration.DestinationHighlightColor = newValue;
        }

        public override string Key { get; set; } = "DestinationHighlightColour";
        public override string Name { get; set; } = "1）目标栏位高亮颜色";
        public override string HelpText { get; set; } = "设置目标栏位中与源过滤器匹配的物品的颜色（假设高亮目标栏位重复项已打开）。";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.DestinationHighlighting;
    }
}