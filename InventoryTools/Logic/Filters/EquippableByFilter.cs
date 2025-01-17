﻿using System.Collections.Generic;
using System.Linq;
using CriticalCommonLib;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class EquippableByFilter : UintMultipleChoiceFilter
    {
        public override string Key { get; set; } = "EquippableBy";
        public override string Name { get; set; } = "装备职业";
        public override string HelpText { get; set; } = "哪些职业可以装备这件装备？";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return FilterItem(configuration, item.Item) == true;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = this.CurrentValue(configuration);
            if (currentValue.Count == 0)
            {
                return true;
            }
            Service.ExcelCache.CalculateClassJobCategoryLookup();
            var lookup = Service.ExcelCache.ClassJobCategoryLookup;
            if (lookup.ContainsKey(item.ClassJobCategory.Row))
            {
                var map = lookup[item.ClassJobCategory.Row];
                if (map.Any(c => currentValue.Contains(c)))
                {
                    return true;
                }
            }

            return false;
        }

        public override Dictionary<uint, string> GetChoices(FilterConfiguration configuration)
        {
            var choices = new Dictionary<uint, string>();
            var sheet = Service.ExcelCache.GetClassJobSheet();
            foreach (var classJob in sheet)
            {
                choices.Add(classJob.RowId, classJob.Name.ToString().ToTitleCase());
            }

            return choices;
        }

        public override bool HideAlreadyPicked { get; set; } = true;
    }
}