using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveSuffixTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveSuffix(null, "", true));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveSuffix(null, "", false));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveSuffix("aaa", null, false));
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveSuffix("vdfsds", null, false));
        }

        [Theory]
        [InlineData("abcdef", "def", false, "abc")]
        [InlineData("abcdef", "", false, "abcdef")]
        [InlineData("abcdef", "F", false, "abcdef")]
        [InlineData("abcdef", "F", true, "abcde")]
        public void Replace_NormalCases(string sourceString, string prefix, bool ignoreCase, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveSuffix(sourceString, prefix, ignoreCase));
        }
    }
}
