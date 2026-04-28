using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllStringTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllString(null, "", true));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllString("aaaa", null, true));
        }

        [Theory]
        [InlineData("abcdefAAAA", "aaa", true, "abcdefA")]
        [InlineData("abcdefAAAA", "", false, "abcdefAAAA")]
        [InlineData("abcdefAAAA", "aa", false, "abcdefAAAA")]
        [InlineData("abcdefAAAA", "aa", true, "abcdef")]
        [InlineData("", "aa", true, "")]
        [InlineData("XXXX", "", true, "XXXX")]
        public void Replace_NormalCases(string sourceString, string targetSubstring, bool ignoreCase, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveAllString(sourceString, targetSubstring, ignoreCase));
        }
    }
}
