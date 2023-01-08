using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Settings
{
    public class ShowItemNumberRetainerListSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ShowItemNumberRetainerList;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.ShowItemNumberRetainerList = newValue;
        }

        public override string Key { get; set; } = "ShowItemNumberRetainerList";
        public override string Name { get; set; } = "7）在雇员列表显示物品数量";

        public override string HelpText { get; set; } =
            "传唤铃列表中的雇员名称是否应该显示要分类或在他们的库存中可用的物品数量？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Visuals;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Highlighting;

    }
}