using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsContainsStringTest
    {
        [Theory(DisplayName = "包含指定子串")]
        [InlineData("abcDDdfd", "abcd", true)]
        public void Test_Return_True(string sourceString, string targetString, bool ignoreCase)
        {
            Assert.True(ValidatorUtil.IsContainsString(sourceString, targetString, ignoreCase));
        }

        [Theory(DisplayName = "不包含指定子串")]
        [InlineData("abcDDdfd", "abcd", false)]
        public void Test_Return_False(string sourceString, string targetString, bool ignoreCase)
        {
            Assert.False(ValidatorUtil.IsContainsString(sourceString, targetString, ignoreCase));
        }
    }
}
