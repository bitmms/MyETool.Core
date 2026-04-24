using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsEndsWithTest
    {
        [Theory(DisplayName = "以指定子串结束")]
        [InlineData("abcDDdfd", "Ddfd", false)]
        [InlineData("abcDDdfd", "ddfd", true)]
        public void Test_Return_True(string s, string suffix, bool ignoreCase = false)
        {
            Assert.True(ValidatorUtil.IsEndsWith(s, suffix, ignoreCase));
        }

        [Theory(DisplayName = "不以指定子串结束")]
        [InlineData("abcDDdfd", "ddfd", false)]
        public void Test_Return_False(string s, string suffix, bool ignoreCase = false)
        {
            Assert.False(ValidatorUtil.IsEndsWith(s, suffix, ignoreCase));
        }
    }
}
