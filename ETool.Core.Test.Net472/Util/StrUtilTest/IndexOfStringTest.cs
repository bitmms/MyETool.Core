using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class IndexOfStringTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
        }

        [Theory]
        [InlineData("abcdef", "abc", 0, 6, false, 0)]
        [InlineData("abcdef", "abC", 0, 6, false, -1)]
        [InlineData("abcdef", "abC", 0, 6, true, 0)]
        public void Replace_NormalCases(string sourceString, string prefix, int start, int count, bool ignoreCase, int expected)
        {
            Assert.Equal(expected, StrUtil.IndexOfString(sourceString, prefix, start, count, ignoreCase));
        }
    }
}
