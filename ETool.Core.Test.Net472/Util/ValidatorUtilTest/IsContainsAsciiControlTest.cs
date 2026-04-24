using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsContainsAsciiControlTest
    {
        [Theory(DisplayName = "包含 ASCII 中的控制字符")]
        [InlineData("ab\tcDdfd")]
        [InlineData("ab\ncDdfd")]
        [InlineData("ab\rcDdfd")]
        public void Test_Return_True(string input)
        {
            Assert.True(ValidatorUtil.IsContainsAsciiControl(input));
        }

        [Theory(DisplayName = "不包含 ASCII 中的控制字符")]
        [InlineData("你好")]
        [InlineData("ab")]
        [InlineData("abcDdfd")]
        [InlineData("123+-/-*-$%^**(%&%&~~!#$66467")]
        [InlineData("123+-/-*-$%          ^**(%&%&~~!#$66467")]
        public void Test_Return_False(string input)
        {
            Assert.False(ValidatorUtil.IsContainsAsciiControl(input));
        }
    }
}
