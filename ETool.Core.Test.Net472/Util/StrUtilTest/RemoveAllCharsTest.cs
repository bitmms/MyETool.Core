using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllCharsTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllChars(null, 'a', 'b'));
        }

        [Theory]
        [InlineData("abcdefAA", "cdefAA", 'a', 'b')]
        [InlineData("abcdefAA", "bcdefAA", 'a')]
        [InlineData("abcdefAA", "dfAA", 'a', 'b', 'c', 'e')]
        public void Replace_NormalCases(string sourceString, string expected, params char[] c)
        {
            Assert.Equal(expected, StrUtil.RemoveAllChars(sourceString, c));
        }
    }
}
