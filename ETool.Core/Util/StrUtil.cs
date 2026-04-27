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

        /// <summary>
        /// 将字符串指定区间的字符替换为目标字符
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">替换字符的数量</param>
        /// <param name="targetChar">用于替换的目标字符</param>
        /// <returns>替换后的新字符串</returns>
        public static string ReplaceRangeWithChar(string sourceString, int startIndex, int count, char targetChar)
        {
            // 判 null
            if (sourceString == null)
            {
                throw new ArgumentNullException(nameof(sourceString));
            }

            var length = sourceString.Length;

            // 起始索引必须在合法范围内
            if (startIndex < 0 || startIndex > length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            // 替换数量不能为负数
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            // 如果无需替换，直接返回原字符串
            if (startIndex == length || count == 0)
            {
                return sourceString;
            }

            // 如果替换范围超出字符串长度，则自动截断
            if (startIndex + count > length)
            {
                count = length - startIndex;
            }

            // 将字符串复制为字符数组（因为 string 不可变）
            var resultChars = sourceString.ToCharArray();

            // 计算结束索引（避免循环中重复计算）
            var end = startIndex + count;

            // 执行替换
            for (var i = startIndex; i < end; i++)
            {
                resultChars[i] = targetChar;
            }

            // 构造新的字符串实例
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
            // 判 null
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (sep == null) throw new ArgumentNullException(nameof(sep));

            // 重复次数不能为负数
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            // 重复 0 次
            if (count == 0 || s.Length == 0 && sep.Length == 0) return string.Empty;
            if (count == 1) return s;

            // 总长度判断
            if (((long)s.Length + sep.Length) * (count - 1) + s.Length > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "字符串重复后长度超出限制");
            }

            // 计算总长度
            var totalLength = (s.Length + sep.Length) * (count - 1);

            // 结果字符数组
            var resultChars = new char[totalLength + s.Length];

            // 1. 写入第一个(s+sep) 
            s.CopyTo(0, resultChars, 0, s.Length);
            sep.CopyTo(0, resultChars, s.Length, sep.Length);

            // 2. 倍增复制填充(s+sep) 
            var pos = s.Length + sep.Length;
            while (pos < totalLength - pos)
            {
                Array.Copy(resultChars, 0, resultChars, pos, pos);
                pos <<= 1; // n *= 2;
            }

            // 3. 补充剩余部分
            Array.Copy(resultChars, 0, resultChars, pos, totalLength - pos);

            // 4. 添加最后一个(s) 
            s.CopyTo(0, resultChars, totalLength, s.Length);

            // 返回
            return new string(resultChars);
        }

        /// <summary>
        /// 移除字符串中全部的换行符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>移除全部换行符后的字符串</returns>
        /// <remarks>换行符："\r\n"，"\n"，"\r"</remarks>
        public static string RemoveAllNewLine(string s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));

            var result = new char[s.Length];
            var idx = 0;
            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (s[i] != '\r' && s[i] != '\n') result[idx++] = s[i];
            }

            return new string(result, 0, idx);
        }

        /// <summary>
        /// 移除字符串中全部的空白字符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>移除全部空白字符后的字符串</returns>
        public static string RemoveAllWhitespace(string s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));

            var result = new char[s.Length];
            var idx = 0;
            var len = s.Length;
            for (var i = 0; i < len; i++)
            {
                if (!char.IsWhiteSpace(s[i])) result[idx++] = s[i];
            }

            return new string(result, 0, idx);
        }

        // ===========================================================================================================================

        /// <summary>
        /// 在字符串的指定范围内查找指定字符首次出现的索引
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="targetChar">目标字符</param>
        /// <param name="start">起始索引位置（包含）</param>
        /// <param name="count">需要检查的字符数量</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>找到返回索引，否则返回 -1</returns>
        /// <exception cref="ArgumentNullException"><c>sourceString</c> 为 null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>start</c> 小于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>start</c> 大于等于 <c>sourceString.Length</c></exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>count</c> 小于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException">检查范围超出字符串边界</exception>
        public static int IndexOfChar(string sourceString, char targetChar, int start, int count, bool ignoreCase = false)
        {
            // 空值校验
            if (sourceString == null) throw new ArgumentNullException(nameof(sourceString));
            // 起始索引范围校验
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start), $"起始索引必须大于等于 0，当前值为 {start}");
            if (start >= sourceString.Length) throw new ArgumentOutOfRangeException(nameof(start), $"起始索引必须小于 {sourceString.Length}，当前值为 {start}");
            // 检查数量校验
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), $"检查数量必须大于等于 0，当前值为 {count}");
            // 检查范围超出字符串边界
            if (start > sourceString.Length - count) throw new ArgumentOutOfRangeException(nameof(count), $"检查范围超出字符串边界。起始位置={start}，数量={count}，长度={sourceString.Length}");

            // 不忽略大小写时使用内置方法
            if (!ignoreCase) return sourceString.IndexOf(targetChar, start, count);

            // 忽略大小时遍历查找
            var endIndex = start + count - 1;
            var upperTarget = CharUtil.ToUpperLetter(targetChar);

            for (var i = start; i <= endIndex; i++)
            {
                if (upperTarget == CharUtil.ToUpperLetter(sourceString[i])) return i;
            }

            return -1;
        }

        /// <summary>
        /// 在字符串中查找指定字符首次出现的索引
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="targetChar">目标字符</param>
        /// <param name="ignoreCase">是否忽略英文字符的大小写</param>
        /// <returns>找到返回索引，否则返回 -1</returns>
        /// <exception cref="ArgumentNullException"><c>sourceString</c> 为 null</exception>
        public static int IndexOfChar(string sourceString, char targetChar, bool ignoreCase = false)
        {
            return sourceString == null
                ? throw new ArgumentNullException(nameof(sourceString))
                : IndexOfChar(sourceString, targetChar, 0, sourceString.Length, ignoreCase);
        }
    }
}
