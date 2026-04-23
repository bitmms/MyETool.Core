using System.Diagnostics;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsValidQqNumberTest
    {
        /// <summary>
        /// 测试合法的标准 QQ 号码
        /// </summary>
        [Theory]
        [InlineData("10000", true, "5位标准QQ号应验证成功")]
        [InlineData("123456", true, "6位标准QQ号应验证成功")]
        [InlineData("1234567", true, "7位标准QQ号应验证成功")]
        [InlineData("12345678", true, "8位标准QQ号应验证成功")]
        [InlineData("123456789", true, "9位标准QQ号应验证成功")]
        [InlineData("1234567890", true, "10位标准QQ号应验证成功")]
        [InlineData("12345678901", true, "11位标准QQ号应验证成功")]
        [InlineData("100000000", true, "真实QQ号示例应验证成功")]
        [InlineData("800800800", true, "真实QQ号示例应验证成功")]
        [InlineData("9999999999", true, "9开头的10位QQ号应验证成功")]
        [InlineData("99999999999", true, "9开头的11位QQ号应验证成功")]
        public void IsValidQqNumber_ValidQqNumbers_ReturnsTrue(string input, bool expectedResult, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(result == expectedResult, errorMessage);
        }

        /// <summary>
        /// 全角半角字符测试
        /// </summary>
        [Theory]
        [InlineData("1565１656", false, "包含全角字符『１』应验证失败")]
        [InlineData("134254224２", false, "包含全角字符『２』应验证失败")]
        public void IsValidIpv4_FullWidthCharacters_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 空值和空白
        /// </summary>
        [Theory]
        [InlineData(null, false, "null 应验证失败")]
        [InlineData("", false, "空字符串应验证失败")]
        [InlineData("   ", false, "仅包含空格的字符串应验证失败")]
        [InlineData("\t", false, "制表符应验证失败")]
        [InlineData("\r\n", false, "换行符应验证失败")]
        [InlineData(" ", false, "单个空格应验证失败")]
        [InlineData("  10000  ", false, "前后带空格应验证失败")]
        public void IsValidQqNumber_NullOrWhitespace_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 长度不符
        /// </summary>
        [Theory]
        [InlineData("1", false, "1位数字应验证失败")]
        [InlineData("12", false, "2位数字应验证失败")]
        [InlineData("123", false, "3位数字应验证失败")]
        [InlineData("1234", false, "4位数字应验证失败")]
        [InlineData("123456789012", false, "12位数字应验证失败")]
        [InlineData("1234567890123", false, "13位数字应验证失败")]
        [InlineData("99999999999999", false, "过长的数字应验证失败")]
        public void IsValidQqNumber_InvalidLength_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 首位为0或其他非1-9数字
        /// </summary>
        [Theory]
        [InlineData("00000", false, "首位为0的5位数应验证失败")]
        [InlineData("01234", false, "首位为0的QQ号应验证失败")]
        [InlineData("012345", false, "首位为0的6位号应验证失败")]
        [InlineData("0123456789", false, "首位为0的10位号应验证失败")]
        public void IsValidQqNumber_LeadingZero_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 包含非数字字符
        /// </summary>
        [Theory]
        [InlineData("1000a", false, "包含字母「1000a」应验证失败")]
        [InlineData("a10000", false, "以字母开头「a10000」应验证失败")]
        [InlineData("100 00", false, "中间包含空格「100 00」应验证失败")]
        [InlineData("10000 ", false, "末尾带空格应验证失败")]
        [InlineData(" 10000", false, "开头带空格应验证失败")]
        [InlineData("1000.0", false, "包含小数点应验证失败")]
        [InlineData("1000-0", false, "包含连字符应验证失败")]
        [InlineData("10,000", false, "包含千位分隔符应验证失败")]
        [InlineData("1000/0", false, "包含斜杠应验证失败")]
        [InlineData("①0000", false, "包含全角数字应验证失败")]
        [InlineData("+10000", false, "包含正号应验证失败")]
        [InlineData("-10000", false, "包含负号应验证失败")]
        [InlineData("1q0000", false, "中间包含字母应验证失败")]
        public void IsValidQqNumber_InvalidCharacters_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 边界值测试 - 最小有效值
        /// </summary>
        [Fact]
        public void IsValidQqNumber_MinimumValidLength_ReturnsTrue()
        {
            var result = ValidatorUtil.IsQqNumber("10000");
            Assert.True(result, "最小长度（5位）的有效QQ号应验证成功");
        }

        /// <summary>
        /// 边界值测试 - 最大有效值
        /// </summary>
        [Fact]
        public void IsValidQqNumber_MaximumValidLength_ReturnsTrue()
        {
            var result = ValidatorUtil.IsQqNumber("12345678901");
            Assert.True(result, "最大长度（11位）的有效QQ号应验证成功");
        }

        /// <summary>
        /// 边界值测试 - 最小长度减1（应失败）
        /// </summary>
        [Fact]
        public void IsValidQqNumber_BelowMinimumLength_ReturnsFalse()
        {
            var result = ValidatorUtil.IsQqNumber("1234");
            Assert.False(result, "小于最小长度（5位）应验证失败");
        }

        /// <summary>
        /// 边界值测试 - 最大长度加1（应失败）
        /// </summary>
        [Fact]
        public void IsValidQqNumber_ExceedsMaximumLength_ReturnsFalse()
        {
            var result = ValidatorUtil.IsQqNumber("123456789012");
            Assert.False(result, "超过最大长度（11位）应验证失败");
        }

        /// <summary>
        /// 边界值测试 - 首位为1到9的各种情况
        /// </summary>
        [Theory]
        [InlineData("10000", true, "首位为1应验证成功")]
        [InlineData("20000", true, "首位为2应验证成功")]
        [InlineData("30000", true, "首位为3应验证成功")]
        [InlineData("40000", true, "首位为4应验证成功")]
        [InlineData("50000", true, "首位为5应验证成功")]
        [InlineData("60000", true, "首位为6应验证成功")]
        [InlineData("70000", true, "首位为7应验证成功")]
        [InlineData("80000", true, "首位为8应验证成功")]
        [InlineData("90000", true, "首位为9应验证成功")]
        public void IsValidQqNumber_VariousLeadingDigits_ReturnsTrue(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(expected == result, errorMessage);
        }

        /// <summary>
        /// 综合真实场景测试
        /// </summary>
        [Theory]
        [InlineData("10001", true, "真实QQ示例1")]
        [InlineData("123456", true, "真实QQ示例2")]
        [InlineData("88888888", true, "真实QQ示例3（吉祥号）")]
        [InlineData("666666666", true, "真实QQ示例4（长号）")]
        [InlineData("999999999", true, "真实QQ示例5")]
        [InlineData("10000a", false, "携带字母的非有效号")]
        [InlineData("1000", false, "过短的号码")]
        [InlineData("100000000000", false, "过长的号码")]
        public void IsValidQqNumber_RealWorldScenarios_ReturnsExpected(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(expected == result, errorMessage);
        }

        /// <summary>
        /// 性能压力测试 - 有效QQ号
        /// </summary>
        [Fact]
        public void IsValidQqNumber_Performance_LargeVolume_ValidQq()
        {
            const int expectedMillisecond = 1 * 1000;
            const int count = 100_0000;
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < count; i++)
            {
                ValidatorUtil.IsQqNumber("123456789");
            }

            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds < expectedMillisecond,
                $"验证 {count} 个有效QQ号耗时 {stopwatch.ElapsedMilliseconds}ms，超出预期的 {expectedMillisecond}ms");
        }

        /// <summary>
        /// 性能压力测试 - 无效QQ号
        /// </summary>
        [Fact]
        public void IsValidQqNumber_Performance_LargeVolume_InvalidQq()
        {
            const int expectedMillisecond = 1 * 1000;
            const int count = 100_0000;
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < count; i++)
            {
                ValidatorUtil.IsQqNumber("abc");
            }

            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds < expectedMillisecond,
                $"验证 {count} 个无效QQ号耗时 {stopwatch.ElapsedMilliseconds}ms，超出预期的 {expectedMillisecond}ms");
        }

        /// <summary>
        /// 所有可能的边界长度测试
        /// </summary>
        [Theory]
        [InlineData("1234", false, "4位应失败")]
        [InlineData("12340", true, "5位应成功")]
        [InlineData("123401", true, "6位应成功")]
        [InlineData("1234012", true, "7位应成功")]
        [InlineData("12340123", true, "8位应成功")]
        [InlineData("123401234", true, "9位应成功")]
        [InlineData("1234012345", true, "10位应成功")]
        [InlineData("12340123456", true, "11位应成功")]
        [InlineData("123401234567", false, "12位应失败")]
        public void IsValidQqNumber_AllBoundaryLengths_ReturnsExpected(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsQqNumber(input);
            Assert.True(expected == result, errorMessage);
        }
    }
}
