using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsContainsLetterTest
    {
        [Theory(DisplayName = "包含英文字符")]
        [InlineData("abcDDdfd")]
        [InlineData("abcDdfd")]
        public void Test_Return_True(string input)
        {
            Assert.True(ValidatorUtil.IsContainsLetter(input));
        }

        [Theory(DisplayName = "不包含英文字符")]
        [InlineData("123")]
        public void Test_Return_False(string input)
        {
            Assert.False(ValidatorUtil.IsContainsLetter(input));
        }
    }
}
