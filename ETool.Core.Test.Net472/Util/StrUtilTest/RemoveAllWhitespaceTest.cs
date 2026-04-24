using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllWhitespaceTest
    {
        [Fact]
        public void ExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllWhitespace(null));
        }

        [Theory]
        [InlineData("abcD  Ddfd", "abcDDdfd")]
        public void Test(string s, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveAllWhitespace(s));
        }
    }
}
