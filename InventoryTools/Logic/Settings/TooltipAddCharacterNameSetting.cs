using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class TooltipAddCharacterNameSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;
        
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipAddCharacterNameOwned;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipAddCharacterNameOwned = newValue;
        }

        public override string Key { get; set; } = "TooltipCharacterName";
        public override string Name { get; set; } = "1）将角色名称添加到拥有？";

        public override string HelpText { get; set; } =
            "当悬停在你的雇员也拥有的物品上时，该雇员的所有者是否应该添加到该物品上？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Tooltips;

    }
}