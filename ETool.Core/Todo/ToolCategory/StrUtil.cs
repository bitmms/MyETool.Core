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
    }
}
