using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllCharTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllChar(null, ' ', true));
        }

        [Theory]
        [InlineData("abcdefAA", 'a', false, "bcdefAA")]
        [InlineData("abcdefAA", 'A', false, "abcdef")]
        [InlineData("abcdefAA", 'A', true, "bcdef")]
        public void Replace_NormalCases(string sourceString, char c, bool ignoreCase, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveAllChar(sourceString, c, ignoreCase));
        }
    }
}
