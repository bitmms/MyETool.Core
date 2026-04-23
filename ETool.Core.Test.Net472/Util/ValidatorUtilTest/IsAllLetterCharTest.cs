using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsAllLetterCharTest
    {
        [Fact]
        public void IsAllLetterChar_全大小写字母_返回True()
        {
            string s = "AbcDef";
            Assert.True(ValidatorUtil.IsAllLetterChar(s));
        }

        [Fact]
        public void IsAllLetterChar_包含数字_返回False()
        {
            string s = "abc123";
            Assert.False(ValidatorUtil.IsAllLetterChar(s));
        }

        [Fact]
        public void IsAllLetterChar_空字符串_返回False()
        {
            string s = "";
            Assert.False(ValidatorUtil.IsAllLetterChar(s));
        }
    }
}
