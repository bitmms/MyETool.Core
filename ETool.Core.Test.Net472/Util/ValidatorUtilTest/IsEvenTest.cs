using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsEvenTest
    {
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(-1, false)]
        [InlineData(-2, true)]
        [InlineData(-10, true)]
        public void Test(int number, bool expected)
        {
            Assert.Equal(expected, ValidatorUtil.IsEven(number));
        }
    }
}
