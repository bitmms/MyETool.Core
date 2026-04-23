using System;

namespace ETool.Core.Util
{
    /// <summary>
    /// 校验工具类
    /// </summary>
    public static class ValidatorUtil
    {
        /// <summary>
        /// 校验指定字符串是否符合中国大陆 11 位手机号码的格式规范
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <returns>如果字符串符合中国大陆手机号码的格式规范返回 true，否则返回 false</returns>
        public static bool IsPhoneNumber(string s)
        {
            // 判空
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            // 判断长度
            if (s.Length != 11)
            {
                return false;
            }

            // 第 1 位：必须是数字 1
            if (s[0] != '1')
            {
                return false;
            }

            // 第 2 位：必须是 3-9 的数字
            if (s[1] < '3' || s[1] > '9')
            {
                return false;
            }

            // 后 9 位：必须是 0-9 的数字
            for (var i = 2; i < 11; i++)
            {
                if (!CharUtil.IsDigit(s[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 校验指定字符串是否符合正整数的格式规范
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <returns>如果字符串符合正整数的格式规范返回 true，否则返回 false</returns>
        /// <remarks>
        /// 不支持以 '+' 开头的正整数
        /// </remarks>
        public static bool IsPositiveNumber(string s)
        {
            // 判空
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            // 不允许前导零的存在
            if (s[0] == '0')
            {
                return false;
            }

            // 逐字符检查是否为数字
            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (!CharUtil.IsDigit(s[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 校验指定字符串是否符合整数的格式规范
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <returns>如果字符串符合整数的格式规范返回 true，否则返回 false</returns>
        /// <remarks>
        /// 不支持以 '+' 开头的正整数
        /// </remarks>
        public static bool IsNumber(string s)
        {
            // 判空
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            // 无效的负数
            if (s == "-")
            {
                return false;
            }

            // 单独判断整数 0
            if (s == "0")
            {
                return true;
            }

            // 起始索引
            var startIndex = s[0] != '-' ? 0 : 1;

            // 不允许前导零的存在
            if (s[startIndex] == '0')
            {
                return false;
            }

            // 逐字符检查是否为数字
            var len = s.Length;
            for (var i = startIndex; i < len; i++)
            {
                if (!CharUtil.IsDigit(s[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 校验指定字符串是否符合 QQ 号码的格式规范
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <returns>如果字符串符合 QQ 号码的格式规范返回 true，否则返回 false</returns>
        /// <remarks>
        /// QQ号码的长度是 5-11
        /// </remarks>
        public static bool IsQqNumber(string s)
        {
            // 判空
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            // 长度校验：必须是 5-11 位
            var len = s.Length;
            if (len < 5 || len > 11)
            {
                return false;
            }

            // 首位校验：必须是 1-9 的数字
            if (s[0] < '1' || s[0] > '9')
            {
                return false;
            }

            // 除首位的其他字符校验：必须是 0-9 的数字
            for (var i = 1; i < len; i++)
            {
                if (!CharUtil.IsDigit(s[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 校验指定字符串是否符合 IPv4 地址点分十进制表示法的格式规范
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <returns>如果字符串符合 IPv4 地址点分十进制表示法的格式规范返回 true，否则返回 false</returns>
        public static bool IsIpv4(string s)
        {
            // 判空
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            // 长度校验：最短7位(0.0.0.0)，最长15位(255.255.255.255)
            var len = s.Length;
            if (len < 7 || len > 15)
            {
                return false;
            }

            var dotCount = 0; // 统计 '.' 的个数
            var currentSegmentSum = 0; // 记录当前段落的数值大小
            var currentSegmentLen = 0; // 当前段落中已处理字符的长度【小段落的指针】

            // 双指针算法
            for (var i = 0; i < len; i++)
            {
                // 段落处理
                if (CharUtil.IsDigit(s[i]))
                {
                    // 前导零
                    if (currentSegmentLen > 0 && currentSegmentSum == 0)
                    {
                        return false;
                    }

                    // 单段超过3位数
                    if (++currentSegmentLen > 3)
                    {
                        return false;
                    }

                    // 计算数值
                    currentSegmentSum = currentSegmentSum * 10 + (s[i] - '0');

                    // 数值超过255
                    if (currentSegmentSum > 255)
                    {
                        return false;
                    }
                }
                // 段落结束校验
                else if (s[i] == '.')
                {
                    // 连续的 '.' 或 整个字符串以 '.' 开头
                    if (currentSegmentLen == 0)
                    {
                        return false;
                    }

                    // 超过3个点
                    if (++dotCount > 3)
                    {
                        return false;
                    }

                    currentSegmentSum = 0;
                    currentSegmentLen = 0;
                }
                // 非法字符
                else
                {
                    return false;
                }
            }

            // 不是4段
            if (dotCount != 3)
            {
                return false;
            }

            // 以 '.' 结尾 
            if (currentSegmentLen == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断一个数字是否是质数
        /// </summary>
        /// <param name="number">待判断的数字</param>
        /// <returns>如果是质数，则返回 true，否则返回 false</returns>
        /// <example>
        /// <code>
        /// 定理：除2和3之外的所有质数都可以表示为 6k±1 的形式，其中 k 为正整数
        /// ---------------------------------------------------------------
        ///     6k+0   -->   被6整除
        ///  (*)6n+1   -->   可能是质数
        ///     6n+2   -->   2(3n+1)   -->   被2整除
        ///     6n+3   -->   3(2n+1)   -->   被3整除
        ///     6n+4   -->   2(3n+2)   -->   被2整除   
        ///  (*)6n+5   -->   6(n+1)-1  -->   6n-1     -->  可能是质数
        /// ---------------------------------------------------------------
        /// 所以
        ///     1. 所有大于 3 的质数，只能出现在 6k - 1 或 6k + 1 的序列中
        ///     2. 所有不是 6k±1 的数一定是 2 或 3 的倍数
        /// </code>
        /// </example>
        public static bool IsPrime(int number)
        {
            // 1. 小于 2 的一定不是
            if (number < 2) return false;

            // 2. 小质数快速判断
            int[] smallPrimes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
            foreach (var p in smallPrimes)
            {
                if (number % p == 0) return number == p;
            }

            // 3. 利用 6k±1 的性质判断质数
            for (var i = 5; i <= number / i; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 判断一个数字是否是偶数
        /// </summary>
        /// <param name="number">待判断的数字</param>
        /// <returns>如果是偶数，则返回 true，否则返回 false</returns>
        /// <example>
        /// <code>当 n 可以使用 2k（k 属于任意整数） 表示时，n 是偶数</code>
        /// </example>
        public static bool IsEven(int number) => (number & 1) == 0;

        /// <summary>
        /// 判断一个数字是否是奇数
        /// </summary>
        /// <param name="number">待判断的数字</param>
        /// <returns>如果是奇数，则返回 true，否则返回 false</returns>
        /// <example>
        /// <code>当 n 可以使用 2k+1（k 属于任意整数） 表示时，n 是奇数</code>
        /// </example>
        public static bool IsOdd(int number) => (number & 1) == 1;

        /// <summary>
        /// 判断一个数字是否是完全平方数
        /// </summary>
        /// <param name="number">待判断的数字</param>
        /// <returns>如果是完全平方数，则返回 true，否则返回 false</returns>
        public static bool IsPerfectSquare(int number)
        {
            if (number < 0) return false;

            var sqrt = (int)Math.Sqrt(number);
            return sqrt * sqrt == number;
        }
    }
}
