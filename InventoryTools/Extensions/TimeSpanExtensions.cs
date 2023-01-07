using System;

namespace InventoryTools.Extensions
{
    public static class TimespanExtensions
    {
        public static string ToHumanReadableString (this TimeSpan t)
        {
            if (t.TotalMinutes <= 1) {
                return $@"{t:%s} 秒";
            }
            if (t.TotalHours <= 1) {
                return $@"{t:%m} 分钟";
            }
            if (t.TotalDays <= 1) {
                return $@"{t:%h} 小时";
            }

            return $@"{t:%d} 天";
        }
    }
}