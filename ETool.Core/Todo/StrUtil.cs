using System;
using System.Collections.Generic;
using System.Text;
using ETool.Core.Util;

namespace ETool.Core.Todo
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class StrUtil
    {
        /// <summary>
        /// 将使用 Unicode 字符集表示的字符串以 UTF8 编码保存到字节数组
        /// </summary>
        /// <remarks>无字节序问题，1~4字节的变长编码格式</remarks>
        /// <param name="s">入参字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] GetUtf8Bytes(string s)
        {
            if (string.IsNullOrEmpty(s)) return Array.Empty<byte>();
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
            if (string.IsNullOrEmpty(s)) return Array.Empty<byte>();
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
            if (string.IsNullOrEmpty(s)) return Array.Empty<byte>();
            return Encoding.UTF32.GetBytes(s);
        }

        /// <summary>
        /// 将 Unicode 字符集的字符串转换为 GBK 编码的字节数组（基于 GBK 字符集映射）
        /// </summary>
        /// <param name="s">入参字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] GetGbkBytes(string s)
        {
            if (string.IsNullOrEmpty(s)) return Array.Empty<byte>();
            return Encoding.GetEncoding("GBK").GetBytes(s);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 移除字符串的指定前缀
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="prefix">待移除的前缀子串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>移除前缀的字符串</returns>
        public static string RemovePrefix(string sourceString, string prefix, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                return "";
            }

            if (string.IsNullOrEmpty(prefix))
            {
                return sourceString;
            }

            if (ValidatorUtil.IsStartsWith(sourceString, prefix, ignoreCase))
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
            if (string.IsNullOrEmpty(sourceString))
            {
                return "";
            }

            if (string.IsNullOrEmpty(suffix))
            {
                return sourceString;
            }

            if (ValidatorUtil.IsEndsWith(sourceString, suffix, ignoreCase))
            {
                sourceString = sourceString.Substring(0, sourceString.Length - suffix.Length);
            }

            return sourceString;
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

            var index = ignoreCase ? Core.Util.StrUtil.IndexOfChar(s, c, true) : s.IndexOf(c);
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

            var index = ignoreCase ? Core.Util.StrUtil.IndexOfString(sourceString, targetSubstring, true) : sourceString.IndexOf(targetSubstring, StringComparison.Ordinal);
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
                var foundIndex = Core.Util.StrUtil.IndexOfString(sourceString, targetSubstring, startIndex, sourceLength - startIndex, true);
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

        //-----------------------------------------------------------------------------------------------------------------------------------

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
                    ? Core.Util.StrUtil.IndexOfString(s, sourceString, startIndex, s.Length - startIndex, true)
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

            targetString ??= string.Empty;

            var index = ignoreCase
                ? Core.Util.StrUtil.IndexOfString(s, sourceString, true)
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

        //-----------------------------------------------------------------------------------------------------------------------------------

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
