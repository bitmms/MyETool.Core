using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.NumberUtilTest
{
    public class AddTest
    {
        [Fact]
        public void Add_ShouldThrow_WhenNull()
        {
            Assert.Throws<ArgumentNullException>(() => NumberUtil.Add(null, "1"));
            Assert.Throws<ArgumentNullException>(() => NumberUtil.Add("1", null));
        }

        [Fact]
        public void Add_ShouldThrow_WhenInvalidFormat()
        {
            Assert.Throws<ArgumentException>(() => NumberUtil.Add("abc", "1"));
            Assert.Throws<ArgumentException>(() => NumberUtil.Add("1", "1.23"));
            Assert.Throws<ArgumentException>(() => NumberUtil.Add("+1", "1"));
            Assert.Throws<ArgumentException>(() => NumberUtil.Add("--1", "1"));
        }

        [Fact]
        public void Add_ShouldThrow_WhenLengthExceeded()
        {
            var tooLong = new string('1', 100001);

            Assert.Throws<ArgumentOutOfRangeException>(() => NumberUtil.Add(tooLong, "1"));
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberUtil.Add("1", tooLong));
        }

        [Theory]
        [InlineData("1", "1", "2")]
        [InlineData("9", "1", "10")]
        [InlineData("123", "456", "579")]
        [InlineData("999", "1", "1000")]
        [InlineData("123456789", "987654321", "1111111110")]
        public void Add_Positive_Positive(string n1, string n2, string expected)
        {
            var result = NumberUtil.Add(n1, n2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("5", "-3", "2")]
        [InlineData("3", "-5", "-2")]
        [InlineData("1000", "-1", "999")]
        [InlineData("1000", "-999", "1")]
        [InlineData("999", "-999", "0")]
        public void Add_Positive_Negative(string n1, string n2, string expected)
        {
            var result = NumberUtil.Add(n1, n2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("-5", "3", "-2")]
        [InlineData("-3", "5", "2")]
        [InlineData("-1000", "1", "-999")]
        [InlineData("-1000", "999", "-1")]
        [InlineData("-999", "999", "0")]
        public void Add_Negative_Positive(string n1, string n2, string expected)
        {
            var result = NumberUtil.Add(n1, n2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("-1", "-1", "-2")]
        [InlineData("-123", "-456", "-579")]
        [InlineData("-999", "-1", "-1000")]
        public void Add_Negative_Negative(string n1, string n2, string expected)
        {
            var result = NumberUtil.Add(n1, n2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("0", "123", "123")]
        [InlineData("123", "0", "123")]
        [InlineData("0", "-123", "-123")]
        [InlineData("-123", "0", "-123")]
        public void Add_WithZero(string n1, string n2, string expected)
        {
            var result = NumberUtil.Add(n1, n2);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Add_LargeNumbers_ShouldWork()
        {
            var n1 = new string('9', 10000);
            var n2 = "1";

            var result = NumberUtil.Add(n1, n2);

            Assert.Equal("1" + new string('0', 10000), result);
        }

        [Fact]
        public void Add_ContinuousBorrow()
        {
            var result = NumberUtil.Add("1000000", "-1");
            Assert.Equal("999999", result);
        }

        // 符号极端交叉
        [Theory]
        [InlineData("1", "-1000000000000", "-999999999999")]
        [InlineData("-1", "1000000000000", "999999999999")]
        public void Add_ExtremeSignCross(string n1, string n2, string expected)
        {
            var result = NumberUtil.Add(n1, n2);
            Assert.Equal(expected, result);
        }

        // 连续借位跨越全部位数
        [Fact]
        public void Add_FullBorrowAcrossAllDigits()
        {
            var result = NumberUtil.Add("1000000000000000", "-999999999999999");
            Assert.Equal("1", result);
        }

        // 等值绝对值场景
        [Theory]
        [InlineData("10", "-10")]
        [InlineData("123456", "-123456")]
        [InlineData("-98765", "98765")]
        public void Add_AbsoluteEqual_ShouldBeZero(string n1, string n2)
        {
            var result = NumberUtil.Add(n1, n2);
            Assert.Equal("0", result);
        }

        // 极端边界长度测试（最大长度）
        [Fact]
        public void Add_MaxLengthBoundary()
        {
            var n1 = new string('9', 100000);
            var n2 = "1";
            var result = NumberUtil.Add(n1, n2);
            Assert.Equal("1" + new string('0', 100000), result);
        }
    }
}
