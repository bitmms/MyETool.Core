using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsStartsWithTest
    {
        [Theory(DisplayName = "以指定子串开头")]
        [InlineData("abcDDdfd", "abc", false)]
        [InlineData("abcDDdfd", "abcd", true)]
        public void Test_Return_True(string s, string prefix, bool ignoreCase = false)
        {
            Assert.True(ValidatorUtil.IsStartsWith(s, prefix, ignoreCase));
        }

        [Theory(DisplayName = "不以指定子串开头")]
        [InlineData("abcDDdfd", "abcd", false)]
        public void Test_Return_False(string s, string prefix, bool ignoreCase = false)
        {
            Assert.False(ValidatorUtil.IsStartsWith(s, prefix, ignoreCase));
        }
    }
}
