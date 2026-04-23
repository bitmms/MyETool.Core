using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class ReplaceRangeWithCharTests
    {
        #region ✅ 正常情况

        [Theory]
        [InlineData("abcdef", 2, 3, '*', "ab***f")]
        [InlineData("abcdef", 0, 2, '#', "##cdef")]
        [InlineData("abcdef", 4, 2, 'X', "abcdXX")]
        public void Replace_NormalCases(
            string input,
            int start,
            int count,
            char replaceChar,
            string expected)
        {
            var result = StrUtil.ReplaceRangeWithChar(input, start, count, replaceChar);
            Assert.Equal(expected, result);
        }

        #endregion

        #region ✅ 超出范围自动截断

        [Fact]
        public void Replace_CountExceedsLength_ShouldTrim()
        {
            var result = StrUtil.ReplaceRangeWithChar("abcdef", 4, 10, '*');
            Assert.Equal("abcd**", result);
        }

        #endregion

        #region ✅ count 为 0

        [Fact]
        public void Replace_CountZero_ShouldReturnOriginal()
        {
            var input = "abcdef";
            var result = StrUtil.ReplaceRangeWithChar(input, 2, 0, '*');
            Assert.Equal(input, result);
        }

        #endregion

        #region ✅ startIndex == Length

        [Fact]
        public void Replace_StartIndexAtEnd_ShouldReturnOriginal()
        {
            var input = "abcdef";
            var result = StrUtil.ReplaceRangeWithChar(input, input.Length, 3, '*');
            Assert.Equal(input, result);
        }

        #endregion

        #region ✅ 空字符串

        [Fact]
        public void Replace_EmptyString()
        {
            var result = StrUtil.ReplaceRangeWithChar("", 0, 5, '*');
            Assert.Equal("", result);
        }

        #endregion

        #region ✅ 单字符字符串

        [Fact]
        public void Replace_SingleCharacter()
        {
            var result = StrUtil.ReplaceRangeWithChar("a", 0, 1, '*');
            Assert.Equal("*", result);
        }

        #endregion

        #region ✅ 异常测试

        [Fact]
        public void Replace_NullSource_ShouldThrow()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
                StrUtil.ReplaceRangeWithChar(null, 0, 1, '*'));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100)]
        public void Replace_InvalidStartIndex_ShouldThrow(int start)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
                StrUtil.ReplaceRangeWithChar("abc", start, 1, '*'));
        }

        [Fact]
        public void Replace_NegativeCount_ShouldThrow()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
                StrUtil.ReplaceRangeWithChar("abc", 0, -1, '*'));
        }

        #endregion
    }
}
