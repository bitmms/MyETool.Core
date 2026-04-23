using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsPerfectSquareTest
    {
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(4, true)]
        [InlineData(-1, false)]
        [InlineData(-2, false)]
        [InlineData(-10, false)]
        [InlineData(1000000, true)]
        public void Test(int number, bool expected)
        {
            Assert.Equal(expected, ValidatorUtil.IsPerfectSquare(number));
        }
    }
}
