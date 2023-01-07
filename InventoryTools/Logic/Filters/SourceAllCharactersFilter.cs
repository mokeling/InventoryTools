using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class SourceAllCharactersFilter : BooleanFilter
    {
        public override int LabelSize { get; set; } = 240;
        public override string Key { get; set; } = "SourceAllCharacters";
        public override string Name { get; set; } = "来源 - 所有角色？";
        public override string HelpText { get; set; } = "使用每个角色库存作为来源。这通常只是您自己的角色，除非您启用了跨角色库存跟踪。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Inventories;
        public override FilterType AvailableIn { get; set; } = FilterType.SearchFilter | FilterType.SortingFilter | FilterType.CraftFilter;
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }

        public override bool? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.SourceAllCharacters;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
        {
            configuration.SourceAllCharacters = newValue;
        }
    }
}