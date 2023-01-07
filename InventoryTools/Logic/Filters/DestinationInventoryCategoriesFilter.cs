using System.Collections.Generic;
using System.Linq;
using CriticalCommonLib.Extensions;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class DestinationInventoryCategoriesFilter : MultipleChoiceFilter<InventoryCategory>
    {
        public override List<InventoryCategory> CurrentValue(FilterConfiguration configuration)
        {
            return configuration.DestinationCategories?.ToList() ?? new List<InventoryCategory>();
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, List<InventoryCategory> newValue)
        {
            configuration.DestinationCategories = newValue.Count == 0 ? null : newValue.Distinct().ToHashSet();
        }
        public override int LabelSize { get; set; } = 240;
        public override string Key { get; set; } = "DestinationInventoryCategories";
        public override string Name { get; set; } = "目标栏位 - 库存类别";
        public override string HelpText { get; set; } =
            "这是要将物品分类到目标栏位的类别列表。它会尝试将物品分类到任何给定类别的背包中。";
        
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Inventories;
        public override FilterType AvailableIn { get; set; } = FilterType.SortingFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }

        public override Dictionary<InventoryCategory, string> GetChoices(FilterConfiguration configuration)
        {
           
            var dict = new Dictionary<InventoryCategory, string>();
            dict.Add(InventoryCategory.RetainerBags, "雇员 " +InventoryCategory.RetainerBags.FormattedName());
            dict.Add(InventoryCategory.RetainerMarket, "雇员 " +InventoryCategory.RetainerMarket.FormattedName());
            dict.Add(InventoryCategory.CharacterEquipped, InventoryCategory.CharacterEquipped.FormattedName());
            dict.Add(InventoryCategory.RetainerEquipped, "雇员 " +InventoryCategory.RetainerEquipped.FormattedName());
            dict.Add(InventoryCategory.CharacterBags, InventoryCategory.CharacterBags.FormattedName());
            dict.Add(InventoryCategory.CharacterSaddleBags, InventoryCategory.CharacterSaddleBags.FormattedName());
            dict.Add(InventoryCategory.CharacterPremiumSaddleBags, InventoryCategory.CharacterPremiumSaddleBags.FormattedName());
            dict.Add(InventoryCategory.FreeCompanyBags, InventoryCategory.FreeCompanyBags.FormattedName());
            dict.Add(InventoryCategory.CharacterArmoryChest, InventoryCategory.CharacterArmoryChest.FormattedName());
            dict.Add(InventoryCategory.GlamourChest, InventoryCategory.GlamourChest.FormattedName());
            dict.Add(InventoryCategory.Armoire, InventoryCategory.Armoire.FormattedName());
            dict.Add(InventoryCategory.Currency, InventoryCategory.Currency.FormattedName());
            dict.Add(InventoryCategory.Crystals, InventoryCategory.Crystals.FormattedName());

            return dict;
        }

        public override bool HideAlreadyPicked { get; set; } = true;
    }
}