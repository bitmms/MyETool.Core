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
    }
}
