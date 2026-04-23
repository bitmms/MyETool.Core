using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.NumberUtilTest
{
    public class MulTest
    {
        [Theory]
        [InlineData("0", "1", "0")]
        [InlineData("0", "0", "0")]
        [InlineData("0", "123", "0")]
        [InlineData("123", "0", "0")]
        [InlineData("1", "999", "999")]
        [InlineData("999", "1", "999")]
        [InlineData("2", "3", "6")]
        [InlineData("9", "9", "81")]
        [InlineData("12", "34", "408")]
        [InlineData("123", "456", "56088")]
        [InlineData("999", "999", "998001")]
        [InlineData("1000", "1000", "1000000")]
        [InlineData("123456789", "987654321", "121932631112635269")]
        public void Mul_ShouldReturnExpectedResult_ForPositiveNumbers(string n1, string n2, string expected)
        {
            var actual = NumberUtil.Mul(n1, n2);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("-2", "3", "-6")]
        [InlineData("2", "-3", "-6")]
        [InlineData("-2", "-3", "6")]
        [InlineData("-123", "456", "-56088")]
        [InlineData("123", "-456", "-56088")]
        [InlineData("-123", "-456", "56088")]
        [InlineData("-999", "999", "-998001")]
        [InlineData("-999", "-999", "998001")]
        public void Mul_ShouldHandleSignCorrectly(string n1, string n2, string expected)
        {
            var actual = NumberUtil.Mul(n1, n2);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Mul_N1IsNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => NumberUtil.Mul(null, "123"));
        }

        [Fact]
        public void Mul_N2IsNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => NumberUtil.Mul("123", null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        [InlineData("--1")]
        [InlineData("1.2")]
        [InlineData("+123")] // 如果你的 ValidatorUtil 允许 +123，这条就删掉
        public void Mul_InvalidN1_ShouldThrow(string n1)
        {
            Assert.Throws<ArgumentException>(() => NumberUtil.Mul(n1, "123"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        [InlineData("--1")]
        [InlineData("1.2")]
        [InlineData("+123")] // 如果你的 ValidatorUtil 允许 +123，这条就删掉
        public void Mul_InvalidN2_ShouldThrow(string n2)
        {
            Assert.Throws<ArgumentException>(() => NumberUtil.Mul("123", n2));
        }

        [Fact]
        public void Mul_N1TooLong_ShouldThrow()
        {
            var n1 = new string('9', 10_0001); // 假设 MaxLen = 10_0000
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberUtil.Mul(n1, "1"));
        }

        [Fact]
        public void Mul_N2TooLong_ShouldThrow()
        {
            var n2 = new string('9', 10_0001); // 假设 MaxLen = 10_0000
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberUtil.Mul("1", n2));
        }

        [Fact]
        public void Mul_MaxLenAndOne_ShouldWork()
        {
            var n1 = new string('9', 10000);
            var actual = NumberUtil.Mul(n1, "1");
            Assert.Equal(n1, actual);
        }

        [Fact]
        public void Mul_MaxLenSingleDigit_ShouldWork()
        {
            var n1 = new string('9', 10000);
            var actual = NumberUtil.Mul(n1, "9");
            Assert.StartsWith("8", actual);
            Assert.Equal(10001, actual.Length);
        }

        [Theory]
        [InlineData("-0", "123")]
        [InlineData("123", "-0")]
        [InlineData("-0", "-123")]
        [InlineData("-0", "-0")]
        public void Mul_ZeroResult_ShouldNeverReturnNegativeZero(string n1, string n2)
        {
            Assert.Throws<ArgumentException>(() => NumberUtil.Mul(n1, n2));
        }
    }
}
