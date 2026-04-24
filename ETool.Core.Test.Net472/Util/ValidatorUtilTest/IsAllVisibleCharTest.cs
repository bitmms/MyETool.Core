using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsAllVisibleCharTest
    {
        [Theory(DisplayName = "仅包含可见的可打印字符")]
        [InlineData("123+-/-*-$%^**(%&%&~~!#$66467")]
        [InlineData("123+-/-*-$%          ^**(%&%&~~!#$66467")]
        public void Test_Return_True(string input)
        {
            Assert.True(ValidatorUtil.IsAllAsciiPrintable(input));
        }

        [Theory(DisplayName = "包含其他字符")]
        [InlineData("你好")]
        [InlineData("a\tb")]
        [InlineData("abc\nDdfd")]
        public void Test_Return_False(string input)
        {
            Assert.False(ValidatorUtil.IsAllAsciiPrintable(input));
        }
    }
}
