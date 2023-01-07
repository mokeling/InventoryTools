using System.Collections.Generic;
using System.Linq;
using CriticalCommonLib.Extensions;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class SourceInventoryCategoriesFilter : MultipleChoiceFilter<InventoryCategory>
    {
        public override List<InventoryCategory> CurrentValue(FilterConfiguration configuration)
        {
            return configuration.SourceCategories?.ToList() ?? new List<InventoryCategory>();
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, List<InventoryCategory> newValue)
        {
            configuration.SourceCategories = newValue.Count == 0 ? null : newValue.Distinct().ToHashSet();
        }
        
        public override int LabelSize { get; set; } = 240;
        public override string Key { get; set; } = "SourceInventoryCategories";
        public override string Name { get; set; } = "来源 - 库存类别";
        public override string HelpText { get; set; } =
            "这是要搜索的来源类别列表。它将尝试搜索任何给定类别库存中的物品。";
        
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