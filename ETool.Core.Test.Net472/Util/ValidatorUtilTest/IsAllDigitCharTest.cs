using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsAllDigitCharTest
    {
        [Fact]
        public void IsAllDigitChar_全数字字符串_返回True()
        {
            string s = "123456";
            Assert.True(ValidatorUtil.IsAllDigitChar(s));
        }

        [Fact]
        public void IsAllDigitChar_包含字母_返回False()
        {
            string s = "123a56";
            Assert.False(ValidatorUtil.IsAllDigitChar(s));
        }

        [Fact]
        public void IsAllDigitChar_空字符串_返回False()
        {
            string s = "";
            Assert.False(ValidatorUtil.IsAllDigitChar(s));
        }

        [Fact]
        public void IsAllDigitChar_空格字符串_返回False()
        {
            string s = "   ";
            Assert.False(ValidatorUtil.IsAllDigitChar(s));
        }

        [Fact]
        public void IsAllDigitChar_Null_返回False()
        {
            string s = null;
            Assert.False(ValidatorUtil.IsAllDigitChar(s));
        }

        [Fact]
        public void IsAllDigitChar_包含符号_返回False()
        {
            string s = "123-45";
            Assert.False(ValidatorUtil.IsAllDigitChar(s));
        }
    }
}
