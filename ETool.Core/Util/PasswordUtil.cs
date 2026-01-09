using System;
using System.Collections.Generic;

namespace ETool.Core.Util
{
    /// <summary>
    /// 密码复杂度等级枚举
    /// </summary>
    /// <remarks>
    /// 密码需要满足的要求有：
    /// <para>1. 非空、非NULL、仅包含数字、字母或允许的特殊字符（见 AllowedSpecialChars），且长度合规 [min, max]</para>
    /// <para>2. 至少包含有1个特殊符号</para>
    /// <para>3. 至少包含有1个数字</para>
    /// <para>4. 至少包含有1个大写字母</para>
    /// <para>5. 至少包含有1个小写字母</para>
    /// <para>6. 通过弱口令检测（包括字典弱口令、连续序列、全重复等）</para>
    /// </remarks>
    public enum PasswordStrength
    {
        /// <summary>
        /// 无效，不满足 [1]
        /// </summary>
        Invalid,

        /// <summary>
        /// 极弱，满足 [1] 且 [2][3][4][5] 仅满足 1 项，不考虑 [6]
        /// </summary>
        Weak,

        /// <summary>
        /// 较弱，满足 [1] 且 [2][3][4][5] 仅满足 2 项，不考虑 [6]
        /// </summary>
        Weakish,

        /// <summary>
        /// 中等，满足 [1] 且 [2][3][4][5] 仅满足 3 项，不考虑 [6]
        /// </summary>
        Medium,

        /// <summary>
        /// 较高，满足 [1][2][3][4][5] 但不满足 [6]
        /// </summary>
        Strong,

        /// <summary>
        /// 极高，满足 [1][2][3][4][5][6]
        /// </summary>
        VeryStrong
    }

    /// <summary>
    /// 密码工具类
    /// </summary>
    public static class PasswordUtil
    {
        #region 密码复杂度等级检测

        /// <summary>
        /// 密码允许的最小长度
        /// </summary>
        private const int MinPasswordLength = 8;

        /// <summary>
        /// 密码允许的最大长度
        /// </summary>
        private const int MaxPasswordLength = 32;

        /// <summary>
        /// 弱口令检测中，连续相同字符的最大允许长度，超过即为弱口令
        /// </summary>
        private const int MaxAllowedRepeatedCharCount = 3;

        /// <summary>
        /// 弱口令检测中，连续递增或递减字符序列的最大允许长度，超过即为弱口令
        /// </summary>
        private const int MaxAllowedSequentialCharCount = 4;

        /// <summary>
        /// 允许的特殊字符
        /// </summary>
        private static readonly HashSet<char> AllowedSpecialChars = new HashSet<char> { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', '.' };

        /// <summary>
        /// 常见的弱口令
        /// </summary>
        private static readonly HashSet<string> CommonPasswords = new HashSet<string>()
        {
            "012345", "123456", "234567", "345678", "456789", "567890",
            "root", "admin",
            "qwerty", "wertyu", "ertyui", "rtyuio", "tyuiop",
            "asdfgh", "sdfghj", "dfghjk", "fghjkl",
            "zxcvbn", "xcvbnm",
            "password"
        };

        /// <summary>
        /// 判断密码字符串是否通过弱口令检测
        /// </summary>
        /// <param name="password">待检测密码</param>
        /// <returns>true 表示通过检测，false 表示没有通过检测</returns>
        private static bool PassesWeakPasswordCheck(string password)
        {
            // 空或 null 视为弱口令
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // 包含弱口令
            if (CommonPasswords.Contains(password.ToLowerInvariant()))
            {
                return false;
            }

            int repeatCount = 1, decCount = 1, incCount = 1;
            for (var i = 1; i < password.Length; i++)
            {
                // 重复字符
                if (password[i] == password[i - 1])
                {
                    repeatCount++;
                    if (repeatCount > MaxAllowedRepeatedCharCount)
                    {
                        return false;
                    }
                }
                else
                {
                    repeatCount = 1;
                }

                // 连续递增字符：数字、大写字母、小写字母
                if (password[i] == password[i - 1] + 1)
                {
                    var bothDigits = password[i] >= '0' && password[i] <= '9' && password[i - 1] >= '0' && password[i - 1] <= '9';
                    var bothLower = password[i] >= 'a' && password[i] <= 'z' && password[i - 1] >= 'a' && password[i - 1] <= 'z';
                    var bothUpper = password[i] >= 'A' && password[i] <= 'Z' && password[i - 1] >= 'A' && password[i - 1] <= 'Z';
                    if (bothDigits || bothLower || bothUpper)
                    {
                        incCount++;
                        if (incCount > MaxAllowedSequentialCharCount)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    incCount = 1;
                }

                // 连续递减字符：数字、大写字母、小写字母
                if (password[i] == password[i - 1] - 1)
                {
                    var bothDigits = password[i] >= '0' && password[i] <= '9' && password[i - 1] >= '0' && password[i - 1] <= '9';
                    var bothLower = password[i] >= 'a' && password[i] <= 'z' && password[i - 1] >= 'a' && password[i - 1] <= 'z';
                    var bothUpper = password[i] >= 'A' && password[i] <= 'Z' && password[i - 1] >= 'A' && password[i - 1] <= 'Z';
                    if (bothDigits || bothLower || bothUpper)
                    {
                        decCount++;
                        if (decCount > MaxAllowedSequentialCharCount)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    decCount = 1;
                }
            }

            // 通过
            return true;
        }

        /// <summary>
        /// 检测密码的复杂度等级
        /// </summary>
        /// <param name="password">待检测的密码字符串</param>
        /// <param name="minLen">密码允许的最小长度，默认为 8</param>
        /// <param name="maxLen">密码允许的最大长度，默认为 32</param>
        /// <param name="additionalSpecialChars">可选。额外允许的特殊字符集合，将合并到默认允许列表中</param>
        /// <returns>表示密码复杂度的 <see cref="PasswordStrength"/> 枚举值</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minLen</c> 小于等于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>maxLen</c> 小于等于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>maxLen</c> 小于等于 <c>minLen</c></exception>
        public static PasswordStrength CheckPasswordStrength(string password, int minLen = MinPasswordLength, int maxLen = MaxPasswordLength, HashSet<char> additionalSpecialChars = null)
        {
            if (minLen <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(minLen),
                    minLen,
                    $"最小长度必须大于 0，实际值：{minLen}");
            }

            if (maxLen <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxLen),
                    maxLen,
                    $"最大长度必须大于 0，实际值：{maxLen}");
            }

            if (maxLen <= minLen)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxLen),
                    maxLen,
                    $"最大长度必须大于最小长度 ({minLen})，实际值：{maxLen}");
            }

            // 空值
            if (string.IsNullOrEmpty(password))
            {
                return PasswordStrength.Invalid;
            }

            // 长度
            if (password.Length < minLen || password.Length > maxLen)
            {
                return PasswordStrength.Invalid;
            }

            // 构建本次校验使用的特殊字符集
            HashSet<char> allowedSpecialChars;
            if (additionalSpecialChars == null || additionalSpecialChars.Count == 0)
            {
                allowedSpecialChars = AllowedSpecialChars;
            }
            else
            {
                allowedSpecialChars = new HashSet<char>(AllowedSpecialChars);
                allowedSpecialChars.UnionWith(additionalSpecialChars);
            }

            // 检查字符
            bool hasSpecial = false, hasDigit = false, hasUpper = false, hasLower = false;
            foreach (var c in password)
            {
                if (c >= '0' && c <= '9') hasDigit = true;
                else if (c >= 'A' && c <= 'Z') hasUpper = true;
                else if (c >= 'a' && c <= 'z') hasLower = true;
                else if (allowedSpecialChars.Contains(c)) hasSpecial = true;
                else return PasswordStrength.Invalid;
            }

            var count = (hasSpecial ? 1 : 0) + (hasDigit ? 1 : 0) + (hasUpper ? 1 : 0) + (hasLower ? 1 : 0);
            switch (count)
            {
                case 1:
                    return PasswordStrength.Weak;
                case 2:
                    return PasswordStrength.Weakish;
                case 3:
                    return PasswordStrength.Medium;
                // case 4
                default:
                    return PassesWeakPasswordCheck(password) ? PasswordStrength.VeryStrong : PasswordStrength.Strong;
            }
        }

        #endregion
    }
}
