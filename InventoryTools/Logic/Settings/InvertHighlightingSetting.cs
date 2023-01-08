using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class InvertHighlightingSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.InvertHighlighting;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.InvertHighlighting = newValue;
        }

        public override string Key { get; set; } = "InvertHighlighting";
        public override string Name { get; set; } = "4）反转突出显示？";

        public override string HelpText { get; set; } =
            "是否应该突出显示所有不匹配过滤器的项目？ 这可以在过滤器配置中被覆盖。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Highlighting;

    }
}