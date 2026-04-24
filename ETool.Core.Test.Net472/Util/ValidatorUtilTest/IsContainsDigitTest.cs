using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsContainsDigitTest
    {
        [Theory(DisplayName = "包含数字字符")]
        [InlineData("123")]
        public void Test_Return_True(string input)
        {
            Assert.True(ValidatorUtil.IsContainsDigit(input));
        }

        [Theory(DisplayName = "不包含数字字符")]
        [InlineData("abcDDdfd")]
        [InlineData("abcDdfd")]
        public void Test_Return_False(string input)
        {
            Assert.False(ValidatorUtil.IsContainsDigit(input));
        }
    }
}
