using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class ReplaceCharTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.ReplaceChar(null, 'a', ' ', true));
        }

        [Theory]
        [InlineData("abcdefAA", 'a', '*', false, "*bcdefAA")]
        [InlineData("abcdefAA", 'a', '*', true, "*bcdef**")]
        public void Replace_NormalCases(string s, char sourceChar, char targetChar, bool ignoreCase, string expected)
        {
            Assert.Equal(expected, StrUtil.ReplaceChar(s, sourceChar, targetChar, ignoreCase));
        }
    }
}
