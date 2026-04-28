using System.Collections.Generic;
using ETool.Core.Util.Enum;

namespace ETool.Core.Util
{
    /// <summary>
    /// 密码工具类
    /// </summary>
    public static class PasswordUtil
    {
        /// <summary>
        /// 密码允许的最小长度
        /// </summary>
        private const int PasswordMinLength = 8;

        /// <summary>
        /// 密码允许的最大长度
        /// </summary>
        private const int PasswordMaxLength = 32;

        /// <summary>
        /// 密码允许的特殊字符
        /// </summary>
        private static readonly HashSet<char> PasswordAllowedSpecialChars = new HashSet<char>
        {
            '!', '@', '#', '$', '%', '^', '&', '*',
            '(', ')', '_', '-', '+', '=',
            '[', ']', '{', '}',
            '.', ',', '?', ':', ';', '~'
        };

        /// <summary>
        /// 检测密码的复杂度等级
        /// </summary>
        /// <param name="password">待检测的密码字符串</param>
        /// <returns>表示密码复杂度的 <see cref="PasswordStrength"/> 枚举值</returns>
        public static PasswordStrength CheckPasswordStrength(string password)
        {
            // 空值
            if (string.IsNullOrEmpty(password))
            {
                return PasswordStrength.Invalid;
            }

            // 长度
            if (password.Length < PasswordMinLength || password.Length > PasswordMaxLength)
            {
                return PasswordStrength.Invalid;
            }

            // 检查字符
            bool hasSpecial = false, hasDigit = false, hasUpper = false, hasLower = false;
            foreach (var c in password)
            {
                if (ValidatorUtil.IsDigit(c)) hasDigit = true;
                else if (ValidatorUtil.IsUpperLetter(c)) hasUpper = true;
                else if (ValidatorUtil.IsLowerLetter(c)) hasLower = true;
                else if (PasswordAllowedSpecialChars.Contains(c)) hasSpecial = true;
                else return PasswordStrength.Invalid;
            }

            var count = (hasSpecial ? 1 : 0) + (hasDigit ? 1 : 0) + (hasUpper ? 1 : 0) + (hasLower ? 1 : 0);
            return count switch
            {
                2 => PasswordStrength.Weak,
                3 => PasswordStrength.Medium,
                4 => PasswordStrength.Strong,
                _ => PasswordStrength.Invalid
            };
        }
    }
}
