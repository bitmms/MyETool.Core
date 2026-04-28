using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllCharsIgnoreCaseTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllCharsIgnoreCase(null, 'a', 'b'));
        }

        [Theory]
        [InlineData("abcdefAA", "cdef", 'a', 'b')]
        [InlineData("abcdefAA", "bcdef", 'a')]
        [InlineData("abcdefAA", "df", 'a', 'b', 'c', 'e')]
        public void Replace_NormalCases(string sourceString, string expected, params char[] c)
        {
            Assert.Equal(expected, StrUtil.RemoveAllCharsIgnoreCase(sourceString, c));
        }
    }
}
