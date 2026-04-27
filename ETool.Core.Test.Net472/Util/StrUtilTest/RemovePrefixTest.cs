using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemovePrefixTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemovePrefix(null, "", true));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemovePrefix(null, "", false));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemovePrefix("aaa", null, false));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemovePrefix("vdfsds", null, false));
        }

        [Theory]
        [InlineData("abcdef", "abc", false, "def")]
        [InlineData("abcdef", "", false, "abcdef")]
        [InlineData("abcdef", "A", false, "abcdef")]
        [InlineData("abcdef", "A", true, "bcdef")]
        public void Replace_NormalCases(string sourceString, string prefix, bool ignoreCase, string expected)
        {
            Assert.Equal(expected, StrUtil.RemovePrefix(sourceString, prefix, ignoreCase));
        }
    }
}
