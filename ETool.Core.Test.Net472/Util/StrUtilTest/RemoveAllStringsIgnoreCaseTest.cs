using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllStringsIgnoreCaseTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllStringsIgnoreCase(null, ""));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllStringsIgnoreCase("aaaa", null));
        }

        [Theory]
        [InlineData("AAabcdefAAAA", "bcdefA", "AAA", "abc")]
        [InlineData("AAabcdefAAAA", "def", "AA", "abc")]
        public void Replace_NormalCases(string sourceString, string expected, params string[] targetSubstring)
        {
            Assert.Equal(expected, StrUtil.RemoveAllStringsIgnoreCase(sourceString, targetSubstring));
        }
    }
}
