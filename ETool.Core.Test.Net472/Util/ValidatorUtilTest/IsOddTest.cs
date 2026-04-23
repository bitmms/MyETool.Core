using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsOddTest
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(-1, true)]
        [InlineData(-2, false)]
        [InlineData(-10, false)]
        public void Test(int number, bool expected)
        {
            Assert.Equal(expected, ValidatorUtil.IsOdd(number));
        }
    }
}
