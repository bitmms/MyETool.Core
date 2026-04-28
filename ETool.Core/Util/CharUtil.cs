namespace ETool.Core.Util
{
    /// <summary>
    /// 字符工具类
    /// </summary>
    public static class CharUtil
    {
        /// <summary>
        /// 比较两个字符是否相等
        /// </summary>
        /// <param name="c1">第一个字符</param>
        /// <param name="c2">第二个字符</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>如果字符相等返回 true，否则返回 false</returns>
        public static bool Equals(char c1, char c2, bool ignoreCase)
        {
            return ignoreCase ? char.ToUpperInvariant(c1) == char.ToUpperInvariant(c2) : c1 == c2;
        }

        /// <summary>
        /// 将小写英文字符转换成对应的大写英文字符，如果输入字符不是小写英文字符则返回本身
        /// </summary>
        /// <param name="c">待转换的字符</param>
        /// <returns>转换后的字符</returns>
        public static char ToUpperLetter(char c)
        {
            return char.ToUpperInvariant(c);
        }

        /// <summary>
        /// 将大写英文字符转换成对应的小写英文字符，如果输入字符不是大写英文字符则返回本身
        /// </summary>
        /// <param name="c">待转换的字符</param>
        /// <returns>转换后的字符</returns>
        public static char ToLowerLetter(char c)
        {
            return char.ToLowerInvariant(c);
        }
    }
}
