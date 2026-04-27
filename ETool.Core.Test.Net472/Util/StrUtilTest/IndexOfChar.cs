using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class IndexOfChar
    {
        [Theory]
        [InlineData("Hello", 'e', 0, 5, true, 1)]
        [InlineData("Hello", 'E', 0, 5, true, 1)]
        [InlineData("Hello", 'E', 0, 5, false, -1)]
        [InlineData("Hello", 'l', 0, 5, false, 2)]
        [InlineData("a", 'a', 0, 1, false, 0)]
        [InlineData("a", 'A', 0, 1, true, 0)]
        [InlineData("abcde", 'e', 0, 5, false, 4)]
        public void IndexOfChar_Char_Found(string s, char c, int start, int count, bool ignoreCase, int expected)
        {
            Assert.Equal(expected, StrUtil.IndexOfChar(s, c, start, count, ignoreCase));
        }

        [Theory]
        [InlineData("Hello", 'x', 0, 5, false, -1)]
        [InlineData("Hello", 'L', 2, 2, false, -1)]
        [InlineData("abc", 'd', 0, 3, true, -1)]
        public void IndexOfChar_Char_NotFound(string s, char c, int start, int count, bool ignoreCase, int expected)
        {
            Assert.Equal(expected, StrUtil.IndexOfChar(s, c, start, count, ignoreCase));
        }

        [Theory]
        [InlineData("abcdef", 'c', 1, 3, false, 2)]
        [InlineData("abcdef", 'd', 1, 3, false, 3)]
        [InlineData("AAAAA", 'a', 2, 2, true, 2)]
        public void IndexOfChar_Char_Range(string s, char c, int start, int count, bool ignoreCase, int expected)
        {
            Assert.Equal(expected, StrUtil.IndexOfChar(s, c, start, count, ignoreCase));
        }

        [Fact]
        public void IndexOfChar_CountZero_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOfChar("test", 't', 0, 0, false));
            Assert.Equal(-1, StrUtil.IndexOfChar("a", 'a', 0, 0, true));
        }

        [Fact]
        public void IndexOfChar_SourceNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.IndexOfChar(null, 'a', 0, 1, false));
        }

        [Fact]
        public void IndexOfChar_StartNegative_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StrUtil.IndexOfChar("abc", 'a', -1, 1, false));
        }

        [Fact]
        public void IndexOfChar_StartOutOfRange_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StrUtil.IndexOfChar("abc", 'a', 3, 1, false));
            Assert.Throws<ArgumentOutOfRangeException>(() => StrUtil.IndexOfChar("abc", 'a', 10, 1, false));
        }

        [Fact]
        public void IndexOfChar_CountNegative_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StrUtil.IndexOfChar("abc", 'a', 0, -1, false));
        }

        [Fact]
        public void IndexOfChar_RangeOutOfBounds_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StrUtil.IndexOfChar("abc", 'a', 1, 3, false));
            Assert.Throws<ArgumentOutOfRangeException>(() => StrUtil.IndexOfChar("abc", 'a', 2, 4, false));
        }
    }
}
