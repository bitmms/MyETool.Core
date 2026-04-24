using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllWhitespaceTest
    {
        [Theory]
        [InlineData("abcD  Ddfd", "abcDDdfd")]
        public void Test(string s, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveAllWhitespace(s));
        }
    }
}
