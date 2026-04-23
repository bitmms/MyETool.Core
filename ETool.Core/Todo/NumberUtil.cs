using System;

namespace ETool.Core.Todo
{
    /// <summary>
    /// 数字工具类，提供了多种对数字的操作方法
    /// </summary>
    public class NumberUtil
    {
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
    }
}
