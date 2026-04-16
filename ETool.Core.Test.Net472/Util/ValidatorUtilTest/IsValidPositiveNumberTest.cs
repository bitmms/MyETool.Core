using System.Diagnostics;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsValidPositiveNumberTest
    {
        /// <summary>
        /// 测试合法的正整数
        /// </summary>
        [Theory]
        [InlineData("13711112221", true, "标准正整数应验证成功")]
        [InlineData("1", true, "标准正整数应验证成功")]
        [InlineData("10", true, "标准正整数应验证成功")]
        [InlineData("123", true, "标准正整数应验证成功")]
        [InlineData("999", true, "标准正整数应验证成功")]
        [InlineData("99999999999999999999", true, "标准大数字应验证成功")]
        public void IsValidPositiveNumber_ValidNumber_ReturnsTrue(string input, bool expectedResult, string errorMessage)
        {
            var result = ValidatorUtil.IsValidPositiveNumber(input);
            Assert.True(result == expectedResult, errorMessage);
        }

        /// <summary>
        /// 全角半角字符测试
        /// </summary>
        [Theory]
        [InlineData("1371１112246", false, "包含全角字符『１』应验证失败")]
        [InlineData("1371１11224２", false, "包含全角字符『２』应验证失败")]
        public void IsValidIpv4_FullWidthCharacters_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidPositiveNumber(input);
            Assert.True(result == expected, errorMessage);
        }
        
        /// <summary>
        /// 综合测试所有不合法的正整数输入
        /// </summary>
        [Theory]
        // 空值 / 空白
        [InlineData(null, false, "null 应验证失败")]
        [InlineData("", false, "空字符串应验证失败")]
        [InlineData("   ", false, "仅包含空格的字符串应验证失败")]
        [InlineData("\t", false, "制表符应验证失败")]
        [InlineData("\r\n", false, "换行符应验证失败")]
        // 前导零（包括单独的零）
        [InlineData("0", false, "单独的零（前导零）应验证失败")]
        [InlineData("01", false, "前导零「01」应验证失败")]
        [InlineData("00123", false, "前导零「00123」应验证失败")]
        [InlineData("000", false, "多个零「000」应验证失败")]
        // 包含非数字字符
        [InlineData("12a", false, "包含字母「12a」应验证失败")]
        [InlineData("a12", false, "以字母开头「a12」应验证失败")]
        [InlineData("1 2", false, "包含空格「1 2」应验证失败")]
        [InlineData("1-2", false, "包含连字符「1-2」应验证失败")]
        [InlineData("1.2", false, "包含小数点「1.2」应验证失败")]
        [InlineData("+123", false, "包含正号「+123」应验证失败")]
        [InlineData("-123", false, "包含负号「-123」应验证失败")]
        [InlineData("1,000", false, "包含千位分隔符「1,000」应验证失败")]
        [InlineData("1/2", false, "包含斜杠「1/2」应验证失败")]
        [InlineData("①②③", false, "全角数字（圆圈数字）应验证失败")]
        [InlineData(" ", false, "单个空格应验证失败")]
        public void IsValidPositiveNumber_InvalidCases_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidPositiveNumber(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 边界值测试：长度极长但内容全为数字的字符串（无前导零）
        /// </summary>
        [Fact]
        public void IsValidPositiveNumber_VeryLongNumber_ReturnsTrue()
        {
            // 构造一个 1000 位的数字字符串（全部为 '1'）
            var longNumber = new string('1', 100000);
            var result = ValidatorUtil.IsValidPositiveNumber(longNumber);
            Assert.True(result, "超长纯数字字符串（无前导零）应验证通过");
        }

        /// <summary>
        /// 边界值测试：长度极长但内容全为数字的字符串 + 前导零
        /// </summary>
        [Fact]
        public void IsValidPositiveNumber_LongNumberWithLeadingZero_ReturnsFalse()
        {
            var numberWithLeadingZero = "0" + new string('1', 100000);
            var result = ValidatorUtil.IsValidPositiveNumber(numberWithLeadingZero);
            Assert.False(result, "前导零的超长数字字符串应验证失败");
        }

        /// <summary>
        /// 性能压力测试
        /// </summary>
        [Fact]
        public void IsValidPositiveNumber_Performance_LargeVolume()
        {
            const int expectedMillisecond = 1 * 1000;
            const int count = 100_0000;
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < count; i++)
            {
                ValidatorUtil.IsValidPositiveNumber("999999999999999999999");
            }

            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds < expectedMillisecond, $"验证 {count} 个字符串耗时 {stopwatch.ElapsedMilliseconds}ms，超出预期的 {expectedMillisecond}ms");
        }
    }
}
