namespace ETool.Core.Util
{
    /// <summary>
    /// 字符工具类
    /// </summary>
    public static class CharUtil
    {
       /// <summary>
        /// 将小写英文字符转换成对应的大写英文字符，如果输入字符不是小写英文字符则返回本身
        /// </summary>
        /// <param name="c">待转换的字符</param>
        /// <returns>转换后的字符</returns>
        public static char ToUpperLetter(char c)
        {
            return ValidatorUtil.IsLowerLetter(c) ? (char)(c - 32) : c;
        }

        /// <summary>
        /// 将大写英文字符转换成对应的小写英文字符，如果输入字符不是大写英文字符则返回本身
        /// </summary>
        /// <param name="c">待转换的字符</param>
        /// <returns>转换后的字符</returns>
        public static char ToLowerLetter(char c)
        {
            return ValidatorUtil.IsUpperLetter(c) ? (char)(c + 32) : c;
        }
    }
}
