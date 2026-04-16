using ETool.Core.Util;

namespace ETool.Core.Todo.MyUtil
{
    /// <summary>
    /// 手机号码工具类
    /// </summary>
    public static class PhoneUtil
    {
        /// <summary>
        /// 将一个有效的中国大陆手机号码拆分为三段：前3位、中间4位、后4位
        /// </summary>
        /// <param name="s">待拆分的手机号字符串</param>
        /// <returns>
        /// <para>一个具名值元组 <c>(Prefix, Middle, Suffix)</c>，各字段含义如下：</para>
        /// <list type="bullet">
        ///   <item><description><c>Prefix</c>：前3位</description></item>
        ///   <item><description><c>Middle</c>：中间4位</description></item>
        ///   <item><description><c>Suffix</c>：后4位</description></item>
        /// </list>
        /// <para>若输入未通过 <see cref="ValidatorUtil.IsValidPhoneNumber(string)"/> 验证则返回默认占位值 <c>("000", "0000", "0000")</c></para>
        /// </returns>
        public static (string Prefix, string Middle, string Suffix) Format(string s)
        {
            if (!ValidatorUtil.IsValidPhoneNumber(s))
            {
                return ("000", "0000", "0000");
            }

            return (s.Substring(0, 3), s.Substring(3, 4), s.Substring(7, 4));
        }
    }
}
