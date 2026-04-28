using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class ReplaceStringTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.ReplaceString(null, " ", "", true));
            Assert.Throws<ArgumentNullException>(() => StrUtil.ReplaceString("", " ", null, true));
            Assert.Throws<ArgumentNullException>(() => StrUtil.ReplaceString("", null, "", true));
        }

        [Theory]
        [InlineData("abcdefAA", "a", "*", false, "*bcdefAA")]
        [InlineData("abcdefAA", "a", "*", true, "*bcdef**")]
        public void Replace_NormalCases(string s, string sourceString, string targetString, bool ignoreCase, string expected)
        {
            Assert.Equal(expected, StrUtil.ReplaceString(s, sourceString, targetString, ignoreCase));
        }
    }
}
