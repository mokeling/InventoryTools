using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;
using InventoryTools.Misc;

namespace InventoryTools.Logic.Columns
{
    public class IsHousingItemColumn : CheckboxColumn
    {
        public override bool? CurrentValue(InventoryItem item)
        {
            return item.Item == null ? false : CurrentValue(item.Item);
        }

        public override bool? CurrentValue(ItemEx item)
        {
            return Helpers.HousingCategoryIds.Contains(item.ItemUICategory.Row);
        }

        public override bool? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "是房屋物品？";
        public override float Width { get; set; } = 100;
        public override string HelpText { get; set; } = "这个物品是房屋物品吗？ 这可能暂时有点不准确。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}