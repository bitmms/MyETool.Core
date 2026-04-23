using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsPrimeTest
    {
        [Theory]
        [InlineData(-10, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, false)]
        public void Int_InvalidInput_ReturnsFalse(int number, bool expected)
        {
            Assert.Equal(expected, ValidatorUtil.IsPrime(number));
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(5, true)]
        [InlineData(7, true)]
        [InlineData(11, true)]
        [InlineData(13, true)]
        [InlineData(17, true)]
        [InlineData(19, true)]
        [InlineData(23, true)]
        [InlineData(29, true)]
        [InlineData(31, true)]
        public void Int_SmallPrimes_ReturnsTrue(int number, bool expected)
        {
            Assert.Equal(expected, ValidatorUtil.IsPrime(number));
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(6, false)]
        [InlineData(8, false)]
        [InlineData(9, false)]
        [InlineData(10, false)]
        [InlineData(14, false)]
        [InlineData(15, false)]
        [InlineData(21, false)]
        [InlineData(25, false)]
        [InlineData(27, false)]
        [InlineData(33, false)]
        [InlineData(35, false)]
        public void Int_SmallComposites_ReturnsFalse(int number, bool expected)
        {
            Assert.Equal(expected, ValidatorUtil.IsPrime(number));
        }

        [Theory]
        [InlineData(101, true)]
        [InlineData(103, true)]
        [InlineData(107, true)]
        [InlineData(109, true)]
        [InlineData(113, true)]
        [InlineData(121, false)] // 11^2
        [InlineData(143, false)] // 11*13
        [InlineData(169, false)] // 13^2
        [InlineData(187, false)] // 11*17
        [InlineData(221, false)] // 13*17
        [InlineData(223, true)]
        public void Int_MidRange_PrimesAndComposites(int number, bool expected)
        {
            Assert.Equal(expected, ValidatorUtil.IsPrime(number));
        }

        [Theory]
        [InlineData(999983, true)]
        [InlineData(1000003, true)]
        [InlineData(1000000007, true)]
        [InlineData(2147483647, true)] // int.MaxValue == 2147483647
        public void Int_MaxSmallPrime_ReturnsTrue(int number, bool expected)
        {
            Assert.Equal(expected, ValidatorUtil.IsPrime(number));
        }

        [Fact]
        public void Int_LargeComposite_ReturnsFalse()
        {
            // 2147483647 不是，选一个 int 范围内的大合数
            Assert.False(ValidatorUtil.IsPrime(2147483646));
            Assert.False(ValidatorUtil.IsPrime(1000000005));
        }
    }
}
