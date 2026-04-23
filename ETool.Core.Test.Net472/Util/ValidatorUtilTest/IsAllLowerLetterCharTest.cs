using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsAllLowerLetterCharTest
    {
        [Fact]
        public void IsAllLowerLetterChar_全小写_返回True()
        {
            string s = "hello";
            Assert.True(ValidatorUtil.IsAllLowerLetterChar(s));
        }

        [Fact]
        public void IsAllLowerLetterChar_包含大写_返回False()
        {
            string s = "Hello";
            Assert.False(ValidatorUtil.IsAllLowerLetterChar(s));
        }
    }
}
