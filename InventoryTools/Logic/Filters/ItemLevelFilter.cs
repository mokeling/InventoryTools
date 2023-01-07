using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class ItemLevelFilter : StringFilter
    {
        public override string Key { get; set; } = "ILvl";
        public override string Name { get; set; } = "品级";
        public override string HelpText { get; set; } = "物品品级。";
        public override FilterType AvailableIn { get; set; }  = FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {
            return FilterItem(configuration, item.Item);
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            if (!string.IsNullOrEmpty(currentValue))
            {
                if (item.EquipSlotCategory.Row != 0 &&((int)item.LevelItem.Row).PassesFilter(currentValue.ToLower()))
                {
                    return true;
                }

                return false;
            }
            return true;
        }
    }
}