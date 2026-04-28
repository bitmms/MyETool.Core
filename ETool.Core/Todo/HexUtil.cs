using System;
using System.Text;
using ETool.Core.Util;

namespace ETool.Core.Todo.ToolCategory
{
    /// <summary>
    /// 16进制工具类
    /// </summary>
    public class HexUtil
    {
        /// <summary>
        /// 将16进制字符串转换为字节数组
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <returns>字节数组</returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] HexToBytes(string hex)
        {
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
                throw new ArgumentException("Hex string must have an even length");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }

        /// <summary>
        /// 将字节数组转换为16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>16进制字符串</returns>
        public static string BytesToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        /// <summary>
        /// 比较两个16进制字符串是否相等
        /// </summary>
        /// <param name="hex1">16进制字符串</param>
        /// <param name="hex2">16进制字符串</param>
        /// <returns>是否相等</returns>
        public static bool HexEquals(string hex1, string hex2)
        {
            byte[] bytes1 = HexToBytes(hex1);
            byte[] bytes2 = HexToBytes(hex2);

            if (bytes1.Length != bytes2.Length)
                return false;

            for (int i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] != bytes2[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 将16进制字符串转换为十进制数值
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <returns>十进制数值</returns>
        public static int HexToInt(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }

        /// <summary>
        /// 将十进制数值转换为16进制字符串
        /// </summary>
        /// <param name="number">十进制数值</param>
        /// <returns>16进制字符串</returns>
        public static string IntToHex(int number)
        {
            return number.ToString("X");
        }

        /// <summary>
        /// 获取16进制字符串中指定位置的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <returns>字符</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static char GetHexChar(string hex, int index)
        {
            hex = hex.ToUpper();
            if (index < 0 || index >= hex.Length)
                throw new IndexOutOfRangeException("Index must be within the range of the hex string");

            return hex[index];
        }

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
        /// 将16进制字符串中指定位置的字符替换为新的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <param name="newInt">字符</param>
        /// <returns>新16进制字符串</returns>
        public static string ReplaceHexChar(string hex, int index, int newInt)
        {
            string newHexChar = newInt.ToString("X");
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
        /// 在16进制字符串的指定位置插入新的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <param name="newByte">字符</param>
        /// <returns>新16进制字符串></returns>
        public static string InsertHexChar(string hex, int index, byte newByte)
        {
            string newHexChar = newByte.ToString("X2");
            return InsertHexChar(hex, index, newHexChar);
        }

        /// <summary>
        /// 在16进制字符串的指定位置插入新的字符
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <param name="newInt">字符</param>
        /// <returns>新16进制字符串</returns>
        public static string InsertHexChar(string hex, int index, int newInt)
        {
            string newHexChar = newInt.ToString("X");
            return InsertHexChar(hex, index, newHexChar);
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

            char hexChar = GetHexChar(hex, fromIndex);
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
                throw new IndexOutOfRangeException("Index must be within the range of the hex string");

            int newIndex = index + steps;
            if (newIndex < 0 || newIndex >= hex.Length)
                throw new IndexOutOfRangeException("New index must be within the range of the hex string");

            char hexChar = GetHexChar(hex, index);
            hex = RemoveHexChar(hex, index);
            hex = InsertHexChar(hex, newIndex, hexChar);

            return hex;
        }

        /// <summary>
        /// 将16进制字符串中指定位置的字符向左旋转1位
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static string RotateHexCharLeft(string hex, int index)
        {
            if (index < 0 || index >= hex.Length)
                throw new IndexOutOfRangeException("Index must be within the range of the hex string");

            char hexChar = GetHexChar(hex, index);
            hex = RemoveHexChar(hex, index);
            hex += hexChar;

            return hex;
        }

        /// <summary>
        /// 将16进制字符串中指定位置的字符向右旋转1位
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <param name="index">位置下标</param>
        /// <returns>新16进制字符串</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static string RotateHexCharRight(string hex, int index)
        {
            if (index < 0 || index >= hex.Length)
                throw new IndexOutOfRangeException("Index must be within the range of the hex string");

            char hexChar = GetHexChar(hex, index);
            hex = RemoveHexChar(hex, index);
            hex = hexChar + hex;

            return hex;
        }
    }
}
