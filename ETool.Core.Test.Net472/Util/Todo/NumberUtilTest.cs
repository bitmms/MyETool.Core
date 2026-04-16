using System.Collections.Generic;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util
{
    public class NumberUtilTest
    {
        /*public static IEnumerable<object[]> AddTestData()
        {
            // ✅ 正数 + 正数（含动态超大数）
            yield return new object[] { "1", "2", "3", "1 + 2 应等于 3" };
            /*yield return new object[] { "999", "1", "1000", "999 + 1 应等于 1000" };
            yield return new object[] { "123456789", "987654321", "1111111110", "大正数相加应正确进位" };
            yield return new object[] { "999999999999999999999999999999", "1", "1000000000000000000000000000000", "极大数加1应正确" };
            // 动态生成100位超大数（解决InlineData常量限制）
            var bigNum1 = "1" + new string('9', 99);
            var bigNum1Result = "2" + new string('0', 99);
            yield return new object[] { bigNum1, "1", bigNum1Result, "100位极大正数相加应正确进位" };
            yield return new object[] { "12345678901234567890123456789012345678901234567890", "98765432109876543210987654321098765432109876543210", "111111111011111111101111111110111111111011111111100", "50位超大正数相加应正确" };
            yield return new object[] { "9223372036854775807", "1", "9223372036854775808", "long最大值+1应正确（超出long范围仍计算）" };

            // ✅ 正数 + 负数（结果为正）
            yield return new object[] { "10", "-3", "7", "10 + (-3) 应等于 7" };
            yield return new object[] { "1000", "-1", "999", "1000 + (-1) 应等于 999" };
            yield return new object[] { "500", "-499", "1", "差值为1的正负相加应得1" };
            yield return new object[] { "1000000000000000000000000000000", "-999999999999999999999999999999", "1", "极大正数+极大负数（结果为1）应正确" };

            // ✅ 正数 + 负数（结果为负）
            yield return new object[] { "3", "-10", "-7", "3 + (-10) 应等于 -7" };
            yield return new object[] { "1", "-999", "-998", "小正数加大负数应得负数" };
            yield return new object[] { "999999999999999999999999999999", "-1000000000000000000000000000000", "-1", "极大正数+极大负数（结果为-1）应正确" };

            // ✅ 正数 + 负数（结果为零）
            yield return new object[] { "123", "-123", "0", "互为相反数相加应为0" };
            yield return new object[] { "1", "-1", "0", "1 + (-1) 应等于 0" };
            yield return new object[] { "9223372036854775807", "-9223372036854775807", "0", "long最大值+其相反数应等于0" };

            // ✅ 负数 + 正数（结果为负）
            yield return new object[] { "-10", "3", "-7", "-10 + 3 应等于 -7" };
            yield return new object[] { "-1000", "1", "-999", "-1000 + 1 应等于 -999" };
            yield return new object[] { "-1000000000000000000000000000000", "999999999999999999999999999999", "-1", "极大负数+极大正数（结果为-1）应正确" };

            // ✅ 负数 + 正数（结果为正）
            yield return new object[] { "-3", "10", "7", "-3 + 10 应等于 7" };
            yield return new object[] { "-1", "999", "998", "大正数加小负数应得正数" };
            yield return new object[] { "-999999999999999999999999999999", "1000000000000000000000000000000", "1", "极大负数+极大正数（结果为1）应正确" };

            // ✅ 负数 + 正数（结果为零）
            yield return new object[] { "-555", "555", "0", "互为相反数相加应为0" };
            yield return new object[] { "-9223372036854775808", "9223372036854775808", "0", "long最小值+其相反数应等于0" };

            // ✅ 负数 + 负数
            yield return new object[] { "-1", "-2", "-3", "-1 + (-2) 应等于 -3" };
            yield return new object[] { "-999", "-1", "-1000", "两个负数相加应为更小的负数" };
            yield return new object[] { "-123456789", "-987654321", "-1111111110", "大负数相加应正确" };
            yield return new object[] { "-999999999999999999999999999999", "-1", "-1000000000000000000000000000000", "极大负数加-1应正确" };
            yield return new object[] { "-12345678901234567890123456789012345678901234567890", "-98765432109876543210987654321098765432109876543210", "-111111111011111111101111111110111111111011111111100", "50位超大负数相加应正确" };
            yield return new object[] { "-9223372036854775808", "-1", "-9223372036854775809", "long最小值-1应正确（超出long范围仍计算）" };

            // ✅ 零值处理
            yield return new object[] { "0", "0", "0", "0 + 0 应等于 0" };
            yield return new object[] { "0", "123", "123", "0 + 正数 应等于该正数" };
            yield return new object[] { "456", "0", "456", "正数 + 0 应等于该正数" };
            yield return new object[] { "0", "-789", "-789", "0 + 负数 应等于该负数" };
            yield return new object[] { "-100", "0", "-100", "负数 + 0 应等于该负数" };
            yield return new object[] { "0", "-0", "", "0 + '-0'（非法负零）应等于空字符串" };

            // ❌ 非法输入：返回空字符串 - 基础非法场景
            yield return new object[] { "", "123", "", "空字符串作为输入应返回空" };
            yield return new object[] { "123", "", "", "空字符串作为输入应返回空" };
            yield return new object[] { "abc", "123", "", "非数字字符串应返回空" };
            yield return new object[] { "12a", "34", "", "含字母的字符串应返回空" };
            yield return new object[] { "-", "5", "", "单独 '-' 不是合法整数" };
            yield return new object[] { "01", "2", "", "'01' 含前导零，不是合法正整数" };
            yield return new object[] { "-01", "2", "", "'-01' 含前导零，不是合法负整数" };
            yield return new object[] { "--5", "3", "", "双重负号非法" };
            yield return new object[] { "+-5", "3", "", "混合符号非法" };
            yield return new object[] { " ", "5", "", "空白字符串非法" };
            yield return new object[] { "123", "00", "", "'00' 非法（前导零且非单个0）" };
            yield return new object[] { "0", "00", "", "'00' 非法" };

            // ❌ 非法输入：返回空字符串 - Null输入场景
            yield return new object[] { null, "123", "", "null作为第一个输入应返回空" };
            yield return new object[] { "123", null, "", "null作为第二个输入应返回空" };
            yield return new object[] { null, null, "", "两个null输入应返回空" };

            // ❌ 非法输入：返回空字符串 - 特殊字符/格式变种
            yield return new object[] { "１２３", "456", "", "全角数字字符串非法" };
            yield return new object[] { "123", "４５６", "", "全角数字字符串非法" };
            yield return new object[] { "1,234", "567", "", "含千分位逗号的数字非法" };
            yield return new object[] { "123", "5,678", "", "含千分位逗号的数字非法" };
            yield return new object[] { "123.45", "67", "", "含小数点的字符串不是整数，应返回空" };
            yield return new object[] { "123", "67.89", "", "含小数点的字符串不是整数，应返回空" };
            yield return new object[] { " 123 ", "45", "", "数字前后含空格非法" };
            yield return new object[] { "123", " 45 ", "", "数字前后含空格非法" };
            yield return new object[] { "\t123", "45", "", "含制表符的输入非法" };
            yield return new object[] { "123", "\n45", "", "含换行符的输入非法" };
            yield return new object[] { "000123", "45", "", "多个前导零非法" };
            yield return new object[] { "123", "-00045", "", "负数含多个前导零非法" };
            yield return new object[] { "+123", "45", "", "'+123' 以+开头非法（仅允许-开头）" };
            yield return new object[] { "123", "+45", "", "'+45' 以+开头非法（仅允许-开头）" };
            yield return new object[] { "0000", "1", "", "'0000' 多个零非法（非单个0）" };
            yield return new object[] { "123_456", "78", "", "含下划线的数字字符串非法" };
            yield return new object[] { "\0", "5", "", "空字符（\\0）非法" };
            yield return new object[] { "123", "\0", "", "空字符（\\0）非法" };
            // yield return new object[] { "９９９", "888", "", "全角数字（９９９）非法" };
            yield return new object[] { "∞", "123", "", "特殊符号（无穷大）非法" };
            yield return new object[] { "123", "Ⅷ", "", "罗马数字非法" };

            // ❌ 非法输入：返回空字符串 - 边界非法场景
            yield return new object[] { "9223372036854775807a", "1", "", "long最大值后加字母非法" };
            yield return new object[] { "-9223372036854775808-", "1", "", "long最小值后加符号非法" };
            yield return new object[] { "0", "00000", "", "多个零（00000）非法" };#1#
        }

        [Theory]
        [MemberData(nameof(AddTestData))]
        public void AddTest(string n1, string n2, string expected, string message)
        {
            string actual = NumberUtil.Add(n1, n2);
            Assert.True(expected == actual, message);
        }

        public static IEnumerable<object[]> SubTestData()
        {
            // ✅ 0 相关
            yield return new object[] { "0", "0", "0", "0 - 0 = 0" };
            yield return new object[] { "0", "5", "-5", "0 - 5 = -5" };
            yield return new object[] { "0", "-3", "3", "0 - (-3) = 3" };
            yield return new object[] { "7", "0", "7", "7 - 0 = 7" };
            yield return new object[] { "-4", "0", "-4", "-4 - 0 = -4" };

            // ✅ 正 - 正（a > b）
            yield return new object[] { "10", "3", "7", "10 - 3 = 7" };
            yield return new object[] { "100", "1", "99", "100 - 1 = 99" };
            yield return new object[] { "999999999", "123456789", "876543210", "大正数相减应正确" };

            // ✅ 正 - 正（a < b）→ 负结果
            yield return new object[] { "3", "10", "-7", "3 - 10 = -7" };
            yield return new object[] { "1", "100", "-99", "1 - 100 = -99" };
            yield return new object[] { "123456789", "999999999", "-876543210", "小正减大正得负" };

            // ✅ 正 - 正（相等）
            yield return new object[] { "5", "5", "0", "5 - 5 = 0" };
            yield return new object[] { "999999999999", "999999999999", "0", "大数相减相等得0" };

            // ✅ 正 - 负 → 加法
            yield return new object[] { "5", "-3", "8", "5 - (-3) = 8" };
            yield return new object[] { "100", "-1", "101", "100 - (-1) = 101" };
            yield return new object[] { "123456789", "-987654321", "1111111110", "正减大负数得大正" };

            // ✅ 负 - 正 → 更负
            yield return new object[] { "-5", "3", "-8", "-5 - 3 = -8" };
            yield return new object[] { "-100", "1", "-101", "-100 - 1 = -101" };
            yield return new object[] { "-123456789", "987654321", "-1111111110", "负减正得更负" };

            // ✅ 负 - 负（|n1| > |n2|）→ 负结果
            yield return new object[] { "-10", "-3", "-7", "-10 - (-3) = -7" };
            yield return new object[] { "-1000", "-1", "-999", "-1000 - (-1) = -999" };

            // ✅ 负 - 负（|n1| < |n2|）→ 正结果
            yield return new object[] { "-3", "-10", "7", "-3 - (-10) = 7" };
            yield return new object[] { "-1", "-100", "99", "-1 - (-100) = 99" };

            // ✅ 负 - 负（相等）
            yield return new object[] { "-777", "-777", "0", "-777 - (-777) = 0" };

            // ✅ 超大数减法
            yield return new object[] { "1000000000000000000000000000000", "1", "999999999999999999999999999999", "极大数减1" };
            yield return new object[] { "-1000000000000000000000000000000", "-1", "-999999999999999999999999999999", "极大负数减(-1)" };
            yield return new object[] { "123456789012345678901234567890", "98765432109876543210987654321", "24691356902469135690246913569", "50位大数相减" };

            // ❌ 非法输入（返回空字符串）
            yield return new object[] { "abc", "1", "", "非数字输入应返回空" };
            yield return new object[] { "01", "2", "", "前导零非法" };
            yield return new object[] { "1", "-0", "", "'-0' 非法（假设 Validator 拒绝）" };
            yield return new object[] { null, "1", "", "null 输入应返回空" };
            yield return new object[] { "123", "", "", "空字符串输入应返回空" };
            yield return new object[] { "--5", "3", "", "双重负号非法" };
            yield return new object[] { "12.3", "4", "", "含小数点非法" };
        }

        [Theory]
        [MemberData(nameof(SubTestData))]
        public void SubTest(string n1, string n2, string expected, string message)
        {
            string actual = NumberUtil.Sub(n1, n2);
            Assert.True(expected == actual, message);
        }

        public static IEnumerable<object[]> MulTestData()
        {
            // ✅ 正数 × 正数（基础）
            yield return new object[] { "1", "1", "1", "1 × 1 = 1" };
            yield return new object[] { "2", "3", "6", "2 × 3 = 6" };
            yield return new object[] { "10", "10", "100", "10 × 10 = 100" };
            yield return new object[] { "999", "999", "998001", "999 × 999 = 998001" };

            // ✅ 正数 × 正数（超大数）
            yield return new object[] { "123456789", "987654321", "121932631112635269", "大正数相乘应正确" };
            yield return new object[] { "99999999999999999999", "99999999999999999999", "9999999999999999999800000000000000000001", "20位全9相乘" };
            var big100 = new string('9', 100);
            var big100Result = "99999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999980000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001";
            yield return new object[] { big100, big100, big100Result, "100位全9相乘应正确" };

            // ✅ 正数 × 负数（结果为负）
            yield return new object[] { "5", "-3", "-15", "5 × (-3) = -15" };
            yield return new object[] { "100", "-1", "-100", "100 × (-1) = -100" };
            yield return new object[] { "123456789", "-987654321", "-121932631112635269", "大正数 × 大负数" };
            yield return new object[] { "999999999999999999999999999999", "-2", "-1999999999999999999999999999998", "极大正数 × -2" };

            // ✅ 负数 × 正数（结果为负）
            yield return new object[] { "-7", "8", "-56", "-7 × 8 = -56" };
            yield return new object[] { "-1", "999", "-999", "-1 × 999 = -999" };
            yield return new object[] { "-12345678901234567890", "10", "-123456789012345678900", "负大数 × 10" };

            // ✅ 负数 × 负数（结果为正）
            yield return new object[] { "-2", "-3", "6", "(-2) × (-3) = 6" };
            yield return new object[] { "-100", "-50", "5000", "两个负数相乘为正" };
            yield return new object[] { "-99999999999999999999", "-99999999999999999999", "9999999999999999999800000000000000000001", "两个极大负数相乘为正" };

            // ✅ 与零相乘（任何数 × 0 = 0）
            yield return new object[] { "0", "0", "0", "0 × 0 = 0" };
            yield return new object[] { "0", "123", "0", "0 × 正数 = 0" };
            yield return new object[] { "456", "0", "0", "正数 × 0 = 0" };
            yield return new object[] { "0", "-789", "0", "0 × 负数 = 0" };
            yield return new object[] { "-100", "0", "0", "负数 × 0 = 0" };
            yield return new object[] { "9223372036854775807", "0", "0", "long最大值 × 0 = 0" };
            yield return new object[] { "-9223372036854775808", "0", "0", "long最小值 × 0 = 0" };

            // ✅ 乘以1或-1（单位元）
            yield return new object[] { "12345678901234567890", "1", "12345678901234567890", "任何数 × 1 = 自身" };
            yield return new object[] { "-98765432109876543210", "1", "-98765432109876543210", "负数 × 1 = 自身" };
            yield return new object[] { "12345678901234567890", "-1", "-12345678901234567890", "正数 × (-1) = 相反数" };
            yield return new object[] { "-98765432109876543210", "-1", "98765432109876543210", "负数 × (-1) = 相反数" };

            // ❌ 非法输入：返回空字符串（复用 Add 中的非法规则）
            yield return new object[] { "", "123", "", "空字符串作为输入应返回空" };
            yield return new object[] { "123", "", "", "空字符串作为输入应返回空" };
            yield return new object[] { "abc", "123", "", "非数字字符串应返回空" };
            yield return new object[] { "12a", "34", "", "含字母的字符串应返回空" };
            yield return new object[] { "-", "5", "", "单独 '-' 不是合法整数" };
            yield return new object[] { "01", "2", "", "'01' 含前导零，不是合法正整数" };
            yield return new object[] { "-01", "2", "", "'-01' 含前导零，不是合法负整数" };
            yield return new object[] { "--5", "3", "", "双重负号非法" };
            yield return new object[] { "+5", "3", "", "'+5' 以+开头非法（仅允许-开头）" };
            yield return new object[] { " 123 ", "45", "", "数字前后含空格非法" };
            yield return new object[] { "123.45", "67", "", "含小数点的字符串不是整数" };
            yield return new object[] { "1,234", "567", "", "含千分位逗号的数字非法" };
            yield return new object[] { "00", "5", "", "'00' 非法（前导零且非单个0）" };
            yield return new object[] { null, "123", "", "null 输入应返回空" };
            yield return new object[] { "123", null, "", "null 输入应返回空" };
            yield return new object[] { "0", "-0", "", "'-0' 非法（不允许负零）" };
            yield return new object[] { "123_456", "78", "", "含下划线的数字字符串非法" };
        }

        [Theory]
        [MemberData(nameof(MulTestData))]
        public void MulTest(string n1, string n2, string expected, string message)
        {
            string actual = NumberUtil.Mul(n1, n2);
            Assert.True(expected == actual, message);
        }*/
    }
}
