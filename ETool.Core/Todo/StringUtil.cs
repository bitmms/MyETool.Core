using System;
using System.Collections.Generic;
using System.Text;
using ETool.Core.Util;

namespace ETool.Core.Todo
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class StringUtil
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

        /// <summary>
        /// 获取字符串的字节数组
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>字符串的字节数组</returns>
        public static byte[] GetBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 将字节数组转换为字符串
        /// </summary>
        /// <param name="bytes">要处理的字节数组</param>
        /// <returns>字节数组转换后的字符串</returns>
        public static string GetString(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 把一个数字转换为二进制字符串
        /// </summary>
        /// <param name="number">待转换的数字</param>
        /// <returns>该数字的二进制字符串</returns>
        public static string ToBinaryString(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string result = string.Empty;
            while (number > 0)
            {
                result = (number % 2).ToString() + result;
                number /= 2;
            }

            return result;
        }

        /// <summary>
        /// 把一个数字转换为八进制字符串
        /// </summary>
        /// <param name="number">待转换的数字</param>
        /// <returns>该数字的八进制字符串</returns>
        public static string ToOctalString(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string result = string.Empty;
            while (number > 0)
            {
                result = (number % 8).ToString() + result;
                number /= 8;
            }

            return result;
        }

        /// <summary>
        /// 把一个数字转换为十六进制字符串
        /// </summary>
        /// <param name="number">待转换的数字</param>
        /// <returns>该数字的十六进制字符串</returns>
        public static string ToHexString(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string result = string.Empty;
            while (number > 0)
            {
                int remainder = number % 16;
                if (remainder < 10)
                {
                    result = remainder.ToString() + result;
                }
                else
                {
                    result = (char)('A' + remainder - 10) + result;
                }

                number /= 16;
            }

            return result;
        }
        
        /// <summary>
        /// 格式化一个 decimal 数字
        /// </summary>
        /// <param name="number">待格式化的数字</param>
        /// <param name="format">格式化字符串</param>
        /// <returns>格式化后的字符串</returns>
        public static string DecimalFormat(decimal number, string format)
        {
            return number.ToString(format);
        }

        /// <summary>
        /// 保留一个 decimal 数字的小数点后指定位数
        /// </summary>
        /// <param name="number">待格式化的数字</param>
        /// <param name="decimalPlaces">小数点后保留的位数</param>
        /// <returns>格式化后的字符串</returns>
        public static string DecimalFormat(decimal number, int decimalPlaces)
        {
            string format = "0.";
            for (int i = 0; i < decimalPlaces; i++)
            {
                format += "0";
            }

            return DecimalFormat(number, format);
        }

        /// <summary>
        /// 格式化一个 decimal 数字，并加上千位分隔符
        /// </summary>
        /// <param name="number">待格式化的数字</param>
        /// <returns>格式化后的字符串</returns>
        public static string DecimalFormatWithCommas(decimal number)
        {
            return DecimalFormat(number, "0,0.00");
        }
    }
}
