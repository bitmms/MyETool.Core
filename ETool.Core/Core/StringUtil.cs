using System;
using AssertUtil = ETool.Core.Core.Internal.AssertUtil;

namespace ETool.Core.Core
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class StringUtil
    {
        public static bool Equals(string s1, string s2, bool ignoreCase)
        {
            return string.Equals(s1, s2, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        public static string ToUpper(string s)
        {
            AssertUtil.IfTrue(s == null, "入参不能为 null");
            return s!.ToUpperInvariant();
        }

        public static string ToLower(string s)
        {
            AssertUtil.IfTrue(s == null, "入参不能为 null");
            return s!.ToLowerInvariant();
        }
    }
}
