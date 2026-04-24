using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RemoveAllNewLinesTest
    {
        [Theory]
        [InlineData("abc\nDD\rdfd", "abcDDdfd")]
        public void Test(string s, string expected)
        {
            Assert.Equal(expected, StrUtil.RemoveAllNewLines(s));
        }
    }
}
