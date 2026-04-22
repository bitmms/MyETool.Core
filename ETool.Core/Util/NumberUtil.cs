using System;

namespace ETool.Core.Util
{
    /// <summary>
    /// 整数工具类
    /// </summary>
    public static class NumberUtil
    {
        /// <summary>
        /// 正整数比较大小（支持 Offset）
        /// </summary>
        private static int Compare(string n1, int start1, int end1, string n2, int start2, int end2)
        {
            var len1 = end1 - start1 + 1;
            var len2 = end2 - start2 + 1;

            if (len1 > len2) return 1;
            if (len1 < len2) return -1;

            for (var i = 0; i < len1; i++)
            {
                if (n1[i + start1] > n2[i + start2]) return 1;
                if (n1[i + start1] < n2[i + start2]) return -1;
            }

            return 0;
        }

        /// <summary>
        /// 正整数加法（支持 Offset）
        /// </summary>
        private static string AddPositive(string n1, int start1, int end1, string n2, int start2, int end2)
        {
            var len1 = end1 - start1 + 1;
            var len2 = end2 - start2 + 1;

            // 最大长度
            var maxLen = Math.Max(len1, len2);

            // 结果数组
            var result = new char[maxLen + 1];

            // 指针
            var p1 = end1; // 指向 n1 的末尾
            var p2 = end2; // 指向 n2 的末尾
            var writePos = maxLen; // 结果数组从后往前写
            var carry = 0;

            // 模拟竖式加法：只要还有数字没加完 / 还有进位，就继续计算
            while (p1 >= start1 || p2 >= start2 || carry != 0)
            {
                // 取当前位数字，指针越界则取 0
                var a = p1 >= start1 ? n1[p1] - '0' : 0;
                var b = p2 >= start2 ? n2[p2] - '0' : 0;

                // 当前位结果 = 数字1 + 数字2 + 进位
                var sum = a + b + carry;

                // 取个位存入结果数组
                result[writePos] = (char)('0' + sum % 10);

                // 取十位作为新的进位
                carry = sum / 10; // 逻辑上等价于 carry = sum >= 10 ? 1 : 0;

                // 指针左移到高位，结果数组写入位置左移
                p1--;
                p2--;
                writePos--;
            }

            // 构造最终字符串
            return new string(result, writePos + 1, maxLen - writePos);
        }

        /// <summary>
        /// 正整数相减（支持 Offset）【在考虑 offset 之后，确保 n1 >= n2】
        /// </summary>
        private static string SubPositive(string n1, int start1, int end1, string n2, int start2, int end2)
        {
            // 1. 获取两个数字字符串的长度
            var len1 = end1 - start1 + 1;
            var len2 = end2 - start2 + 1;
            var maxLen = len1;

            // 2. 结果数组
            var result = new char[maxLen];

            // 3. 指针
            var pos1 = end1; // 指向 n1 的末尾
            var pos2 = end2; // 指向 n2 的末尾
            var writePos = maxLen - 1; // 结果数组的写入位置（从后往前写），writePos 最终只可能是 0 或 -1
            var carry = 0; // 借位值（0 或 -1）

            // 4. 模拟竖式减法：只要还有数字没减完，就继续计算
            while (pos1 >= start1)
            {
                // 取当前位数字，指针越界则取 0
                var a = n1[pos1] - '0';
                var b = pos2 >= start2 ? n2[pos2] - '0' : 0;

                // 当前位结果 = 数字1 - 数字2 + 进位
                var sum = a - b + carry;

                // 取个位存入结果数组
                result[writePos] = (char)('0' + (sum + 10) % 10);

                // 取十位作为新的进位
                carry = sum >= 0 ? 0 : -1;

                // 指针左移到高位，结果数组写入位置左移
                pos1--;
                pos2--;
                writePos--;
            }

            // 5. 找到第一个非 0 位置
            var pos = 0;
            while (pos < maxLen && result[pos] == '0')
            {
                pos++;
            }

            // 6. 构造最终字符串
            return pos == maxLen ? "0" : new string(result, pos, maxLen - pos);
        }

        /// <summary>
        /// 高精度整数相加
        /// </summary>
        /// <param name="n1">第一个整数</param>
        /// <param name="n2">第二个整数</param>
        /// <returns>整数相加的和</returns>
        /// <remarks>限制入参长度不超过 100000 字符</remarks>
        /// <exception cref="ArgumentException"><c>n1</c> 不是有效的整数格式</exception>
        /// <exception cref="ArgumentException"><c>n2</c> 不是有效的整数格式</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>n1</c> 的长度超过 100000 个字符</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>n2</c> 的长度超过 100000 个字符</exception>
        public static string Add(string n1, string n2)
        {
            const int maxLen = 10_0000;

            if (n1 == null) throw new ArgumentNullException(nameof(n1));
            if (n2 == null) throw new ArgumentNullException(nameof(n2));
            if (n1.Length > maxLen) throw new ArgumentOutOfRangeException(nameof(n1), $"'{nameof(n1)}' 的长度不能超过 {maxLen} 个字符。");
            if (n2.Length > maxLen) throw new ArgumentOutOfRangeException(nameof(n2), $"'{nameof(n2)}' 的长度不能超过 {maxLen} 个字符。");
            if (!ValidatorUtil.IsValidNumber(n1)) throw new ArgumentException($"'{nameof(n1)}' 不是有效的整数格式", nameof(n1));
            if (!ValidatorUtil.IsValidNumber(n2)) throw new ArgumentException($"'{nameof(n2)}' 不是有效的整数格式", nameof(n2));

            if (n1 == "0") return n2;
            if (n2 == "0") return n1;

            // 1. n1>0, n2>0 → 直接相加
            if (n1[0] != '-' && n2[0] != '-')
            {
                return AddPositive(n1, 0, n1.Length - 1, n2, 0, n2.Length - 1);
            }

            // 2. n1>0, n2<0 → 绝对值相减
            if (n1[0] != '-' && n2[0] == '-')
            {
                var compareResult = Compare(n1, 0, n1.Length - 1, n2, 1, n2.Length - 1);
                return compareResult switch
                {
                    1 => SubPositive(n1, 0, n1.Length - 1, n2, 1, n2.Length - 1),
                    -1 => "-" + SubPositive(n2, 1, n2.Length - 1, n1, 0, n1.Length - 1),
                    _ => "0"
                };
            }

            // 3. n1<0, n2>0 → 绝对值相减
            if (n1[0] == '-' && n2[0] != '-')
            {
                var compareResult = Compare(n1, 1, n1.Length - 1, n2, 0, n2.Length - 1);
                return compareResult switch
                {
                    1 => "-" + SubPositive(n1, 1, n1.Length - 1, n2, 0, n2.Length - 1),
                    -1 => SubPositive(n2, 0, n2.Length - 1, n1, 1, n1.Length - 1),
                    _ => "0"
                };
            }

            // 4. n1<0, n2<0
            return "-" + AddPositive(n1, 1, n1.Length - 1, n2, 1, n2.Length - 1);
        }

        /// <summary>
        /// 高精度整数相减 n1-n2
        /// </summary>
        /// <param name="n1">第一个整数</param>
        /// <param name="n2">第二个整数</param>
        /// <returns>n1-n2 的值</returns>
        /// <remarks>限制入参长度不超过 100000 字符</remarks>
        /// <exception cref="ArgumentException"><c>n1</c> 不是有效的整数格式</exception>
        /// <exception cref="ArgumentException"><c>n2</c> 不是有效的整数格式</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>n1</c> 的长度超过 100000 个字符</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>n2</c> 的长度超过 100000 个字符</exception>
        public static string Sub(string n1, string n2)
        {
            const int maxLen = 10_0000;

            if (n1 == null) throw new ArgumentNullException(nameof(n1));
            if (n2 == null) throw new ArgumentNullException(nameof(n2));
            if (n1.Length > maxLen) throw new ArgumentOutOfRangeException(nameof(n1), $"'{nameof(n1)}' 的长度不能超过 {maxLen} 个字符。");
            if (n2.Length > maxLen) throw new ArgumentOutOfRangeException(nameof(n2), $"'{nameof(n2)}' 的长度不能超过 {maxLen} 个字符。");
            if (!ValidatorUtil.IsValidNumber(n1)) throw new ArgumentException($"'{nameof(n1)}' 不是有效的整数格式", nameof(n1));
            if (!ValidatorUtil.IsValidNumber(n2)) throw new ArgumentException($"'{nameof(n2)}' 不是有效的整数格式", nameof(n2));

            /*if (n1 == "0")
            {
                return n2[0] == '-' ? n2.Substring(1) : "-" + n2;
            }*/
            if (n2 == "0") return n1;
            if (n1.Equals(n2)) return "0";

            // 1. n1>0, n2>0 → 直接相加
            if (n1[0] != '-' && n2[0] != '-')
            {
                var compareResult1 = Compare(n1, 0, n1.Length - 1, n2, 0, n2.Length - 1);
                return compareResult1 switch
                {
                    1 => SubPositive(n1, 0, n1.Length - 1, n2, 0, n2.Length - 1),
                    _ => "-" + SubPositive(n2, 0, n2.Length - 1, n1, 0, n1.Length - 1),
                };
            }

            // 2. n1>0, n2<0 → 绝对值相减
            if (n1[0] != '-' && n2[0] == '-')
            {
                return AddPositive(n1, 0, n1.Length - 1, n2, 1, n2.Length - 1);
            }

            // 3. n1<0, n2>0 → 绝对值相减
            if (n1[0] == '-' && n2[0] != '-')
            {
                return "-" + AddPositive(n1, 1, n1.Length - 1, n2, 0, n2.Length - 1);
            }

            // 4. n1<0, n2<0
            var compareResult2 = Compare(n1, 1, n1.Length - 1, n2, 1, n2.Length - 1);
            return compareResult2 switch
            {
                1 => "-" + SubPositive(n1, 1, n1.Length - 1, n2, 1, n2.Length - 1),
                _ => SubPositive(n2, 1, n2.Length - 1, n1, 1, n1.Length - 1),
            };
        }
    }
}
