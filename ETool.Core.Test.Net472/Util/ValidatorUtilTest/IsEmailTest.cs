using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsEmailTest
    {
        [Theory]
        [InlineData("abcDDdfd", false)]
        [InlineData("11111@qq.com.", false)]
        [InlineData("11111@", false)]
        [InlineData("11111@qq.com", true)]
        [InlineData("a@b.com", true)]
        [InlineData("zhang.san@gmail.com", true)]
        [InlineData("user+tag@outlook.com", true)]
        public void Test_Return_False(string s,  bool expectedResult = false)
        {
            Assert.Equal(expectedResult, ValidatorUtil.IsEmail(s));
        }
    }
}
