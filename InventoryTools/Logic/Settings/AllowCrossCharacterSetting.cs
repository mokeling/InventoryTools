using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class AllowCrossCharacterSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.DisplayCrossCharacter;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.DisplayCrossCharacter = newValue;
        }

        public override string Key { get; set; } = "DisplayCrossCharacter";
        public override string Name { get; set; } = "允许跨角色库存？";

        public override string HelpText { get; set; } =
            "这是一项实验性功能，是否应该在过滤器配置中显示当前未登录的角色及其雇员？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Experimental;
    }
}