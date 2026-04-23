using System.Diagnostics;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsValidPhoneNumberTest
    {
        /// <summary>
        /// 测试合法的手机号
        /// </summary>
        [Theory]
        [InlineData("13711112221", true, "标准11位合法手机号应验证成功")]
        [InlineData("13711112222", true, "标准11位合法手机号应验证成功")]
        [InlineData("13711112223", true, "标准11位合法手机号应验证成功")]
        [InlineData("13711112246", true, "标准11位合法手机号应验证成功")]
        public void IsValidPhoneNumber_ValidStandardMobileNumber_ReturnsTrue(string input, bool expectedResult, string errorMessage)
        {
            var result = ValidatorUtil.IsPhoneNumber(input);
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
            var result = ValidatorUtil.IsPhoneNumber(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 综合测试所有不合法的手机号输入
        /// </summary>
        [Theory]
        // 空值/空白
        [InlineData(null, false, "null 应验证失败")]
        [InlineData("", false, "空字符串应验证失败")]
        [InlineData("   ", false, "仅包含空格的字符串应验证失败")]
        [InlineData("\t", false, "制表符应验证失败")]
        [InlineData("\r\n", false, "换行符应验证失败")]
        // 长度不合法
        [InlineData("10086", false, "5位客服号不应视为手机号")]
        [InlineData("1380013800", false, "10位号码应验证失败")]
        [InlineData("138001380000", false, "12位号码应验证失败")]
        [InlineData("1", false, "1位数字应验证失败")]
        [InlineData("12345678901234", false, "14位数字应验证失败")]
        // 首位不是1
        [InlineData("23711112222", false, "首位为2的号码应验证失败")]
        [InlineData("33711112222", false, "首位为3的号码应验证失败")]
        [InlineData("43711112222", false, "首位为4的号码应验证失败")]
        [InlineData("53711112222", false, "首位为5的号码应验证失败")]
        [InlineData("03711112222", false, "首位为0的号码应验证失败")]
        [InlineData("12345678901", false, "首位为1但第二位为2的号码应验证失败")]
        // 包含非数字字符
        [InlineData("1371111222a", false, "包含字母 a 应验证失败")]
        [InlineData("137 1111 2222", false, "包含空格应验证失败")]
        [InlineData("137-1111-2222", false, "包含连字符应验证失败")]
        [InlineData("137.1111.2222", false, "包含点号应验证失败")]
        [InlineData("137_1111_2222", false, "包含下划线应验证失败")]
        [InlineData("137#1111#2222", false, "包含井号应验证失败")]
        [InlineData("137@1111@2222", false, "包含@符号应验证失败")]
        [InlineData("1371111o222", false, "包含字母 o 代替0应验证失败")]
        // 其他常见非法格式
        [InlineData("01012345678", false, "固定电话（带区号）应验证失败")]
        [InlineData("4008123123", false, "400电话应验证失败")]
        [InlineData("8008123123", false, "800电话应验证失败")]
        [InlineData("+8613711112222", false, "含国际区号 +86 应验证失败")]
        [InlineData("8613711112222", false, "含86前缀应验证失败")]
        [InlineData("１３７１１１１２２２２", false, "全角数字应验证失败")]
        [InlineData("1371111222\n", false, "末尾换行符应验证失败")]
        [InlineData("\t13711112222", false, "前导制表符应验证失败")]
        public void IsValidPhoneNumber_InvalidCases_ReturnsFalse(string input, bool expectedResult, string errorMessage)
        {
            var result = ValidatorUtil.IsPhoneNumber(input);
            Assert.True(result == expectedResult, errorMessage);
        }

        /// <summary>
        /// 性能压力测试
        /// </summary>
        [Fact]
        public void IsValidPhoneNumber_Performance_LargeVolume()
        {
            const int expectedMillisecond = 1 * 1000;
            const int count = 100_0000;
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < count; i++)
            {
                ValidatorUtil.IsPhoneNumber("13785268526");
            }

            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds < expectedMillisecond, $"验证 {count} 个字符串耗时 {stopwatch.ElapsedMilliseconds}ms，超出预期的 {expectedMillisecond}ms");
        }
    }
}
