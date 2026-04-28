using System;
using System.Text;
using ETool.Core.Util;

namespace ETool.Core.Todo
{
    /// <summary>
    /// 16进制工具类
    /// </summary>
    public class HexUtil
    {
        /// <summary>
        /// 将16进制字符串中指定位置的字符替换为新的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">指定位置</param>
        /// <param name="newChar">替换字符</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ReplaceHexChar(string hex, int index, char newChar)
        {
            if (!ValidatorUtil.IsHexChar(newChar))
                throw new ArgumentException("New character must be a valid hex character");

            StringBuilder sb = new StringBuilder(hex);
            sb[index] = newChar.ToString().ToUpper()[0];

            return sb.ToString();
        }

        /// <summary>
        /// 将16进制字符串中指定位置的字符替换为新的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <param name="newHexChar">16进制字符</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ReplaceHexChar(string hex, int index, string newHexChar)
        {
            if (newHexChar.Length != 1 || !ValidatorUtil.IsHexChar(newHexChar[0]))
                throw new ArgumentException("New character must be a valid hex character");

            StringBuilder sb = new StringBuilder(hex);
            sb[index] = newHexChar.ToUpper()[0];

            return sb.ToString();
        }

        /// <summary>
        /// 将16进制字符串中指定位置的字符替换为新的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <param name="newByte">字符</param>
        /// <returns>新16进制字符串</returns>
        public static string ReplaceHexChar(string hex, int index, byte newByte)
        {
            string newHexChar = newByte.ToString("X2");
            return ReplaceHexChar(hex, index, newHexChar);
        }

        /// <summary>
        /// 在16进制字符串的指定位置插入新的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <param name="newChar">字符</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string InsertHexChar(string hex, int index, char newChar)
        {
            if (!ValidatorUtil.IsHexChar(newChar))
                throw new ArgumentException("New character must be a valid hex character");

            StringBuilder sb = new StringBuilder(hex);
            sb.Insert(index, newChar.ToString().ToUpper());

            return sb.ToString();
        }

        /// <summary>
        /// 在16进制字符串的指定位置插入新的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <param name="newHexChar">字符</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string InsertHexChar(string hex, int index, string newHexChar)
        {
            if (newHexChar.Length != 1 || !ValidatorUtil.IsHexChar(newHexChar[0]))
                throw new ArgumentException("New character must be a valid hex character");

            StringBuilder sb = new StringBuilder(hex);
            sb.Insert(index, newHexChar.ToUpper());

            return sb.ToString();
        }

        /// <summary>
        /// 从16进制字符串中移除指定位置的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static string RemoveHexChar(string hex, int index)
        {
            if (index < 0 || index >= hex.Length)
                throw new IndexOutOfRangeException("Index must be within the range of the hex string");

            StringBuilder sb = new StringBuilder(hex);
            sb.Remove(index, 1);

            return sb.ToString();
        }

        /// <summary>
        /// 将16进制字符串中指定位置的字符移动到新的位置
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="fromIndex">移动前位置</param>
        /// <param name="toIndex">移动后位置</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static string MoveHexChar(string hex, int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= hex.Length)
                throw new IndexOutOfRangeException("From index must be within the range of the hex string");
            if (toIndex < 0 || toIndex >= hex.Length)
                throw new IndexOutOfRangeException("To index must be within the range of the hex string");

            char hexChar = hex[fromIndex];
            hex = RemoveHexChar(hex, fromIndex);
            hex = InsertHexChar(hex, toIndex, hexChar);

            return hex;
        }

        /// <summary>
        /// 将16进制字符串中指定位置的字符移动指定的步长
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <param name="steps">步长</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static string ShiftHexChar(string hex, int index, int steps)
        {
            if (index < 0 || index >= hex.Length)
                throw new IndexOutOfRangeException("Index must be  within the range of the hex string");

            int newIndex = index + steps;
            if (newIndex < 0 || newIndex >= hex.Length)
                throw new IndexOutOfRangeException("New index must be within the range of the hex string");

            char hexChar = hex[index];
            hex = RemoveHexChar(hex, index);
            hex = InsertHexChar(hex, newIndex, hexChar);

            return hex;
        }
    }
}
