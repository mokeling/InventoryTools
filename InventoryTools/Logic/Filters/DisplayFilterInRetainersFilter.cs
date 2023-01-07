using System;
using System.Collections.Generic;
using System.Linq;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters
{
    public class DisplayFilterInRetainersFilter : ChoiceFilter<FilterItemsRetainerEnum>
    {
        public override string Key { get; set; } = "FilterInRetainers";
        public override string Name { get; set; } = "在雇员中过滤物品？";

        public override string HelpText { get; set; } =
            "与雇员交谈时，过滤器是否应该自行调整以仅显示应从您的库存中放入雇员中的物品？ 如果设置为仅，只会在传唤铃和雇员内高亮。";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Display;
        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        
        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            return null;
        }

        public override FilterItemsRetainerEnum CurrentValue(FilterConfiguration configuration)
        {
            return configuration.FilterItemsInRetainersEnum;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, FilterItemsRetainerEnum newValue)
        {
            configuration.FilterItemsInRetainersEnum = newValue;
        }

        public override FilterItemsRetainerEnum EmptyValue { get; set; } = FilterItemsRetainerEnum.No;
        public override List<FilterItemsRetainerEnum> GetChoices(FilterConfiguration configuration)
        {
            return Enum.GetValues<FilterItemsRetainerEnum>().ToList();
        }

        public override string GetFormattedChoice(FilterItemsRetainerEnum choice)
        {
            if (choice == FilterItemsRetainerEnum.No)
            {
                return "否";
            }

            if (choice == FilterItemsRetainerEnum.Yes)
            {
                return "是";
            }

            if (choice == FilterItemsRetainerEnum.Only)
            {
                return "仅";
            }

            return choice.ToString();
        }
    }
}