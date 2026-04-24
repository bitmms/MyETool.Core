using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsContainsCharTest
    {
        [Theory(DisplayName = "包含指定字符")]
        [InlineData("abcDDdfd", 'a', false)]
        [InlineData("abcDdfd", 'F', true)]
        [InlineData("abcDdfd", 'A', true)]
        public void Test_Return_True(string input, char c, bool ignoreCase)
        {
            Assert.True(ValidatorUtil.IsContainsChar(input, c, ignoreCase));
        }

        [Theory(DisplayName = "不包含指定字符")]
        [InlineData("abcDDdfd", 'A', false)]
        [InlineData("abcDdFd", 'f', false)]
        [InlineData("abcDdfd", '+', true)]
        public void Test_Return_False(string input, char c, bool ignoreCase)
        {
            Assert.False(ValidatorUtil.IsContainsChar(input, c, ignoreCase));
        }
    }
}
