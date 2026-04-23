using System;

namespace ETool.Core.Util
{
    /// <summary>
    /// 数据脱敏工具类
    /// </summary>
    public static class MaskUtil
    {
        /// <summary>
        /// 中国大陆11位手机号码脱敏处理：保留前3位和后4位，中间4位替换为指定字符
        /// </summary>
        /// <param name="phoneNumber">待脱敏的手机号码字符串</param>
        /// <param name="maskChar">用于替换的填充字符</param>
        /// <returns>脱敏后的字符串</returns>
        public static string MaskPhoneNumber(string phoneNumber, char maskChar = '*')
        {
            if (phoneNumber == null)
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }

            if (!ValidatorUtil.IsPhoneNumber(phoneNumber))
            {
                throw new ArgumentException("输入不是有效的中国大陆11位手机号码", nameof(phoneNumber));
            }

            // 11位固定结构：3 + 4 + 4
            var chars = phoneNumber.ToCharArray();
            chars[3] = maskChar;
            chars[4] = maskChar;
            chars[5] = maskChar;
            chars[6] = maskChar;
            return new string(chars);
        }
    }
}
