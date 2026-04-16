using System;
using System.Collections.Generic;
using System.Threading;

namespace ETool.Core.Todo.MyUtil
{
    /// <summary>
    /// 随机生成工具类
    /// </summary>
    public static class RandomUtil
    {
        /// <summary>
        /// 每一个线程本地存储的 Random 实例【保证多线程环境下随机数生成的线程安全与随机性】
        /// </summary>
        private static readonly ThreadLocal<Random> RandomThreadLocal = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

        /// <summary>
        /// 获取一个线程安全的 Random 实例
        /// </summary>
        /// <returns>线程安全的 Random 实例</returns>
        public static Random GetRandom()
        {
            return RandomThreadLocal.Value;
        }

        /// <summary>
        /// 获取一个指定区间内的随机整数
        /// </summary>
        /// <param name="minValue">随机数的下限（包含）</param>
        /// <param name="maxValue">随机数的上限（不包含）</param>
        /// <returns>一个随机整数</returns>
        public static int GetRandomInt(int minValue, int maxValue)
        {
            if (minValue == maxValue)
            {
                return minValue;
            }

            NumberUtil.SwapIfFirstLarger(ref minValue, ref maxValue);

            return GetRandom().Next(minValue, maxValue);
        }

        /// <summary>
        /// 获取一个随机的布尔值
        /// </summary>
        /// <returns>随机的布尔值</returns>
        public static bool GetRandomBool()
        {
            return GetRandomInt(0, 2) == 0;
        }

        /// <summary>
        /// 获取一个随机的数字字符
        /// </summary>
        /// <param name="minValue">最小的随机字符对应的数值（包含），默认值为 0</param>
        /// <param name="maxValue">最大的随机字符对应的数值（包含），默认值为 9</param>
        /// <returns>介于 <c>minValue</c> 和 <c>maxValue</c> 之间的随机数字字符</returns>
        public static char GetRandomDigitChar(int minValue = 0, int maxValue = 9)
        {
            // 负数自动修正为 0
            minValue = Math.Max(minValue, 0);
            maxValue = Math.Max(maxValue, 0);

            // 自动修正参数顺序
            NumberUtil.SwapIfFirstLarger(ref minValue, ref maxValue);

            char[] defaultChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            // 超出有效范围时：返回 0-9 的随机字符
            if (minValue > 9)
            {
                return defaultChars[GetRandomInt(0, 10)];
            }

            return defaultChars[GetRandomInt(minValue, maxValue + 1)];
        }

        /// <summary>
        /// 获取一个指定长度范围的随机字符串
        /// </summary>
        /// <param name="minLength">字符串的最小长度（包含）</param>
        /// <param name="maxLength">字符串的最大长度（包含）</param>
        /// <returns>长度介于 <c>minValue</c> 和 <c>maxValue</c> 之间的随机字符串</returns>
        public static string GetRandomString(int minLength, int maxLength)
        {
            // 负数自动修正为 0
            minLength = Math.Max(minLength, 0);
            maxLength = Math.Max(maxLength, 0);

            // 自动修正参数顺序
            NumberUtil.SwapIfFirstLarger(ref minLength, ref maxLength);

            // 均为 0 时返回空字符串
            if (maxLength == 0 && minLength == 0)
            {
                return "";
            }

            const string defaultCharList = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ`~!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?";

            // 随机字符串的长度
            int len = GetRandomInt(minLength, maxLength + 1);

            // 生成随机字符串
            char[] resultChars = new char[len];
            for (int i = 0; i < len; i++)
            {
                resultChars[i] = defaultCharList[GetRandomInt(0, defaultCharList.Length)];
            }

            return new string(resultChars);
        }

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
            format = StrUtil.ToUpperLetter(format);

            // 合法 GUID 格式符集合
            const string roleString = "DNBPX";

            // 双字符规则：相同且为 DNBPX 中任意一个 → 返回大写 GUID 字符串
            if (format.Length == 2 && format[0] == format[1] && roleString.Contains(format[0].ToString()))
            {
                return StrUtil.ToUpperLetter(Guid.NewGuid().ToString(format[0].ToString()));
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
        /// 从指定集合中随机选取一个元素。
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="items">要从中选取的集合（支持数组、List 等）</param>
        /// <param name="defaultValue">当集合为 null 或空时返回的默认值</param>
        /// <returns>随机选中的元素，或 defaultValue</returns>
        public static T GetRandomItem<T>(IList<T> items, T defaultValue)
        {
            if (items == null || items.Count == 0)
            {
                return defaultValue;
            }

            return items[GetRandomInt(0, items.Count)];
        }
    }
}
