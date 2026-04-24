using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyTool
{
    /// <summary>
    /// 字符串处理工具类
    /// </summary>
    public class StrUtil
    {
        /// <summary>
        /// 将字符串中的指定字符替换成新的字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="oldChar">要替换的字符</param>
        /// <param name="newChar">新的字符</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceChar(string str, char oldChar, char newChar)
        {
            return str.Replace(oldChar, newChar);
        }

        /// <summary>
        /// 检查字符串是否为日期
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是日期，则返回true，否则返回false</returns>
        public static bool IsDate(string str)
        {
            DateTime result;
            return DateTime.TryParse(str, out result);
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
        /// 截取字符串的指定部分
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="startIndex">起始位置（从0开始）</param>
        /// <param name="length">要截取的长度</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring(string str, int startIndex, int length)
        {
            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 将字符串中的某些字符替换成指定的字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="chars">要替换的字符数组</param>
        /// <param name="newChar">新的字符</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceChars(string str, char[] chars, char newChar)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                str = str.Replace(chars[i], newChar);
            }

            return str;
        }

        /// <summary>
        /// 将字符串中的某些子字符串替换成指定的子字符串
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="oldValues">要替换的子字符串数组</param>
        /// <param name="newValue">新的子字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceStrings(string str, string[] oldValues, string newValue)
        {
            for (int i = 0; i < oldValues.Length; i++)
            {
                str = str.Replace(oldValues[i], newValue);
            }

            return str;
        }

        /// <summary>
        /// 将字符串中的某些子字符串替换成指定的子字符串，忽略大小写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="oldValues">要替换的子字符串数组</param>
        /// <param name="newValue">新的子字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceStringsIgnoreCase(string str, string[] oldValues, string newValue)
        {
            for (int i = 0; i < oldValues.Length; i++)
            {
                str = Regex.Replace(str, oldValues[i], newValue, RegexOptions.IgnoreCase);
            }

            return str;
        }
    }
}
