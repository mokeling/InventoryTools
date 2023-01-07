using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class CanBePurchasedFilter : BooleanFilter
    {
        public override string Key { get; set; } = "Purchasable";
        public override string Name { get; set; } = "可以购买吗？";
        public override string HelpText { get; set; } = "可以购买此物品吗？";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Acquisition;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {

            return FilterItem(configuration, item.Item);
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            var canBePurchased = item.ObtainedGil;
            return currentValue == null || currentValue.Value && canBePurchased || !currentValue.Value && !canBePurchased;
        }
    }
}