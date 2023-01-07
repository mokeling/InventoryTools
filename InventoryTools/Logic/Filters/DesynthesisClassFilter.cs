using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;

namespace InventoryTools.Logic.Filters;

public class DesynthesisClassFilter : StringFilter
{
    public override string Key { get; set; } = "DesynthesisClass";
    public override string Name { get; set; } = "制作分解职业";
    public override string HelpText { get; set; } = "这个物品需要什么职业来制作/分解？";
    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

    public override FilterType AvailableIn { get; set; } = FilterType.SearchFilter | FilterType.SortingFilter |
                                                           FilterType.GameItemFilter;
    
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return FilterItem(configuration, item.Item);
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
    {
        var currentValue = CurrentValue(configuration);
        if (!string.IsNullOrEmpty(currentValue))
        {
            if (item.Desynth == 0 || item.ClassJobRepair.Row == 0)
            {
                return false;
            }

            var valueName = item.ClassJobRepair.Value?.Name.ToString() ?? "未知";
            if (!valueName.PassesFilter(currentValue.ToLower()))
            {
                return false;
            }
        }

        return true;
    }
}