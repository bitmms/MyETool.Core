namespace ETool.Core.Util
{
    /// <summary>
    /// 字符工具类
    /// </summary>
    public static class CharUtil
    {
        /// <summary>
        /// 判断指定字符是否为数字
        /// </summary>
        /// <param name="c">待判断的字符</param>
        /// <returns>如果字符为数字返回 true，否则返回 false</returns>
        /// <remarks>
        /// 本方法只识别半角数字，拒绝全角数字、Unicode 数字等
        /// </remarks>
        public static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// 判断指定字符是否为英文字符
        /// </summary>
        /// <param name="c">待判断的字符</param>
        /// <returns>如果字符为英文字符返回 true，否则返回 false</returns>
        /// <remarks>
        /// 本方法只识别半角英文字符，拒绝英文字符、Unicode 英文字符等
        /// </remarks>
        public static bool IsLetter(char c)
        {
            return c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z';
        }
    }
}
