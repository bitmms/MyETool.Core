using System;

namespace ETool.Core.Util
{
    /// <summary>
    /// 手机号码工具类
    /// </summary>
    public static class PhoneUtil
    {
        /// <summary>
        /// 将一个中国大陆手机号码拆分为三段：前3位、中间4位、后4位
        /// </summary>
        /// <param name="s">待拆分的手机号字符串</param>
        /// <returns>
        /// <para>一个元组 <c>(Prefix, Middle, Suffix)</c>，各字段含义如下：</para>
        /// <list>
        ///   <item><description><c>Prefix</c>：前面 3 位</description></item>
        ///   <item><description><c>Middle</c>：中间 4 位</description></item>
        ///   <item><description><c>Suffix</c>：后面 4 位</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"><c>s</c> 为 null</exception>
        /// <exception cref="ArgumentException"><c>s</c> 不是有效的中国大陆手机号码</exception>
        public static (string Prefix, string Middle, string Suffix) Format(string s)
        {
            // 1. null 值校验
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            // 2. 合法性校验
            if (!ValidatorUtil.IsValidPhoneNumber(s))
            {
                throw new ArgumentException("输入不是有效的中国大陆手机号码", nameof(s));
            }

            // 3. 拆分
            return (
                s.Substring(0, 3),
                s.Substring(3, 4),
                s.Substring(7, 4)
            );
        }
    }
}
