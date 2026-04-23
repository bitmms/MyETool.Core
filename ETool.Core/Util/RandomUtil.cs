using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace ETool.Core.Util
{
    /// <summary>
    /// 随机生成工具类
    /// </summary>
    public static class RandomUtil
    {
        /// <summary>
        /// 每一个线程本地存储的 Random 实例【保证多线程环境下随机数生成的线程安全与随机性】
        /// </summary>
        private static readonly ThreadLocal<Random> RandomThreadLocal = new ThreadLocal<Random>(() =>
        {
            // 1. 创建一个 4 字节的数组
            byte[] buffer = new byte[4];
            // 2. 创建系统级的随机数生成器
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            // 3. 生成 4 个 0-255 的随机数，并填充到 buffer 数组里
            rng.GetBytes(buffer);
            // 4. 把 4 字节的数组转换成 int 类型的种子
            var seed = BitConverter.ToInt32(buffer, 0);
            // 5. 使用种子创建 Random 对象
            var random = new Random(seed);
            // 6. 返回 Random 对象
            return random;
        });

        /// <summary>
        /// 获取一个线程安全的 Random 实例
        /// </summary>
        /// <returns>线程安全的 Random 实例</returns>
        private static Random GetRandom()
        {
            return RandomThreadLocal.Value;
        }

        /// <summary>
        /// 获取一个指定区间内的随机整数
        /// </summary>
        /// <param name="minValue">随机数的下限（包含）</param>
        /// <param name="maxValue">随机数的上限（不包含）</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>一个随机整数</returns>
        /// <exception cref="ArgumentOutOfRangeException">当 minValue > maxValue 时抛出</exception>
        public static int GetRandomInt(int minValue, int maxValue, Random random = null)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue), "最小值不能大于最大值");
            }

            if (minValue == maxValue)
            {
                return minValue;
            }

            random ??= GetRandom();
            return random.Next(minValue, maxValue);
        }

        /// <summary>
        /// 获取一个指定范围内的随机双精度浮点数
        /// </summary>
        /// <param name="minValue">随机浮点数的下限（包含）</param>
        /// <param name="maxValue">随机浮点数的上限（不包含）</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机双精度浮点数</returns>
        public static double GetRandomDouble(double minValue, double maxValue, Random random = null)
        {
            // 拒绝 NaN
            if (double.IsNaN(minValue))
            {
                throw new ArgumentException("minValue 不能为 NaN", nameof(minValue));
            }

            if (double.IsNaN(maxValue))
            {
                throw new ArgumentException("maxValue 不能为 NaN", nameof(maxValue));
            }

            // 拒绝 Infinity
            if (double.IsInfinity(minValue))
            {
                throw new ArgumentException("minValue 不能为无穷大", nameof(minValue));
            }

            if (double.IsInfinity(maxValue))
            {
                throw new ArgumentException("maxValue 不能为无穷大", nameof(maxValue));
            }

            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue 不能大于 maxValue");
            }

            if (minValue.Equals(maxValue))
            {
                return minValue;
            }

            random ??= GetRandom();

            // 生成 [0,1)
            var raw = random.NextDouble();

            // 缩放到区间：利用当 minValue == maxValue 时，(maxValue - minValue) 为 0，自然返回 minValue
            return minValue + (maxValue - minValue) * raw;
        }

        /// <summary>
        /// 从起始数字开始，连续 count 个整数中随机选取一个
        /// </summary>
        /// <param name="startInt">起始数字（包含）</param>
        /// <param name="count">要选取的数字总个数</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机选中的整数</returns>
        /// <exception cref="ArgumentOutOfRangeException">count 必须大于 0</exception>
        public static int GetRandomIntFromRange(int startInt, int count, Random random = null)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "数字个数必须大于 0");
            }

            // 检查是否会发生整数溢出：确保 startInt + count 不超过 int.MaxValue
            if (startInt > int.MaxValue - count)
            {
                throw new ArgumentOutOfRangeException(nameof(startInt), "startInt + count 数值超出 int 范围，导致整数溢出");
            }

            random ??= GetRandom();
            return random.Next(startInt, startInt + count); // startInt + count - 1 + 1
        }

        /// <summary>
        /// 获取一个随机的布尔值
        /// </summary>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机的布尔值</returns>
        public static bool GetRandomBool(Random random = null)
        {
            random ??= GetRandom();
            return random.Next(2) == 0;
        }

        /// <summary>
        /// 获取一个随机的数字字符
        /// </summary>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机的数字字符</returns>
        public static char GetRandomDigitChar(Random random = null)
        {
            random ??= GetRandom();
            return (char)('0' + random.Next(10));
        }

        /// <summary>
        /// 获取一个随机的英文字符
        /// </summary>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机的英文字符</returns>
        public static char GetRandomLetterChar(Random random = null)
        {
            random ??= GetRandom();

            // 总共 26*2 = 52 个字母
            var randomIndex = random.Next(52);

            // 前 26 个[0, 25]：小写 a-z
            if (randomIndex < 26)
            {
                return (char)('a' + randomIndex);
            }

            // 后 26 个[26, 51]：大写 A-Z
            return (char)('A' + (randomIndex - 26));
        }

        /// <summary>
        /// 获取一个指定格式的 Guid 字符串
        /// </summary>
        /// <param name="format">格式参数</param>
        /// <returns>指定格式的 Guid 字符串</returns>
        /// <example>
        /// <code>
        ///     GetGuidString("n");  // 48a204b40e254e4883da8febf5f5c036
        ///     GetGuidString("N");  // 48a204b40e254e4883da8febf5f5c036
        ///     GetGuidString("nn"); // CF048754CF0342A0A75DB1F141A69880
        ///     GetGuidString("NN"); // CF048754CF0342A0A75DB1F141A69880
        ///     ------------------------------------------------------------
        ///     GetGuidString("d");  // d1cc94c4-ac65-462c-8ef4-c3efa0ae1ecc
        ///     GetGuidString("D");  // d1cc94c4-ac65-462c-8ef4-c3efa0ae1ecc
        ///     GetGuidString("dd"); // D3EBEE1C-6526-4C16-882B-9D4272971E6C
        ///     GetGuidString("DD"); // D3EBEE1C-6526-4C16-882B-9D4272971E6C
        /// </code>
        /// </example>
        public static string GetGuidString(string format = "DD")
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            format = StrUtil.ToUpperLetter(format);

            return format.Trim().ToUpperInvariant() switch
            {
                "D" => Guid.NewGuid().ToString("D"),
                "DD" => Guid.NewGuid().ToString("D").ToUpperInvariant(),
                "N" => Guid.NewGuid().ToString("N"),
                "NN" => Guid.NewGuid().ToString("N").ToUpperInvariant(),
                _ => throw new ArgumentException("仅支持 D、N、DD、NN", nameof(format))
            };
        }

        /// <summary>
        /// 从指定可枚举集合中随机选取一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="items">要从中选取的可枚举集合</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机选中的元素</returns>
        /// <remarks>针对无法随机访问的集合采用 "水塘抽样算法" 进行等概率的随机选取</remarks>
        public static T GetRandomItem<T>(IEnumerable<T> items, Random random = null)
        {
            // 如果集合为空，直接返回默认值
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            random ??= GetRandom();

            // 支持快速索引的集合（数组、List）直接取
            if (items is IReadOnlyList<T> list)
            {
                if (list.Count == 0)
                {
                    throw new InvalidOperationException("集合不能为空");
                }

                return list[random.Next(list.Count)];
            }

            // 水塘抽样算法【只遍历 1 次，适合无法随机访问的集合（HashSet、IEnumerable等）】
            var count = 0;
            T result = default;
            foreach (var item in items)
            {
                count++; // 当前遍历到第 count 个元素
                if (random.Next(count) == 0) // 从 [0, count-1] 随机选取 1 个元素，只有 1/(count) 的概率选中数字 0，也即只有 1/(count) 的概率选中该元素
                {
                    result = item; // 即使当前被选中，并不能保证不会被后续元素替换，这里不可以提前返回退出
                }
            }

            if (count == 0)
            {
                throw new InvalidOperationException("集合不能为空");
            }

            return result;
        }

        /// <summary>
        /// 从指定可枚举集合中随机选取 k 个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="items">要从中选取的可枚举集合</param>
        /// <param name="k">要选取的元素数量</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机选中的 k 个元素</returns>
        /// <remarks>针对无法随机访问的集合采用 "水塘抽样算法" 进行等概率的随机选取</remarks>
        public static List<T> GetRandomItems<T>(IEnumerable<T> items, int k, Random random = null)
        {
            // 1. 空值校验
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            // 2. 选取数量必须大于等于 0
            if (k < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(k), "选取数量必须大于等于 0");
            }

            // 3. 选取数量等于 0
            if (k == 0)
            {
                return new List<T>();
            }

            // 4. 水塘抽样算法
            random ??= GetRandom();
            var result = new List<T>(k); // 水塘：用来存放最终选中的 k 个元素
            var count = 0;
            foreach (var item in items)
            {
                count++;
                if (count <= k) // 前 n 个元素
                {
                    result.Add(item); // 直接全部放入蓄水池
                }
                else // 第 n+1 个元素开始
                {
                    var randomIndex = random.Next(count); // 生成 [0, count-1] 随机数
                    if (randomIndex < k) // 如果随机数 < n 则替换蓄水池里的一个位置
                    {
                        result[randomIndex] = item;
                    }
                }
            }

            if (count == 0)
            {
                throw new InvalidOperationException("集合不能为空");
            }

            return result;
        }

        /// <summary>
        /// 从指定集合中随机选取 k 个元素（放回抽样）
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="items">数据源</param>
        /// <param name="k">抽取次数</param>
        /// <param name="random">可选随机数生成器</param>
        /// <returns>包含 k 个元素的列表（允许重复）</returns>
        public static List<T> GetRandomItemsWithReplacement<T>(IEnumerable<T> items, int k, Random random = null)
        {
            // 1. 空值校验
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            // 2. 选取数量必须大于等于 0
            if (k < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(k), "选取数量必须大于等于 0");
            }

            // 3. 选取数量等于 0
            if (k == 0)
            {
                return new List<T>();
            }

            random ??= GetRandom();

            IReadOnlyList<T> itemList = items as IReadOnlyList<T> ?? items.ToList();

            if (itemList.Count == 0)
            {
                throw new InvalidOperationException("集合不能为空");
            }

            // 执行放回抽样
            var randomItems = new List<T>(k);
            for (var i = 0; i < k; i++)
            {
                randomItems.Add(itemList[random.Next(itemList.Count)]);
            }

            return randomItems;
        }

        /// <summary>
        /// 使用 Fisher–Yates 洗牌算法对列表进行原地随机打乱
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">要打乱的列表</param>
        /// <param name="random">可选的随机数生成器</param>
        public static void Shuffle<T>(IList<T> list, Random random = null)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Count <= 1)
            {
                return;
            }

            random ??= GetRandom();
            var count = list.Count;
            for (var i = count - 1; i > 0; i--)
            {
                // 生成 [0, i] 之间的随机索引
                var randomIndex = random.Next(i + 1);

                // 交换当前位置和随机位置的元素
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="charset">字符集</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机字符串</returns>
        public static string GetRandomStringFromCharset(int length, string charset, Random random = null)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "字符串长度必须大于等于 0");
            }

            if (charset == null)
            {
                throw new ArgumentNullException(nameof(charset));
            }

            var charCount = charset.Length;
            if (charCount == 0)
            {
                throw new ArgumentException("字符集长度不能为 0", nameof(charset));
            }

            random ??= GetRandom();

            var sb = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                sb.Append(charset[random.Next(charCount)]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 生成指定长度的随机数字字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机数字字符串</returns>
        public static string GetRandomStringFromNumber(int length, Random random = null) => GetRandomStringFromCharset(length, "0123456789", random);

        /// <summary>
        /// 生成指定长度的随机字母字符串
        /// </summary>
        /// <param name="length">生成的随机字母字符串的长度</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>生成的随机字母字符串</returns>
        public static string GetRandomStringFromLetter(int length, Random random = null) => GetRandomStringFromCharset(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", random);

        /// <summary>
        /// 生成指定长度的随机字母数字字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="random">可选的随机数生成器</param>
        /// <returns>随机字母数字字符串</returns>
        public static string GetRandomStringFromNumberAndLetter(int length, Random random = null) => GetRandomStringFromCharset(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", random);
    }
}
