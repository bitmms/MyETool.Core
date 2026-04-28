using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveFirstStringTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveFirstString(null, "", true));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveFirstString("aaaa", null, true));
        }

        [Theory]
        [InlineData("abcdefAA", "aaa", false, "abcdefAA")]
        [InlineData("abcdefAA", "", false, "abcdefAA")]
        [InlineData("abcdefAA", "aa", false, "abcdefAA")]
        [InlineData("abcdefAA", "aa", true, "abcdef")]
        [InlineData("", "aa", true, "")]
        [InlineData("XXXX", "", true, "XXXX")]
        public void Replace_NormalCases(string sourceString, string targetSubstring, bool ignoreCase, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveFirstString(sourceString, targetSubstring, ignoreCase));
        }
    }
}
