using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class SearchCategoryColumn : TextColumn
    {
        public override string? CurrentValue(InventoryItem item)
        {
            if (item.ItemSearchCategory != null)
            {
                return item.FormattedSearchCategory;
            }

            return "";
        }

        public override string? CurrentValue(ItemEx item)
        {
            if (item.ItemSearchCategory != null)
            {
                return item.FormattedSearchCategory;
            }

            return "";
        }

        public override string? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override string Name { get; set; } = "市场板类别";
        public override float Width { get; set; } = 200.0f;

        public override string HelpText { get; set; } =
            "基于市场板搜索类别的物品类别。";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}