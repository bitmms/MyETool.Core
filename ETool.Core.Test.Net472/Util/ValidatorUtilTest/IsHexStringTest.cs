using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsHexStringTest
    {
        [Theory]
        [InlineData("e3e3e3")]
        [InlineData("aabcd1516546")]
        public void Return_True(string s)
        {
            Assert.True(ValidatorUtil.IsHexString(s));
        }

        [Theory]
        [InlineData("abc1264w3")]
        [InlineData("abc1264 3")]
        public void Return_False(string s)
        {
            Assert.False(ValidatorUtil.IsHexString(s));
        }
    }
}
