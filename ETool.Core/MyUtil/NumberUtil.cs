using System;
using System.Text;
using ETool.Core.Util;

namespace ETool.Core.Todo.MyUtil
{
    /// <summary>
    /// 整数工具类
    /// </summary>
    public static class NumberUtil
    {
        /// <summary>
        /// 比较两个正整数的大小【私有方法：可以确保 n1，n2均为正整数且不包含前导0】
        /// </summary>
        /// <param name="n1">第一个正整数</param>
        /// <param name="n2">第二个正整数</param>
        /// <returns>n1 大于 n2 返回 1；n1 小于 n2 返回 -1；否则返回 0</returns>
        private static int Compare(string n1, string n2)
        {
            if (n1.Length < n2.Length) return -1;
            if (n1.Length > n2.Length) return 1;
            for (int i = 0; i < n1.Length; i++)
            {
                if (n1[i] > n2[i]) return 1;
                if (n1[i] < n2[i]) return -1;
            }

            return 0;
        }

        /// <summary>
        /// 将两个正整数相加【私有方法：可以确保 n1，n2均为正整数且不包含前导0】
        /// </summary>
        /// <param name="n1">第一个正整数</param>
        /// <param name="n2">第二个正整数</param>
        /// <returns>正整数相加的和</returns>
        private static string AddPositive(string n1, string n2)
        {
            byte[] arrA = new byte[n1.Length];
            byte[] arrB = new byte[n2.Length];
            for (int i = n1.Length - 1, j = 0; i >= 0; i--, j++) arrA[j] = (byte)(n1[i] - '0');
            for (int i = n2.Length - 1, j = 0; i >= 0; i--, j++) arrB[j] = (byte)(n2[i] - '0');

            StringBuilder resultC = new StringBuilder(Math.Max(n1.Length, n2.Length) + 1);

            for (int i = 0, carry = 0; i < arrA.Length || i < arrB.Length || carry != 0; i++)
            {
                if (i < arrA.Length) carry += arrA[i];
                if (i < arrB.Length) carry += arrB[i];
                resultC.Append(carry % 10);
                carry /= 10;
            }

            char[] resultChars = resultC.ToString().ToCharArray();
            Array.Reverse(resultChars);

            return new string(resultChars);
        }

        /// <summary>
        /// 将两个正整数相减【私有方法：可以确保 n1，n2均为正整数且不包含前导0，并且 n1 >= n2】
        /// </summary>
        /// <param name="n1">第一个正整数</param>
        /// <param name="n2">第二个正整数</param>
        /// <returns>n1 减 n2 的差值</returns>
        private static string SubPositive(string n1, string n2)
        {
            byte[] arrA = new byte[n1.Length];
            byte[] arrB = new byte[n2.Length];
            for (int i = n1.Length - 1, j = 0; i >= 0; i--, j++) arrA[j] = (byte)(n1[i] - '0');
            for (int i = n2.Length - 1, j = 0; i >= 0; i--, j++) arrB[j] = (byte)(n2[i] - '0');

            StringBuilder resultC = new StringBuilder(n1.Length);

            for (int i = 0, t = 0; i < arrA.Length; i++)
            {
                t += arrA[i];
                if (i < arrB.Length) t -= arrB[i];
                resultC.Append((t + 10) % 10);
                if (t < 0) t = -1;
                else t = 0;
            }

            char[] resultChars = resultC.ToString().ToCharArray();
            Array.Reverse(resultChars);

            int leadingZeroCount = 0;
            while (leadingZeroCount < resultChars.Length && resultChars[leadingZeroCount] == '0')
            {
                leadingZeroCount++;
            }

            if (leadingZeroCount == resultChars.Length) return "0";

            return new string(resultChars, leadingZeroCount, resultChars.Length - leadingZeroCount);
        }

        /// <summary>
        /// 将两个正整数相乘【私有方法：可以确保 n1，n2均为正整数且不包含前导0，并且 n1 和 n2 的最大长度是 10000】
        /// </summary>
        /// <param name="n1">第一个正整数</param>
        /// <param name="n2">第二个正整数</param>
        /// <returns>n1 减 n2 的乘积</returns>
        private static string MulPositive(string n1, string n2)
        {
            // 从个位到高位排序
            byte[] arrA = new byte[n1.Length];
            byte[] arrB = new byte[n2.Length];
            for (int i = n1.Length - 1, j = 0; i >= 0; i--, j++) arrA[j] = (byte)(n1[i] - '0');
            for (int i = n2.Length - 1, j = 0; i >= 0; i--, j++) arrB[j] = (byte)(n2[i] - '0');

            // 保存结果【默认值均为0】
            // 999*999 < 1000 * 1000 -->> 1 * 10e6 = 7个
            int[] resultC = new int[n1.Length + n2.Length];

            // 依次按位相乘，得到从个位到高位排序的结果值
            for (int i = 0; i < n1.Length; i++)
            for (int j = 0; j < n2.Length; j++)
                resultC[i + j] += arrA[i] * arrB[j];

            // 进位处理【不像加法需要考虑最后一位的进位，这里可以保证最大是 m+n 位】
            for (int i = 0, t = 0; i < resultC.Length; i++)
            {
                t += resultC[i];
                resultC[i] = t % 10;
                t /= 10;
            }

            // 反转数组
            Array.Reverse(resultC);

            // 找到第一个 0 的位置
            int leadingZeroCount = 0;
            while (leadingZeroCount < resultC.Length && resultC[leadingZeroCount] == 0)
            {
                leadingZeroCount++;
            }

            if (leadingZeroCount == resultC.Length) return "0";

            StringBuilder sb = new StringBuilder();
            for (int i = leadingZeroCount; i < resultC.Length; i++)
            {
                sb.Append(resultC[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 高精度整数相加【支持负数，不支持前导零】
        /// </summary>
        /// <param name="n1">第一个整数</param>
        /// <param name="n2">第二个整数</param>
        /// <returns>整数相加的和</returns>
        public static string Add(string n1, string n2)
        {
            // 非合法整数返回空
            if (!ValidatorUtil.IsValidNumber(n1) || !ValidatorUtil.IsValidNumber(n2))
            {
                return "";
            }

            // 限制长度 ≤ 10,000 字符
            if (n1.Length > 10_000 || n2.Length > 10_000)
            {
                return "";
            }

            // 0值快速返回
            if (n1 == "0") return n2;
            if (n2 == "0") return n1;

            // 到此，n1，n2只能是正整数或负整数

            // n1>0, n2>0 → 直接相加
            if (ValidatorUtil.IsValidPositiveNumber(n1) && ValidatorUtil.IsValidPositiveNumber(n2))
            {
                return AddPositive(n1, n2);
            }

            // n1>0, n2<0 → 绝对值相减
            if (ValidatorUtil.IsValidPositiveNumber(n1) && !ValidatorUtil.IsValidPositiveNumber(n2))
            {
                string absN2 = n2.Substring(1); // n2 是负数，截取后为绝对值
                int compareResult = Compare(n1, absN2);
                if (compareResult > 0) return SubPositive(n1, absN2);
                if (compareResult < 0) return "-" + SubPositive(absN2, n1);
                return "0";
            }

            // n1<0, n2>0 → 绝对值相减
            if (!ValidatorUtil.IsValidPositiveNumber(n1) && ValidatorUtil.IsValidPositiveNumber(n2))
            {
                string absN1 = n1.Substring(1); // n1 是负数，截取后为绝对值
                int compareResult = Compare(absN1, n2);
                if (compareResult > 0) return "-" + SubPositive(absN1, n2);
                if (compareResult < 0) return SubPositive(n2, absN1);
                return "0";
            }

            // n1<0, n2<0 → 绝对值相加后加负号
            string abs1 = n1.Substring(1); // n1 是负数，截取后为绝对值
            string abs2 = n2.Substring(1); // n2 是负数，截取后为绝对值
            return "-" + AddPositive(abs1, abs2);
        }

        /// <summary>
        /// 用一个整数减去另一个整数【支持负数，不支持前导零】
        /// </summary>
        /// <param name="n1">第一个整数</param>
        /// <param name="n2">第二个整数</param>
        /// <returns>n1-n2的结果</returns>
        public static string Sub(string n1, string n2)
        {
            if (!ValidatorUtil.IsValidNumber(n1) || !ValidatorUtil.IsValidNumber(n2))
            {
                return "";
            }

            // 限制长度 ≤ 10,000 字符
            if (n1.Length > 10_000 || n2.Length > 10_000)
            {
                return "";
            }

            if (n1 == "0")
            {
                if (n2[0] == '-')
                {
                    return n2.Substring(1);
                }

                if (n2 == "0")
                {
                    return "0";
                }

                return "-" + n2;
            }

            if (n2 == "0")
            {
                return n1;
            }

            // 到此，n1，n2只能是正整数或负整数

            // n1>0, n2>0
            if (ValidatorUtil.IsValidPositiveNumber(n1) && ValidatorUtil.IsValidPositiveNumber(n2))
            {
                int compare = Compare(n1, n2);
                if (compare > 0) return SubPositive(n1, n2);
                if (compare < 0) return "-" + SubPositive(n2, n1);
                return "0";
            }

            // n1>0, n2<0
            if (ValidatorUtil.IsValidPositiveNumber(n1) && !ValidatorUtil.IsValidPositiveNumber(n2))
            {
                string absN2 = n2.Substring(1); // n2 是负数，截取后为绝对值
                return Add(n1, absN2);
            }

            // n1<0, n2>0
            if (!ValidatorUtil.IsValidPositiveNumber(n1) && ValidatorUtil.IsValidPositiveNumber(n2))
            {
                string absN1 = n1.Substring(1); // n1 是负数，截取后为绝对值
                return "-" + Add(absN1, n2);
            }

            // n1<0, n2<0
            string abs1 = n1.Substring(1); // n1 是负数，截取后为绝对值
            string abs2 = n2.Substring(1); // n2 是负数，截取后为绝对值
            int compareResult = Compare(abs2, abs1);
            if (compareResult > 0) return SubPositive(abs2, abs1);
            if (compareResult < 0) return "-" + SubPositive(abs1, abs2);
            return "0";
        }

        /// <summary>
        /// 两个整数相乘【支持负数，不支持前导零】
        /// </summary>
        /// <param name="n1">第一个整数</param>
        /// <param name="n2">第二个整数</param>
        /// <returns>n1 * n2的结果</returns>
        public static string Mul(string n1, string n2)
        {
            if (!ValidatorUtil.IsValidNumber(n1) || !ValidatorUtil.IsValidNumber(n2))
            {
                return "";
            }

            // 限制长度 ≤ 10,000 字符
            if (n1.Length > 10_000 || n2.Length > 10_000)
            {
                return "";
            }

            if (n1 == "0" || n2 == "0")
            {
                return "0";
            }

            // n1>0, n2>0
            if (ValidatorUtil.IsValidPositiveNumber(n1) && ValidatorUtil.IsValidPositiveNumber(n2))
            {
                return MulPositive(n1, n2);
            }

            // n1>0, n2<0
            if (ValidatorUtil.IsValidPositiveNumber(n1) && !ValidatorUtil.IsValidPositiveNumber(n2))
            {
                return "-" + MulPositive(n1, n2.Substring(1));
            }

            // n1<0, n2>0
            if (!ValidatorUtil.IsValidPositiveNumber(n1) && ValidatorUtil.IsValidPositiveNumber(n2))
            {
                return "-" + MulPositive(n1.Substring(1), n2);
            }

            // n1<0, n2<0
            return MulPositive(n1.Substring(1), n2.Substring(1));
        }
    }
}
