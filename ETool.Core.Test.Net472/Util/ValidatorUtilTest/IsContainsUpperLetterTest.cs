using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsContainsUpperLetterTest
    {
        [Theory(DisplayName = "包含大写英文字符")]
        [InlineData("abcDDdfd")]
        [InlineData("abcDdfd")]
        public void Test_Return_True(string input)
        {
            Assert.True(ValidatorUtil.IsContainsUpperLetter(input));
        }

        [Theory(DisplayName = "不包含大写英文字符")]
        [InlineData("123")]
        public void Test_Return_False(string input)
        {
            Assert.False(ValidatorUtil.IsContainsUpperLetter(input));
        }
    }
}
