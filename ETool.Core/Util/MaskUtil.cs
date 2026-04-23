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

        /// <summary>
        /// 中国大陆身份证号码脱敏处理：保留前3位和后3位，中间部分替换为指定掩码字符
        /// </summary>
        /// <param name="idCard">待脱敏的身份证号码字符串</param>
        /// <param name="maskChar">用于替换的填充字符</param>
        /// <returns>脱敏后的字符串</returns>
        public static string MaskIdCard(string idCard, char maskChar = '*')
        {
            if (idCard == null)
            {
                throw new ArgumentNullException(nameof(idCard));
            }

            if (!ValidatorUtil.IsChinaIdCard(idCard))
            {
                throw new ArgumentException("输入不是有效的中国大陆身份证号码", nameof(idCard));
            }

            var chars = idCard.ToCharArray();
            for (var i = 3; i < idCard.Length - 3; i++)
            {
                chars[i] = maskChar;
            }

            return new string(chars);
        }

        public static void Main()
        {
            // 110101199003071233
            // 110************233
            Console.WriteLine(MaskIdCard("110101199003071233"));
        }
    }
}
