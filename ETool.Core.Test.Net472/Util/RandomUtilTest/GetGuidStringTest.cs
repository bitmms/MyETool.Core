using System;
using System.Text.RegularExpressions;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetGuidStringTest
    {
        [Theory]
        [InlineData("d")]
        [InlineData("D")]
        public void GetGuidString_DFormat_ReturnsLowerCaseWithHyphens(string format)
        {
            var result = RandomUtil.GetGuidString(format);
            Assert.Equal(36, result.Length); // 8-4-4-4-12 = 36字符
            Assert.Contains("-", result);
            Assert.True(Regex.IsMatch(result, @"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$"), $"格式 {format} 应该返回小写带连字符的GUID，实际: {result}");
        }

        [Theory]
        [InlineData("dd")]
        [InlineData("DD")]
        public void GetGuidString_DDFormat_ReturnsUpperCaseWithHyphens(string format)
        {
            var result = RandomUtil.GetGuidString(format);
            Assert.Equal(36, result.Length);
            Assert.Contains("-", result);
            Assert.True(Regex.IsMatch(result, @"^[0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12}$"), $"格式 {format} 应该返回大写带连字符的GUID，实际: {result}");
        }

        [Theory]
        [InlineData("n")]
        [InlineData("N")]
        public void GetGuidString_NFormat_ReturnsLowerCaseNoHyphens(string format)
        {
            var result = RandomUtil.GetGuidString(format);
            Assert.Equal(32, result.Length);
            Assert.DoesNotContain("-", result);
            Assert.True(Regex.IsMatch(result, @"^[0-9a-f]{32}$"), $"格式 {format} 应该返回小写无连字符的GUID，实际: {result}");
        }

        [Theory]
        [InlineData("nn")]
        [InlineData("NN")]
        public void GetGuidString_NNFormat_ReturnsUpperCaseNoHyphens(string format)
        {
            var result = RandomUtil.GetGuidString(format);
            Assert.Equal(32, result.Length);
            Assert.DoesNotContain("-", result);
            Assert.True(Regex.IsMatch(result, @"^[0-9A-F]{32}$"), $"格式 {format} 应该返回大写无连字符的GUID，实际: {result}");
        }

        [Fact]
        public void GetGuidString_NoParameters_ReturnsDDFormat()
        {
            var result = RandomUtil.GetGuidString();
            Assert.Equal(36, result.Length);
            Assert.True(Regex.IsMatch(result, @"^[0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12}$"), "默认参数应该返回大写带连字符格式(DD)");
        }

        [Fact]
        public void GetGuidString_NullFormat_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => RandomUtil.GetGuidString(null));
            Assert.Equal("format", ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("X")]
        [InlineData("B")]
        [InlineData("P")]
        [InlineData("invalid")]
        [InlineData("dn")]
        public void GetGuidString_InvalidFormat_ThrowsArgumentException(string format)
        {
            var ex = Assert.Throws<ArgumentException>(() => RandomUtil.GetGuidString(format));
            Assert.Equal("format", ex.ParamName);
            Assert.Contains("仅支持 D、N、DD、NN", ex.Message);
        }

        [Fact]
        public void GetGuidString_MultipleCalls_ReturnsUniqueValues()
        {
            var guid1 = RandomUtil.GetGuidString("N");
            var guid2 = RandomUtil.GetGuidString("N");
            Assert.NotEqual(guid1, guid2);
        }
    }
}
