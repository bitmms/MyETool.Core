namespace ETool.Core.Core
{
    /// <summary>
    /// 字符工具类
    /// </summary>
    public static class CharUtil
    {
        public static bool Equals(char c1, char c2, bool ignoreCase)
        {
            return ignoreCase ? char.ToUpperInvariant(c1) == char.ToUpperInvariant(c2) : c1 == c2;
        }

        public static char ToUpper(char c)
        {
            return char.ToUpperInvariant(c);
        }

        public static char ToLower(char c)
        {
            return char.ToLowerInvariant(c);
        }
    }
}
