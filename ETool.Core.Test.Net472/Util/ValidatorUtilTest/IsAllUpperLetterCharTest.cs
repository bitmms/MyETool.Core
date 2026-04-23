using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsAllUpperLetterCharTest
    {
        [Fact]
        public void IsAllUpperLetterChar_全大写_返回True()
        {
            string s = "HELLO";
            Assert.True(ValidatorUtil.IsAllUpperLetterChar(s));
        }

        [Fact]
        public void IsAllUpperLetterChar_包含小写_返回False()
        {
            string s = "HELLo";
            Assert.False(ValidatorUtil.IsAllUpperLetterChar(s));
        }
    }
}
