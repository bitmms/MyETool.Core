using System;
using System.Collections.Generic;
using System.Text;
using ETool.Core.Util;

namespace ETool.Core.MyUtil
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class StrUtil
    {
        /// <summary>
        /// 判断指定字符串是否为 null
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串为 null 返回 true，否则返回 false</returns>
        public static bool IsNull(string s)
        {
            return s == null;
        }

        /// <summary>
        /// 判断指定字符串是否不为 null
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串不为 null 返回 true，否则返回 false</returns>
        public static bool IsNotNull(string s)
        {
            return s != null;
        }

        /// <summary>
        /// 判断指定字符串是否为空
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串为空返回 true，否则返回 false</returns>
        public static bool IsEmpty(string s)
        {
            return s == string.Empty;
        }

        /// <summary>
        /// 判断指定字符串是否不为空
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串不为空返回 true，否则返回 false</returns>
        public static bool IsNotEmpty(string s)
        {
            return s != string.Empty;
        }

        /// <summary>
        /// 判断指定字符串是否为 null 或为空
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>如果字符串为 null 或为空返回 true，否则返回 false</returns>
        public static bool IsNullOrEmpty(string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 获取指定字符串长度
        /// </summary>
        /// <param name="s">要获取长度的字符串</param>
        /// <returns>如果 <paramref name="s"/> 为 <see langword="null"/>，返回 0；否则返回实际长度</returns>
        public static int Length(string s)
        {
            return s?.Length ?? 0;
        }

        // =======================================================

        /// <summary>
        /// 如果指定的字符串为 <c>null</c> 则返回 <c>""</c>，否则返回原字符串
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果 <c>str</c> 为 <c>null</c> 则返回 <c>""</c>，否则返回 <c>str</c></returns>
        public static string EmptyIfNull(string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// 如果指定的字符串为 <c>""</c> 则返回 <c>null</c>，否则返回原字符串
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果 <c>str</c> 为 <c>""</c> 则返回 <c>null</c>，否则返回 <c>str</c></returns>
        public static string NullIfEmpty(string str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }

        /// <summary>
        /// 将使用 Unicode 字符集表示的字符串以 UTF8 编码保存到字节数组
        /// </summary>
        /// <remarks>无字节序问题，1~4字节的变长编码格式</remarks>
        /// <param name="s">入参字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] GetUtf8Bytes(string s)
        {
            if (IsNullOrEmpty(s)) return Array.Empty<byte>();
            return Encoding.UTF8.GetBytes(s);
        }

        /// <summary>
        /// 将使用 Unicode 字符集表示的字符串以 UTF16 编码保存到字节数组
        /// </summary>
        /// <remarks>小端序在前，2字节（BMP字符）或4字节（补充平面字符）的变长编码格式</remarks>
        /// <param name="s">入参字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] GetUtf16Bytes(string s)
        {
            if (IsNullOrEmpty(s)) return Array.Empty<byte>();
            return Encoding.Unicode.GetBytes(s);
        }

        /// <summary>
        /// 将使用 Unicode 字符集表示的字符串以 UTF32 编码保存到字节数组
        /// </summary>
        /// <remarks>小端序在前，固定4字节的编码格式</remarks>
        /// <param name="s">入参字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] GetUtf32Bytes(string s)
        {
            if (IsNullOrEmpty(s)) return Array.Empty<byte>();
            return Encoding.UTF32.GetBytes(s);
        }

        /// <summary>
        /// 将 Unicode 字符集的字符串转换为 GBK 编码的字节数组（基于 GBK 字符集映射）
        /// </summary>
        /// <param name="s">入参字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] GetGbkBytes(string s)
        {
            if (IsNullOrEmpty(s)) return Array.Empty<byte>();
            return Encoding.GetEncoding("GBK").GetBytes(s);
        }

        /// <summary>
        /// 将指定字符重复指定次数
        /// </summary>
        /// <param name="c">待重复的字符</param>
        /// <param name="count">重复次数</param>
        /// <param name="sep">分隔符</param>
        /// <returns>重复拼接后的字符串</returns>
        public static string Repeat(char c, int count, char sep = ' ')
        {
            if (count <= 0)
            {
                return "";
            }

            if (count == 1)
            {
                return c.ToString();
            }

            long totalLength = 2L * count - 1;
            if (totalLength > int.MaxValue)
            {
                return "";
            }

            char[] resultChars = new char[totalLength];

            int nextIndex = 0;
            resultChars[nextIndex] = c;
            nextIndex += 1;
            for (int i = 1; i < count; i++)
            {
                resultChars[nextIndex] = sep;
                nextIndex += 1;

                resultChars[nextIndex] = c;
                nextIndex += 1;
            }

            return new string(resultChars);
        }

        /// <summary>
        /// 将指定字符重复指定次数
        /// </summary>
        /// <param name="c">待重复的字符</param>
        /// <param name="count">重复次数</param>
        /// <param name="sep">分隔符</param>
        /// <returns>重复拼接后的字符串</returns>
        public static string Repeat(char c, int count, string sep = " ")
        {
            if (count <= 0)
            {
                return "";
            }

            if (count == 1)
            {
                return c.ToString();
            }

            if (IsNull(sep))
            {
                sep = "";
            }

            if (IsEmpty(sep))
            {
                return new string(c, count);
            }

            long totalLength = (long)(1 + sep.Length) * count - sep.Length;
            if (totalLength > int.MaxValue)
            {
                return "";
            }

            char[] resultChars = new char[totalLength];

            int nextIndex = 0;
            resultChars[nextIndex] = c;
            nextIndex += 1;
            for (int i = 1; i < count; i++)
            {
                sep.CopyTo(0, resultChars, nextIndex, sep.Length);
                nextIndex += sep.Length;

                resultChars[nextIndex] = c;
                nextIndex += 1;
            }

            return new string(resultChars);
        }

        /// <summary>
        /// 将指定字符串重复指定次数
        /// </summary>
        /// <param name="s">待重复的源字符串</param>
        /// <param name="count">重复次数</param>
        /// <param name="sep">分隔符</param>
        /// <returns>重复拼接后的字符串</returns>
        public static string Repeat(string s, int count, char sep = ' ')
        {
            if (IsNull(s) || count <= 0)
            {
                return "";
            }

            if (count == 1)
            {
                return s;
            }

            long totalLength = (long)(s.Length + 1) * count - 1;
            if (totalLength > int.MaxValue)
            {
                return "";
            }

            char[] resultChars = new char[totalLength];

            int nextIndex = 0;
            s.CopyTo(0, resultChars, nextIndex, s.Length);
            nextIndex += s.Length;
            for (int i = 1; i < count; i++)
            {
                resultChars[nextIndex] = sep;
                nextIndex += 1;

                s.CopyTo(0, resultChars, nextIndex, s.Length);
                nextIndex += s.Length;
            }

            return new string(resultChars);
        }

        /// <summary>
        /// 将指定字符串重复指定次数
        /// </summary>
        /// <param name="s">待重复的源字符串</param>
        /// <param name="count">重复次数</param>
        /// <param name="sep">分隔符</param>
        /// <returns>重复拼接后的字符串</returns>
        public static string Repeat(string s, int count, string sep = " ")
        {
            if (IsNull(s) || count <= 0)
            {
                return "";
            }

            if (count == 1)
            {
                return s;
            }

            if (IsNull(sep))
            {
                sep = "";
            }

            long totalLength = (long)(s.Length + sep.Length) * count - sep.Length;
            if (totalLength > int.MaxValue)
            {
                return "";
            }

            char[] resultChars = new char[totalLength];

            if (IsEmpty(sep))
            {
                // 开头部分：直接手动拷贝
                s.CopyTo(0, resultChars, 0, s.Length);

                // 记录当前已经拷贝的数量
                int n = s.Length;

                // 中间部分：倍增拷贝
                while (n < totalLength - n)
                {
                    Array.Copy(resultChars, 0, resultChars, n, n);
                    n <<= 1; // n *= 2;
                }

                // 结尾部分：直接手动拷贝
                Array.Copy(resultChars, 0, resultChars, n, totalLength - n);

                return new string(resultChars);
            }

            int nextIndex = 0;
            s.CopyTo(0, resultChars, nextIndex, s.Length);
            nextIndex += s.Length;
            for (int i = 1; i < count; i++)
            {
                sep.CopyTo(0, resultChars, nextIndex, sep.Length);
                nextIndex += sep.Length;

                s.CopyTo(0, resultChars, nextIndex, s.Length);
                nextIndex += s.Length;
            }

            return new string(resultChars);
        }

        /// <summary>
        /// 在字符串的指定范围内查找指定字符首次出现的索引
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="c">目标字符</param>
        /// <param name="start">起始索引位置（包含）</param>
        /// <param name="count">需要检查的字符数量</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>找到返回索引，否则返回 -1</returns>
        public static int IndexOf(string s, char c, int start, int count, bool ignoreCase = false)
        {
            if (IsNull(s) || IsEmpty(s))
            {
                return -1;
            }

            if (start >= s.Length || count <= 0)
            {
                return -1;
            }

            if (start < 0)
            {
                start = 0;
            }

            if (count > s.Length - start)
            {
                count = s.Length - start;
            }

            int endIndex = start + count - 1;

            if (!ignoreCase)
            {
                for (int i = start; i <= endIndex; i++)
                {
                    if (s[i] == c)
                    {
                        return i;
                    }
                }
            }
            else
            {
                char upperTargetChar = CharUtil.ToUpperLetter(c);
                for (int i = start; i <= endIndex; i++)
                {
                    if (CharUtil.ToUpperLetter(s[i]) == upperTargetChar)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 在字符串中查找指定字符首次出现的索引
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="c">目标字符</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>找到返回索引，否则返回 -1</returns>
        public static int IndexOf(string s, char c, bool ignoreCase = false)
        {
            if (IsNull(s))
            {
                return -1;
            }

            return IndexOf(s, c, 0, s.Length, ignoreCase);
        }

        /// <summary>
        /// 在字符串的指定范围内查找指定子串首次出现的索引
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="targetString">目标子串</param>
        /// <param name="start">起始索引位置（包含）</param>
        /// <param name="count">需要检查的字符数量</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>找到返回索引，否则返回 -1</returns>
        public static int IndexOf(string sourceString, string targetString, int start, int count, bool ignoreCase = false)
        {
            if (IsNull(sourceString) || IsNull(targetString))
            {
                return -1;
            }

            if (start < 0)
            {
                start = 0;
            }

            if (targetString == string.Empty && start <= sourceString.Length)
            {
                return start;
            }

            if (start >= sourceString.Length || count <= 0)
            {
                return -1;
            }

            if (count > sourceString.Length - start)
            {
                count = sourceString.Length - start;
            }

            if (count < targetString.Length)
            {
                return -1;
            }

            // 外层循环：尝试每一个可能的起始位置
            int lastStart = start + (count - 1) - (targetString.Length - 1);
            for (int i = start; i <= lastStart; i++)
            {
                bool match = true;

                // 内层循环：逐字符比较
                for (int j = 0; j < targetString.Length; j++)
                {
                    char sourceChar = sourceString[i + j];
                    char targetChar = targetString[j];

                    if (ignoreCase)
                    {
                        sourceChar = CharUtil.ToUpperLetter(sourceChar);
                        targetChar = CharUtil.ToUpperLetter(targetChar);
                    }

                    if (sourceChar != targetChar)
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    return i;
                }
            }


            return -1;
        }

        /// <summary>
        /// 在字符串中查找指定子串首次出现的索引
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="targetString">目标子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>找到返回索引，否则返回 -1</returns>
        public static int IndexOf(string sourceString, string targetString, bool ignoreCase = false)
        {
            if (IsNull(sourceString))
            {
                return -1;
            }

            return IndexOf(sourceString, targetString, 0, sourceString.Length, ignoreCase);
        }

        /// <summary>
        /// 判断字符串是否包含指定字符
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="c">目标字符</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>包含返回 true，否则返回 false</returns>
        public static bool Contains(string s, char c, bool ignoreCase = false)
        {
            return IndexOf(s, c, ignoreCase) >= 0;
        }

        /// <summary>
        /// 判断字符串是否包含指定子串
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="targetString">目标子串</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>包含返回 true，否则返回 false</returns>
        public static bool Contains(string sourceString, string targetString, bool ignoreCase = false)
        {
            return IndexOf(sourceString, targetString, ignoreCase) >= 0;
        }

        /// <summary>
        /// 判断入参是否包含数字字符
        /// </summary>
        /// <param name="str">待检查的字符串</param>
        /// <returns>如果字符串包含至少一个数字字符，则返回 true；否则返回 false</returns>
        /// <exception cref="ArgumentNullException">当输入字符串为 null 时抛出。</exception>
        public static bool ContainsDigit(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str), "输入字符串不能为 null");
            }

            foreach (var c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断入参是否包含小写英文字符
        /// </summary>
        /// <param name="str">待检查的字符串</param>
        /// <returns>如果字符串包含至少一个小写英文字符，则返回 true；否则返回 false</returns>
        /// <exception cref="ArgumentNullException">当输入字符串为 null 时抛出。</exception>
        public static bool ContainsLowerLetter(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str), "输入字符串不能为 null");
            }

            foreach (var c in str)
            {
                if (c >= 'a' && c <= 'z')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断入参是否包含大写英文字符
        /// </summary>
        /// <param name="str">待检查的字符串</param>
        /// <returns>如果字符串包含至少一个大写英文字符，则返回 true；否则返回 false</returns>
        /// <exception cref="ArgumentNullException">当输入字符串为 null 时抛出。</exception>
        public static bool ContainsUpperLetter(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str), "输入字符串不能为 null");
            }

            foreach (var c in str)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断入参是否包含 ASCII 中除数字、字母以外的特殊符号（如 !, @, #, $ 等）
        /// </summary>
        /// <param name="str">待检查的字符串</param>
        /// <returns>
        /// 如果字符串中至少有一个字符属于 ASCII 可见字符（33～126）且不是数字（0-9）、小写字母（a-z）和大写字母（A-Z），则返回 true；否则返回 false</returns>
        /// <exception cref="ArgumentNullException">当输入字符串为 null 时抛出</exception>
        public static bool ContainsSpecialSymbol(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str), "输入字符串不能为 null");
            }

            foreach (var c in str)
            {
                // 只考虑 ASCII 可见字符范围（33～126）
                if (c >= 33 && c <= 126)
                {
                    // 排除数字、小写字母、大写字母
                    if ((c < '0' || c > '9') && (c < 'A' || c > 'Z') && (c < 'a' || c > 'z'))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 判断入参是否仅包含 ASCII 可见字符（ASCII 33 到 126，不包括空格）
        /// </summary>
        /// <param name="str">待检查的字符串</param>
        /// <returns>如果字符串中每个字符的 ASCII 值均在 [33, 126] 范围内，则返回 true；否则返回 false。</returns>
        /// <exception cref="ArgumentNullException">当输入字符串为 null 时抛出。</exception>
        public static bool ContainsOnlyVisibleAscii(string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            foreach (char c in str)
            {
                if (c < 33 || c > 126) // 注意：33 起始
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 判断字符串是否以指定子串开头
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="prefix">待检查的前缀子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>字符串以指定子串开头返回 true，否则返回 false</returns>
        public static bool StartsWith(string sourceString, string prefix, bool ignoreCase = false)
        {
            if (IsNull(sourceString) || IsNull(prefix))
            {
                return false;
            }

            if (IsEmpty(prefix))
            {
                return true;
            }

            if (sourceString.Length < prefix.Length)
            {
                return false;
            }

            for (int i = 0; i < prefix.Length; i++)
            {
                char sourceChar = sourceString[i];
                char targetChar = prefix[i];
                if (ignoreCase)
                {
                    sourceChar = CharUtil.ToUpperLetter(sourceChar);
                    targetChar = CharUtil.ToUpperLetter(targetChar);
                }

                if (sourceChar != targetChar)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断字符串是否以指定子串结束
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="suffix">待检查的后缀子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>字符串以指定子串结束返回 true，否则返回 false</returns>
        public static bool EndsWith(string sourceString, string suffix, bool ignoreCase = false)
        {
            if (IsNull(sourceString) || IsNull(suffix))
            {
                return false;
            }

            if (IsEmpty(suffix))
            {
                return true;
            }

            if (sourceString.Length < suffix.Length)
            {
                return false;
            }

            int idx = 0;
            for (int i = sourceString.Length - suffix.Length; i < sourceString.Length; i++)
            {
                char sourceChar = sourceString[i];
                char targetChar = suffix[idx++];
                if (ignoreCase)
                {
                    sourceChar = CharUtil.ToUpperLetter(sourceChar);
                    targetChar = CharUtil.ToUpperLetter(targetChar);
                }

                if (sourceChar != targetChar)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 字符串集合转字符串
        /// </summary>
        /// <param name="separator">用于连接各元素的分隔符</param>
        /// <param name="items">待连接的字符串集合</param>
        /// <param name="skipNull">是否跳过集合中的 null 元素</param>
        /// <param name="nullReplacement">当不跳过 null 时，用此字符串代替 null</param>
        /// <returns>拼接后的字符串</returns>
        public static string Join(string separator, IEnumerable<string> items, bool skipNull = true, string nullReplacement = "null")
        {
            if (items == null)
            {
                return "";
            }

            if (!skipNull && IsNull(nullReplacement))
            {
                nullReplacement = "null";
            }

            if (separator == null)
            {
                separator = "";
            }

            bool isFirstItem = true;
            StringBuilder sb = new StringBuilder();
            foreach (string item in items)
            {
                if (IsNull(item) && skipNull)
                {
                    continue;
                }

                if (isFirstItem)
                {
                    isFirstItem = false;
                }
                else
                {
                    sb.Append(separator);
                }

                sb.Append(IsNull(item) ? nullReplacement : item);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 字符串集合转字符串
        /// </summary>
        /// <param name="sep">用于连接各元素的分隔符</param>
        /// <param name="items">待连接的字符串集合</param>
        /// <param name="skipNull">是否跳过集合中的 null 元素</param>
        /// <param name="nullReplacement">当不跳过 null 时，用此字符串代替 null</param>
        /// <returns>拼接后的字符串</returns>
        public static string Join(char sep, IEnumerable<string> items, bool skipNull = true, string nullReplacement = "null")
        {
            return Join(sep.ToString(), items, skipNull, nullReplacement);
        }

        /// <summary>
        /// 移除字符串的指定前缀
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="prefix">待移除的前缀子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>移除前缀的字符串</returns>
        public static string RemovePrefix(string sourceString, string prefix, bool ignoreCase = false)
        {
            if (IsNull(sourceString) || IsEmpty(sourceString))
            {
                return "";
            }

            if (IsNull(prefix) || IsEmpty(prefix))
            {
                return sourceString;
            }

            if (StartsWith(sourceString, prefix, ignoreCase))
            {
                sourceString = sourceString.Substring(prefix.Length);
            }

            return sourceString;
        }

        /// <summary>
        /// 移除字符串的指定后缀
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="suffix">待移除的后缀子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>移除后缀的字符串</returns>
        public static string RemoveSuffix(string sourceString, string suffix, bool ignoreCase = false)
        {
            if (IsNull(sourceString) || IsEmpty(sourceString))
            {
                return "";
            }

            if (IsNull(suffix) || IsEmpty(suffix))
            {
                return sourceString;
            }

            if (EndsWith(sourceString, suffix, ignoreCase))
            {
                sourceString = sourceString.Substring(0, sourceString.Length - suffix.Length);
            }

            return sourceString;
        }

        /// <summary>
        /// 移除字符串中全部的换行符【"\r\n"，"\n"，"\r"】
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>移除全部换行符后的字符串</returns>
        public static string RemoveAllNewLines(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            var idx = 0;
            var resultChars = new char[str.Length];
            foreach (var c in str)
            {
                if (c != '\r' && c != '\n')
                {
                    resultChars[idx++] = c;
                }
            }

            return new string(resultChars, 0, idx);
        }

        /// <summary>
        /// 移除字符串中全部的空白字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>移除全部空白字符后的字符串</returns>
        public static string RemoveAllWhitespace(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            int idx = 0;
            var resultChars = new char[str.Length];
            foreach (char c in str)
            {
                if (!char.IsWhiteSpace(c))
                {
                    resultChars[idx++] = c;
                }
            }

            return new string(resultChars, 0, idx);
        }

        /// <summary>
        /// 移除字符串中第一个匹配的指定字符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="c">指定字符</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>移除第一个匹配的指定字符后的字符串</returns>
        public static string RemoveFirstChar(string s, char c, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var index = ignoreCase ? IndexOf(s, c, true) : s.IndexOf(c);
            if (index < 0)
            {
                return s;
            }

            // 构造新字符串：跳过 index 位置
            var resultChars = new char[s.Length - 1];

            // 复制字符串前半部分 [0, index)
            s.CopyTo(0, resultChars, 0, index);

            // 复制字符串后半部分 [index+1, end)
            s.CopyTo(index + 1, resultChars, index, s.Length - index - 1);

            return new string(resultChars);
        }

        /// <summary>
        /// 移除字符串中全部的指定字符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="c">指定字符</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>移除全部指定字符后的字符串</returns>
        public static string RemoveAllChar(string s, char c, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var idx = 0;
            var len = s.Length;
            var resultChars = new char[len];

            if (ignoreCase)
            {
                c = CharUtil.ToUpperLetter(c);
                for (var i = 0; i < len; i++)
                {
                    if (CharUtil.ToUpperLetter(s[i]) != c)
                    {
                        resultChars[idx++] = s[i];
                    }
                }
            }
            else
            {
                for (var i = 0; i < len; i++)
                {
                    if (s[i] != c)
                    {
                        resultChars[idx++] = s[i];
                    }
                }
            }

            return new string(resultChars, 0, idx);
        }

        /// <summary>
        /// 移除字符串中全部的指定字符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="chars">字符数组</param>
        /// <returns>移除全部指定字符后的字符串</returns>
        public static string RemoveAllChars(string s, params char[] chars)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            if (chars == null || chars.Length == 0)
            {
                return s;
            }

            var idx = 0;
            var len = s.Length;
            var resultChars = new char[len];

            if (chars.Length > 4)
            {
                var charSet = new HashSet<char>(chars);

                for (var i = 0; i < len; i++)
                {
                    if (!charSet.Contains(s[i]))
                    {
                        resultChars[idx++] = s[i];
                    }
                }
            }
            else
            {
                for (var i = 0; i < len; i++)
                {
                    if (Array.IndexOf(chars, s[i]) < 0)
                    {
                        resultChars[idx++] = s[i];
                    }
                }
            }

            return new string(resultChars, 0, idx);
        }

        /// <summary>
        /// 移除字符串中第一个匹配的指定子串
        /// </summary>
        /// <param name="sourceString">字符串</param>
        /// <param name="targetSubstring">待移除的子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>移除第一个匹配的指定子串后的字符串</returns>
        public static string RemoveFirstString(string sourceString, string targetSubstring, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(targetSubstring))
            {
                return sourceString;
            }

            var index = ignoreCase ? IndexOf(sourceString, targetSubstring, true) : sourceString.IndexOf(targetSubstring, StringComparison.Ordinal);
            if (index < 0)
            {
                return sourceString;
            }

            // 构造新字符串：跳过 index 位置
            var resultChars = new char[sourceString.Length - targetSubstring.Length];

            // 复制字符串前半部分 [0, index)
            sourceString.CopyTo(0, resultChars, 0, index);

            // 复制字符串后半部分 [index+1, end)
            sourceString.CopyTo(index + targetSubstring.Length, resultChars, index, sourceString.Length - index - targetSubstring.Length);

            return new string(resultChars);
        }

        /// <summary>
        /// 移除字符串中全部的指定子串
        /// </summary>
        /// <param name="sourceString">字符串</param>
        /// <param name="targetSubstring">待移除的子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>移除全部指定子串后的字符串</returns>
        public static string RemoveAllString(string sourceString, string targetSubstring, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(targetSubstring))
            {
                return sourceString;
            }

            if (!ignoreCase)
            {
                return sourceString.Replace(targetSubstring, "");
            }

            var sb = new StringBuilder();
            var startIndex = 0;
            var targetLength = targetSubstring.Length;
            var sourceLength = sourceString.Length;

            while (startIndex <= sourceLength - targetLength)
            {
                var foundIndex = IndexOf(sourceString, targetSubstring, startIndex, sourceLength - startIndex, true);
                if (foundIndex == -1)
                {
                    break;
                }

                // 追加 [start, foundIndex) 之间的内容
                sb.Append(sourceString, startIndex, foundIndex - startIndex);
                // 跳过匹配部分
                startIndex = foundIndex + targetLength;
            }

            // 追加剩余部分
            sb.Append(sourceString, startIndex, sourceLength - startIndex);
            return sb.ToString();
        }

        /// <summary>
        /// 移除字符串中全部的指定子串【按照子串的传入顺序进行移除】
        /// </summary>
        /// <param name="sourceString">字符串</param>
        /// <param name="targetSubstrings">字符串数组</param>
        /// <returns>移除全部指定子串后的字符串</returns>
        public static string RemoveAllStrings(string sourceString, params string[] targetSubstrings)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                return string.Empty;
            }

            if (targetSubstrings == null || targetSubstrings.Length == 0)
            {
                return sourceString;
            }

            foreach (var s in targetSubstrings)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    sourceString = RemoveAllString(sourceString, s);
                }
            }

            return sourceString;
        }

        /// <summary>
        /// 替换字符串中第一个匹配的字符为指定字符
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="sourceChar">被替换的源字符</param>
        /// <param name="targetChar">用于替换的目标字符</param>
        /// <param name="ignoreCase">是否忽略大小写（仅对英文字母 a-z / A-Z 生效）</param>
        /// <returns>替换后的新字符串</returns>
        public static string ReplaceFirstChar(string s, char sourceChar, char targetChar, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var index = -1;

            // 只有英文字母才有大小写之分
            if (!ignoreCase || !CharUtil.IsLetter(sourceChar))
            {
                index = s.IndexOf(sourceChar);
            }
            else
            {
                var sourceUpperChar = CharUtil.ToUpperLetter(sourceChar);

                for (var i = 0; i < s.Length; i++)
                {
                    if (CharUtil.ToUpperLetter(s[i]) == sourceUpperChar)
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index < 0)
            {
                return s;
            }

            var resultChars = new char[s.Length];
            s.CopyTo(0, resultChars, 0, index);
            resultChars[index] = targetChar;
            s.CopyTo(index + 1, resultChars, index + 1, s.Length - index - 1);
            return new string(resultChars);
        }

        /// <summary>
        /// 替换字符串中所有匹配的字符为指定字符
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="sourceChar">被替换的源字符</param>
        /// <param name="targetChar">用于替换的目标字符</param>
        /// <param name="ignoreCase">是否忽略大小写（仅对英文字母 a-z / A-Z 生效）</param>
        /// <returns>替换后的新字符串</returns>
        public static string ReplaceAllChar(string s, char sourceChar, char targetChar, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            // 只有英文字母才有大小写之分
            if (!ignoreCase || !CharUtil.IsLetter(sourceChar))
            {
                return s.Replace(sourceChar, targetChar);
            }

            var sourceUpperChar = CharUtil.ToUpperLetter(sourceChar);
            var resultChars = new char[s.Length];
            for (var i = 0; i < s.Length; i++)
            {
                if (CharUtil.ToUpperLetter(s[i]) == sourceUpperChar)
                {
                    resultChars[i] = targetChar;
                }
                else
                {
                    resultChars[i] = s[i];
                }
            }

            return new string(resultChars);
        }

        /// <summary>
        /// 替换字符串中所有匹配的字符串为指定字符串
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="sourceString">被替换的源字符串</param>
        /// <param name="targetString">用于替换的目标字符串</param>
        /// <param name="ignoreCase">是否忽略大小写（仅对英文字母 a-z / A-Z 生效）</param>
        /// <returns>替换后的新字符串</returns>
        public static string ReplaceAllString(string s, string sourceString, string targetString, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(sourceString))
            {
                return s;
            }

            if (string.IsNullOrEmpty(targetString))
            {
                targetString = string.Empty;
            }

            var result = new StringBuilder();
            var startIndex = 0;
            var sourceLen = sourceString.Length;

            while (startIndex <= s.Length - sourceLen)
            {
                var index = ignoreCase
                    ? IndexOf(s, sourceString, startIndex, s.Length - startIndex, true)
                    : s.IndexOf(sourceString, startIndex, StringComparison.Ordinal);

                if (index < 0)
                {
                    break;
                }

                result.Append(s, startIndex, index - startIndex);
                result.Append(targetString);

                startIndex = index + sourceLen;
            }

            if (startIndex < s.Length)
            {
                result.Append(s, startIndex, s.Length - startIndex);
            }

            return result.ToString();
        }

        /// <summary>
        /// 替换字符串中第一个匹配的字符串为指定字符串
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="sourceString">被替换的源字符串</param>
        /// <param name="targetString">用于替换的目标字符串</param>
        /// <param name="ignoreCase">是否忽略大小写（仅对英文字母 a-z / A-Z 生效）</param>
        /// <returns>替换后的新字符串</returns>
        public static string ReplaceFirstString(string s, string sourceString, string targetString, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(sourceString))
            {
                return s;
            }

            targetString = targetString ?? string.Empty;

            var index = ignoreCase
                ? IndexOf(s, sourceString, true)
                : s.IndexOf(sourceString, StringComparison.Ordinal);

            if (index == -1)
            {
                return s;
            }

            if (targetString.Length == 0)
            {
                return s.Substring(0, index) + s.Substring(index + sourceString.Length);
            }

            return s.Substring(0, index) + targetString + s.Substring(index + sourceString.Length);
        }

        /// <summary>
        /// 在字符串指定起始位置删除指定长度的子串，并插入目标字符指定次数
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">替换起始索引</param>
        /// <param name="deleteLength">要删除的子串长度</param>
        /// <param name="insertChar">要插入的目标字符</param>
        /// <param name="count">插入目标字符的次数</param>
        /// <returns>操作后的新字符串</returns>
        /// <remarks>
        /// <para>1. 仅删除：<c>deleteLength != 0, count == 0</c></para>
        /// <para>2. 仅插入：<c>deleteLength == 0, count != 0</c></para>
        /// <para>3. 删除并插入：<c>deleteLength != 0, count != 0</c></para>
        /// </remarks>
        public static string Splice(string sourceString, int startIndex, int deleteLength, char insertChar, int count = 1)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                return string.Empty;
            }

            if (deleteLength == 0 && count == 0)
            {
                return sourceString;
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            if (startIndex > sourceString.Length || deleteLength < 0 || count < 0 || (startIndex == sourceString.Length && count == 0))
            {
                return sourceString;
            }

            if (startIndex == sourceString.Length && count > 0)
            {
                if ((long)sourceString.Length + count > int.MaxValue)
                {
                    return sourceString;
                }

                return sourceString + new string(insertChar, count);
            }

            deleteLength = Math.Min(deleteLength, sourceString.Length - startIndex);
            var totalLength = (long)sourceString.Length - deleteLength + count;
            if (totalLength > int.MaxValue)
            {
                return sourceString;
            }

            var resultChars = new char[(int)totalLength];

            // 复制前段
            sourceString.CopyTo(0, resultChars, 0, startIndex);

            // 插入目标字符 count 次
            for (var i = startIndex; i < startIndex + count; i++)
            {
                resultChars[i] = insertChar;
            }

            // 复制后段（如果有）
            if (sourceString.Length > startIndex + deleteLength)
            {
                sourceString.CopyTo(startIndex + deleteLength, resultChars, startIndex + count, sourceString.Length - startIndex - deleteLength);
            }

            return new string(resultChars);
        }

        /// <summary>
        /// 在字符串指定起始位置删除指定长度的子串，并插入目标字符串指定次数
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">替换起始索引</param>
        /// <param name="deleteLength">要删除的子串长度</param>
        /// <param name="insertString">要插入的目标字符串</param>
        /// <param name="count">插入目标字符串的次数</param>
        /// <returns>操作后的新字符串</returns>
        /// <remarks>
        /// <para>1. 仅删除：<c>deleteLength != 0, count == 0</c></para>
        /// <para>2. 仅插入：<c>deleteLength == 0, count != 0</c></para>
        /// <para>3. 删除并插入：<c>deleteLength != 0, count != 0</c></para>
        /// </remarks>
        public static string Splice(string sourceString, int startIndex, int deleteLength, string insertString, int count = 1)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                return string.Empty;
            }

            if (deleteLength == 0 && count == 0)
            {
                return sourceString;
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            if (startIndex > sourceString.Length || deleteLength < 0 || count < 0 || (startIndex == sourceString.Length && count == 0))
            {
                return sourceString;
            }

            insertString = insertString ?? string.Empty;

            if (startIndex == sourceString.Length)
            {
                if (insertString.Length == 0)
                {
                    return sourceString;
                }

                var totalLength = sourceString.Length + (long)insertString.Length * count;
                if (totalLength > int.MaxValue)
                {
                    return sourceString;
                }

                var resultChars = new char[(int)totalLength];

                // 复制前段
                sourceString.CopyTo(0, resultChars, 0, sourceString.Length);

                // 插入目标字符串 count 次
                var destIndex = sourceString.Length;
                for (var i = 0; i < count; i++)
                {
                    insertString.CopyTo(0, resultChars, destIndex, insertString.Length);
                    destIndex += insertString.Length;
                }

                return new string(resultChars);
            }
            else
            {
                deleteLength = Math.Min(deleteLength, sourceString.Length - startIndex);

                var totalLength = sourceString.Length - deleteLength + (long)insertString.Length * count;
                if (totalLength > int.MaxValue)
                {
                    return sourceString;
                }

                var resultChars = new char[(int)totalLength];

                // 复制前段
                sourceString.CopyTo(0, resultChars, 0, startIndex);

                // 插入目标字符串 count 次
                if (insertString.Length != 0)
                {
                    var destIndex = startIndex;
                    for (var i = 0; i < count; i++)
                    {
                        insertString.CopyTo(0, resultChars, destIndex, insertString.Length);
                        destIndex += insertString.Length;
                    }
                }

                // 复制后段（如果有）
                if (sourceString.Length > startIndex + deleteLength)
                {
                    sourceString.CopyTo(startIndex + deleteLength, resultChars, startIndex + count * insertString.Length, sourceString.Length - startIndex - deleteLength);
                }

                return new string(resultChars);
            }
        }
    }
}
