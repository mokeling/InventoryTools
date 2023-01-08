using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class TooltipColorSetting : GameColorSetting
    {
        public override uint? DefaultValue { get; set; } = null;
        public override uint? CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipColor;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, uint? newValue)
        {
            configuration.TooltipColor = newValue;
        }

        public override string Key { get; set; } = "TooltipColor";
        public override string Name { get; set; } = "帮助提示颜色";
        public override string HelpText { get; set; } = "这是帮助提示中文本的颜色。";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Tooltips;
    }
}