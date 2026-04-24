using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllNewLinesTest
    {
        [Fact]
        public void ExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.RemoveAllNewLine(null));
        }

        [Theory]
        [InlineData("abc\nDD\rdfd", "abcDDdfd")]
        public void Test(string s, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveAllNewLine(s));
        }
    }
}
