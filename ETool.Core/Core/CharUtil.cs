using System;
using System.Text;

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

        /// <summary>
        /// 转为 UTF-16 大端序双字节
        /// </summary>
        /// <param name="c">待转换字符</param>
        /// <returns>high=高字节，low=低字节</returns>
        /// <remarks>C# 中的 char 默认使用小端序处理，所以这里直接使用小端序解析</remarks>
        public static (byte high, byte low) GetByteByBigEndianUnicode(char c)
        {
            byte low = (byte)c; // 强转 byte 直接使用低字节进行转换
            byte high = (byte)(c >> 8); // 右移8位会直接丢弃低字节，剩余高字节直接转换 byte 即可
            return (high, low);
        }

        /// <summary>
        /// 转为 UTF-16 小端序双字节
        /// </summary>
        /// <param name="c">待转换字符</param>
        /// <returns>low=低字节，high=高字节</returns>
        /// <remarks>C# 中的 char 默认使用小端序处理，所以这里直接使用小端序解析</remarks>
        public static (byte low, byte high) GetByteByLittleEndianUnicode(char c)
        {
            byte low = (byte)c; // 强转 byte 直接使用低字节进行转换
            byte high = (byte)(c >> 8); // 右移8位会直接丢弃低字节，剩余高字节直接转换 byte 即可
            return (low, high);
        }

        /// <summary>
        /// 将字符数组转为 UTF-16 小端序字节数组
        /// </summary>
        /// <param name="chars">源字符数组</param>
        /// <returns>UTF-16 LE 字节数组</returns>
        /// <exception cref="ArgumentNullException">chars 为 null 时抛出</exception>
        public static byte[] GetByteByLittleEndianUnicode(char[] chars)
        {
            return chars == null ? throw new ArgumentNullException(nameof(chars)) : Encoding.Unicode.GetBytes(chars);
        }

        /// <summary>
        /// 将字符数组转为 UTF-16 大端序字节数组
        /// </summary>
        /// <param name="chars">源字符数组</param>
        /// <returns>UTF-16 BE 字节数组</returns>
        /// <exception cref="ArgumentNullException">chars 为 null 时抛出</exception>
        public static byte[] GetByteByBigEndianUnicode(char[] chars)
        {
            return chars == null ? throw new ArgumentNullException(nameof(chars)) : Encoding.BigEndianUnicode.GetBytes(chars);
        }
    }
}
