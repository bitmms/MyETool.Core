using System;

namespace ETool.Core.Util
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class StrUtil
    {
        /// <summary>
        /// 比较两个字符串是否相等
        /// </summary>
        /// <param name="s1">第一个字符串</param>
        /// <param name="s2">第二个字符串</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>如果字符串相等返回 true，否则返回 false</returns>
        public static bool Equals(string s1, string s2, bool ignoreCase = false)
        {
            // 引用相等（包括都为 null）直接返回 true
            if (s1 == s2)
            {
                return true;
            }

            // 任意一个为 null，另一个不为 null，直接返回 false
            if (s1 == null || s2 == null)
            {
                return false;
            }

            // 长度不同直接返回 false
            if (s1.Length != s2.Length)
            {
                return false;
            }

            // 不忽略大小写时一定不相等，直接返回 false
            if (!ignoreCase)
            {
                return false;
            }

            // 忽略大小写的前提下逐字符比较
            var len = s1.Length;
            for (var i = 0; i < len; i++)
            {
                if (CharUtil.ToUpperLetter(s1[i]) != CharUtil.ToUpperLetter(s2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 将字符串中的小写英文字符转大写
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        /// <exception cref="ArgumentNullException"><c>s</c> 为 null</exception>
        public static string ToUpperLetter(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            var len = s.Length;
            var resultChars = new char[len];
            for (var i = 0; i < len; i++)
            {
                resultChars[i] = CharUtil.IsLowerLetter(s[i]) ? CharUtil.ToUpperLetter(s[i]) : s[i];
            }

            return new string(resultChars);
        }

        /// <summary>
        /// 将字符串中的大写英文字符转小写
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        /// <exception cref="ArgumentNullException"><c>s</c> 为 null</exception>
        public static string ToLowerLetter(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            var len = s.Length;
            var resultChars = new char[len];
            for (var i = 0; i < len; i++)
            {
                resultChars[i] = CharUtil.IsUpperLetter(s[i]) ? CharUtil.ToLowerLetter(s[i]) : s[i];
            }

            return new string(resultChars);
        }
    }
}
