using System;

namespace ETool.Core.MyUtil
{
    /// <summary>
    /// 随机生成工具类
    /// </summary>
    public static class RandomUtil
    {
        /// <summary>
        /// 获取一个指定格式的随机 GUID 字符串
        /// </summary>
        /// <param name="format">规则字符串</param>
        /// <returns>符合格式规则的 GUID 字符串（异常场景返回全 0 的 GUID 字符串）</returns>
        public static string GetGuidString(string format = "D")
        {
            // null：返回全 0 GUID 字符串
            if (format == null)
            {
                return "00000000-0000-0000-0000-000000000000";
            }

            // 空字符串：返回 D 格式 UUID 字符串
            if (format.Length == 0)
            {
                return Guid.NewGuid().ToString("D");
            }

            // 小写 -> 大写
            format = Util.StrUtil.ToUpperLetter(format);

            // 合法 GUID 格式符集合
            const string roleString = "DNBPX";

            // 双字符规则：相同且为 DNBPX 中任意一个 → 返回大写 GUID 字符串
            if (format.Length == 2 && format[0] == format[1] && roleString.Contains(format[0].ToString()))
            {
                return Util.StrUtil.ToUpperLetter(Guid.NewGuid().ToString(format[0].ToString()));
            }

            // 单字符规则：为 DNBPX 中任意一个  → 返回小写 GUID 字符串
            if (format.Length == 1 && roleString.Contains(format))
            {
                return Guid.NewGuid().ToString(format);
            }

            // 其他情况：返回全 0 GUID 字符串
            return "00000000-0000-0000-0000-000000000000";
        }
        
        /// <summary>
        /// 生成随机日期
        /// </summary>
        /// <param name="startDate">随机日期的最早时间</param>
        /// <param name="endDate">随机日期的最晚时间</param>
        /// <returns>生成的随机日期</returns>
        public static DateTime RandomDate(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, 0, new Random().Next(0, (int)timeSpan.TotalSeconds));
            return startDate + newSpan;
        }
    }
}
