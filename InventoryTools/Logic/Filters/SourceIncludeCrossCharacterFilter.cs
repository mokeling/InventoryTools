using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class SourceIncludeCrossCharacterFilter : BooleanFilter
    {
        public override int LabelSize { get; set; } = 240;
        public override string Key { get; set; } = "SourceIncludeCrossCharacter";
        public override string Name { get; set; } = "来源 - 跨角色？";
        public override string HelpText { get; set; } = "物品应该来自跨角色吗？ 如果未选择，将默认使用Allagan Tools配置中的默认配置。";
        public override FilterType AvailableIn { get; set; } = FilterType.SearchFilter | FilterType.SortingFilter | FilterType.CraftFilter;
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Inventories;

        public override bool? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.SourceIncludeCrossCharacter;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
        {
            configuration.SourceIncludeCrossCharacter = newValue;
        }

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }
    }
}