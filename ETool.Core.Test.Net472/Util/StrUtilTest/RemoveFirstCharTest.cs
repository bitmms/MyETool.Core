using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveFirstCharTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveFirstChar(null, ' ', true));
        }

        [Theory]
        [InlineData("abcdef", 'a', false, "bcdef")]
        [InlineData("abcdef", 'A', false, "abcdef")]
        [InlineData("abcdef", 'A', true, "bcdef")]
        public void Replace_NormalCases(string sourceString, char c, bool ignoreCase, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveFirstChar(sourceString, c, ignoreCase));
        }
    }
}
