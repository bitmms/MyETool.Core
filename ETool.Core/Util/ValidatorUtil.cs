using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;

namespace ETool.Core.Util
{
    /// <summary>
    /// 校验工具类
    /// </summary>
    public static class ValidatorUtil
    {
        /// <summary>
        /// 判断一个字符串是否符合中国大陆 11 位手机号码的格式规范
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
        /// 判断一个字符串是否符合正整数的格式规范
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
        /// 判断一个字符串是否符合网络邮箱的格式规范【规则比较宽容的版本】
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <returns>如果字符串符合网络邮箱的格式规范返回 true，否则返回 false</returns>
        /// <remarks>
        /// <code>
        /// 邮箱格式规则总结
        ///  - 结构组成是：[用户名] @ [域名]
        ///  - 总长度不超过 254 个字符
        ///  - 只可以由 a-z, A-Z, 0-9 以及特殊符号 @ . _ % + - 组成
        ///  - 特殊符号不能在用户名和域名的开头、结尾
        ///  - 有且仅有一个 '@'
        ///  - 用户名和域名不可为空
        /// </code>
        /// </remarks>
        public static bool IsEmail(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;

            // 判空
            // 总长度不超过 254 个字符
            var len = s.Length;
            if (len > 254) return false;

            // 只可以由 a-z, A-Z, 0-9 以及特殊符号 . _ % + - 组成
            var chars = new[] { '.', '_', '%', '+', '-', '@' };
            for (var i = 0; i < len; i++)
            {
                var c = s[i];
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || Array.IndexOf(chars, c) >= 0))
                {
                    return false;
                }
            }

            // 分割
            var segments = s.Split('@');

            // 有且仅有一个 '@'
            if (segments.Length != 2) return false;

            var username = segments[0];
            var domain = segments[1];

            // 用户名和域名不可为空
            if (username.Length <= 0 || domain.Length <= 0) return false;

            // 特殊符号不能在用户名的开头、结尾
            if (Array.IndexOf(chars, username[0]) >= 0 || Array.IndexOf(chars, username[username.Length - 1]) >= 0) return false;

            // 特殊符号不能在域名的开头、结尾
            if (Array.IndexOf(chars, domain[0]) >= 0 || Array.IndexOf(chars, domain[domain.Length - 1]) >= 0) return false;

            // 全部校验通过
            return true;
        }

        /// <summary>
        /// 判断一个字符串是否符合整数的格式规范
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
        /// 判断一个字符串是否符合 QQ 号码的格式规范
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
        /// 判断一个字符串是否符合 IPv4 地址点分十进制表示法的格式规范
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
        /// 判断一个字符串是否符合 私有IPv4 地址点分十进制表示法的格式规范
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <returns>如果字符串符合 私有IPv4 地址点分十进制表示法的格式规范返回 true，否则返回 false</returns>
        /// <remarks>
        /// <code>
        /// 私有 IPv4 地址
        ///     1. 10.0.0.0 – 10.255.255.255
        ///     2. 172.16.0.0 – 172.31.255.255
        ///     3. 192.168.0.0 – 192.168.255.255
        /// </code>
        /// </remarks>
        public static bool IsPrivateIpv4(string s)
        {
            // 先校验是否为合法 IPv4 格式
            if (!IsIpv4(s)) return false;

            // 分割为4个分段
            var segments = s.Split('.');

            // 解析前两段用于私有地址判断
            var part1 = byte.Parse(segments[0]);
            var part2 = byte.Parse(segments[1]);

            // 私有地址三段规则
            return part1 == 10
                   || (part1 == 172 && part2 >= 16 && part2 <= 31)
                   || (part1 == 192 && part2 == 168);
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

        /// <summary>
        /// 判断一个字符串是否完全由数字字符（0-9）组成
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串每一个字符都是数字字符，则返回 true，否则返回 false</returns>
        public static bool IsAllDigitChar(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (!CharUtil.IsDigit(s[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// 判断一个字符串是否完全由英文字符（a-zA-Z）组成
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串每一个字符都是英文字符，则返回 true，否则返回 false</returns>
        public static bool IsAllLetterChar(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (!CharUtil.IsLetter(s[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// 判断一个字符串是否完全由小写英文字符（a-z）组成
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串每一个字符都是小写英文字符，则返回 true，否则返回 false</returns>
        public static bool IsAllLowerLetterChar(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (!CharUtil.IsLowerLetter(s[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// 判断一个字符串是否完全由大写英文字符（A-Z）组成
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串每一个字符都是大写英文字符，则返回 true，否则返回 false</returns>
        public static bool IsAllUpperLetterChar(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (!CharUtil.IsUpperLetter(s[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// 判断一个字符串是否完全由 ASCII 可见的可打印字符组成
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>仅包含 ASCII 可打印可见字符返回 true，否则返回 false</returns>
        /// <remarks>ASCII 编码的范围是 [0-127]，其中 [0-31] 和 127 是不可见的控制字符，[32-126] 是可见的可打印字符</remarks>
        /// 
        public static bool IsAllAsciiPrintable(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (s[i] <= 31 || s[i] >= 127)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断一个字符串是否符合中国大陆身份证号码的格式规范【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>如果字符串符合中国大陆身份证号码的格式规范返回 true，否则返回 false</returns>
        /// <remarks>只校验格式的合法性，格式正确并不等价于身份真实</remarks>
        public static bool IsChinaIdCard(string s, bool ignoreCase = true)
        {
            // 判空
            if (string.IsNullOrWhiteSpace(s)) return false;

            // 长度只能是 15、18 二者之一
            var len = s.Length;
            if (len != 15 && len != 18) return false;

            // 省份编码校验
            var mainland = new HashSet<string>
            {
                "11", "12", "13", "14", "15", // 华北
                "21", "22", "23", // 东北
                "31", "32", "33", "34", "35", "36", "37", // 华东
                "41", "42", "43", "44", "45", "46", // 华南
                "50", "51", "52", "53", "54", // 西南
                "61", "62", "63", "64", "65", // 西北
            };
            var all = new HashSet<string>(mainland)
            {
                "71", // 台湾
                "81", // 香港
                "82", // 澳门
            };
            var provinceCode = s.Substring(0, 2);
            if (len == 15 && !mainland.Contains(provinceCode)) return false;
            if (len == 18 && !all.Contains(provinceCode)) return false;

            // 只可以是数字
            var digitLastIndex = len == 15 ? len : len - 1;
            for (var i = 0; i < digitLastIndex; i++)
            {
                if (!CharUtil.IsDigit(s[i])) return false;
            }

            // 第18位字符只可以是 "xX0-9"
            if (len == 18 && ignoreCase && !CharUtil.IsDigit(s[17]) && s[17] != 'X' && s[17] != 'x') return false;
            if (len == 18 && !ignoreCase && !CharUtil.IsDigit(s[17]) && s[17] != 'X') return false;

            // 出生日期格式校验
            var birthStr = len == 15 ? "19" + s.Substring(6, 6) : s.Substring(6, 8);
            if (!DateTime.TryParseExact(birthStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var datetime)) return false;

            // 出生日期范围校验：1900-01-01 往后
            if (datetime < new DateTime(1900, 1, 1)) return false;

            // 此时的 15 位身份证一定合法
            if (len == 15) return true;

            // 校验码计算
            // 加权因子与校验码映射表
            int[] checkWeights = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            char[] checkCodes = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += (s[i] - '0') * checkWeights[i];
            }

            return checkCodes[sum % 11] == CharUtil.ToUpperLetter(s[17]);
        }

        /// <summary>
        /// 判断一个字符串是否包含指定字符
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <param name="c">目标字符</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>如果包含指定字符则返回 true，否则返回 false</returns>
        public static bool IsContainsChar(string s, char c, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(s)) return false;

            if (ignoreCase)
            {
                s = StrUtil.ToUpperLetter(s);
                c = CharUtil.ToUpperLetter(c);
            }

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (s[i] == c) return true;
            }

            return false;
        }

        /// <summary>
        /// 判断一个字符串是否包含指定子串
        /// </summary>
        /// <param name="sourceString">待判断的字符串</param>
        /// <param name="targetString">目标子串</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>如果包含指定子串则返回 true，否则返回 false</returns>
        public static bool IsContainsString(string sourceString, string targetString, bool ignoreCase = false)
        {
            // 空值校验
            if (string.IsNullOrEmpty(sourceString)) return false;
            if (string.IsNullOrEmpty(targetString)) return false;

            // 长度
            var sourceLen = sourceString.Length;
            var targetLen = targetString.Length;

            // 空串默认包含
            if (targetLen == 0) return true;

            // 子串比源字符串长 → 直接不匹配
            if (sourceLen < targetLen) return false;

            // 开启忽略大小写 → 统一转换（仅英文转大写）
            sourceString = ignoreCase ? StrUtil.ToUpperLetter(sourceString) : sourceString;
            targetString = ignoreCase ? StrUtil.ToUpperLetter(targetString) : targetString;

            // 匹配子串
            for (var i = 0; i < sourceLen - targetLen + 1; i++)
            {
                var match = true;
                for (var j = 0; j < targetLen; j++)
                {
                    if (sourceString[i + j] != targetString[j])
                    {
                        match = false;
                        break;
                    }
                }

                if (match) return true;
            }

            return false;
        }

        /// <summary>
        /// 判断一个字符串是否包含数字字符
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果包含数字字符则返回 true，否则返回 false</returns>
        public static bool IsContainsDigit(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (s[i] >= '0' && s[i] <= '9')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断一个字符串是否包含英文字符
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果包含英文字符则返回 true，否则返回 false</returns>
        public static bool IsContainsLetter(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (s[i] >= 'a' && s[i] <= 'z' || s[i] >= 'A' && s[i] <= 'Z')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断一个字符串是否包含小写英文字符
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果包含小写英文字符则返回 true，否则返回 false</returns>
        public static bool IsContainsLowerLetter(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (s[i] >= 'a' && s[i] <= 'z')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断一个字符串是否包含大写英文字符
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果包含大写英文字符则返回 true，否则返回 false</returns>
        public static bool IsContainsUpperLetter(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (s[i] >= 'A' && s[i] <= 'Z')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断一个字符串是否包含 ASCII 编码中的控制字符
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果包含 ASCII 编码中的控制字符则返回 true，否则返回 false</returns>
        public static bool IsContainsAsciiControl(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (s[i] >= 0 && s[i] <= 31 || s[i] == 127)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断一个字符串是否以指定子串开头
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <param name="prefix">待检查的前缀子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>如果字符串以指定子串开头则返回 true，否则返回 false</returns>
        public static bool IsStartsWith(string s, string prefix, bool ignoreCase = false)
        {
            if (s == null || prefix == null)
            {
                return false;
            }

            if (prefix == string.Empty)
            {
                return true;
            }

            if (s.Length < prefix.Length)
            {
                return false;
            }

            for (var i = 0; i < prefix.Length; i++)
            {
                var sourceChar = s[i];
                var targetChar = prefix[i];

                if (ignoreCase)
                {
                    sourceChar = CharUtil.ToUpperLetter(sourceChar);
                    targetChar = CharUtil.ToUpperLetter(targetChar);
                }

                if (sourceChar != targetChar)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断一个字符串是否以指定子串结束
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <param name="suffix">待检查的后缀子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>如果字符串以指定子串结束则返回 true，否则返回 false</returns>
        public static bool IsEndsWith(string s, string suffix, bool ignoreCase = false)
        {
            if (s == null || suffix == null)
            {
                return false;
            }

            if (suffix == string.Empty)
            {
                return true;
            }

            if (s.Length < suffix.Length)
            {
                return false;
            }

            var idx = 0;
            for (var i = s.Length - suffix.Length; i < s.Length; i++)
            {
                var sourceChar = s[i];
                var targetChar = suffix[idx++];

                if (ignoreCase)
                {
                    sourceChar = CharUtil.ToUpperLetter(sourceChar);
                    targetChar = CharUtil.ToUpperLetter(targetChar);
                }

                if (sourceChar != targetChar)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断一个字符串是否为指定格式的日期字符串
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <param name="formats">日期格式字符串，可传入多个</param>
        /// <returns>如果字符串是指定格式的日期字符串则返回 true，否则返回 false</returns>
        public static bool IsDateTimeString(string s, params string[] formats)
        {
            if (string.IsNullOrEmpty(s) || formats == null || formats.Length == 0) return false;
            return DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}
