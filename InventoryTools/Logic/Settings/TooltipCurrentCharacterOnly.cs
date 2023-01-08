using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class TooltipCurrentCharacterSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;
        
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipCurrentCharacter;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipCurrentCharacter = newValue;
        }

        public override string Key { get; set; } = "TooltipCurrentCharacter";
        public override string Name { get; set; } = "6）限制属于当前角色的物品？";

        public override string HelpText { get; set; } =
            "将帮助提示上限制为只显示属于当前登录角色的库存。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Tooltips;
    }
}