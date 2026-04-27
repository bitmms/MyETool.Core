using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.MaskUtilTest
{
    public class MaskPasswordTest
    {
        [Theory(DisplayName = "1证脱敏成功")]
        [InlineData("11010519491231002X", "11**************2X")]
        [InlineData("110", "****")]
        [InlineData("11", "****")]
        [InlineData("1", "****")]
        [InlineData("", "****")]
        public void Valid_Test1(string password, string expected)
        {
            var result = MaskUtil.MaskPassword(password);
            Assert.Equal(expected, result);
        }
    }
}
