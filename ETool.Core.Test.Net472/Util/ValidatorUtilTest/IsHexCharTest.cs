using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsHexCharTest
    {
        [Theory]
        [InlineData('a')]
        [InlineData('0')]
        public void Return_True(char c)
        {
            Assert.True(ValidatorUtil.IsHexChar(c));
        }

        [Theory]
        [InlineData('w')]
        [InlineData(' ')]
        public void Return_False(char c)
        {
            Assert.False(ValidatorUtil.IsHexChar(c));
        }
    }
}
