using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllStringsTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllStrings(null, ""));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllStrings("aaaa", null));
        }

        [Theory]
        [InlineData("AAabcdefAAAA", "AAdefA", "AAA", "abc")]
        [InlineData("AAabcdefAAAA", "def", "AA", "abc")]
        public void Replace_NormalCases(string sourceString, string expected, params string[] targetSubstring)
        {
            Assert.Equal(expected, StrUtil.RemoveAllStrings(sourceString, targetSubstring));
        }
    }
}
