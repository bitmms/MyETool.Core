using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.NumberUtilTest
{
    public class SubTest
    {
        [Theory]
        [InlineData("5", "3", "2")]
        [InlineData("3", "5", "-2")]
        [InlineData("1000", "1", "999")]
        [InlineData("1", "1000", "-999")]
        public void PositiveMinusPositive(string n1, string n2, string expected)
        {
            Assert.Equal(expected, NumberUtil.Sub(n1, n2));
        }

        [Theory]
        [InlineData("5", "-3", "8")]
        [InlineData("100", "-100", "200")]
        public void PositiveMinusNegative(string n1, string n2, string expected)
        {
            Assert.Equal(expected, NumberUtil.Sub(n1, n2));
        }

        [Theory]
        [InlineData("-5", "3", "-8")]
        [InlineData("-100", "100", "-200")]
        public void NegativeMinusPositive(string n1, string n2, string expected)
        {
            Assert.Equal(expected, NumberUtil.Sub(n1, n2));
        }

        [Theory]
        [InlineData("-5", "-3", "-2")]
        [InlineData("-3", "-5", "2")]
        [InlineData("-1000", "-1", "-999")]
        [InlineData("-1", "-1000", "999")]
        public void NegativeMinusNegative(string n1, string n2, string expected)
        {
            Assert.Equal(expected, NumberUtil.Sub(n1, n2));
        }

        [Theory]
        [InlineData("0", "5", "-5")]
        [InlineData("0", "-5", "5")]
        [InlineData("5", "0", "5")]
        public void ZeroCases(string n1, string n2, string expected)
        {
            Assert.Equal(expected, NumberUtil.Sub(n1, n2));
        }

        [Fact]
        public void LargeNumberTest()
        {
            var a = new string('9', 10000);
            var b = "1";
            var result = NumberUtil.Sub(a, b);

            Assert.Equal(new string('9', 9999) + "8", result);
        }
        
        // 连续借位极端测试（容易出错）
        [Theory]
        [InlineData("1000", "999", "1")]
        [InlineData("1000000", "1", "999999")]
        [InlineData("1000000000000", "1", "999999999999")]
        [InlineData("1000000", "999999", "1")]
        public void ContinuousBorrowTests(string n1, string n2, string expected)
        {
            Assert.Equal(expected, NumberUtil.Sub(n1, n2));
        }
        
        // 异常测试（很多人漏掉）
        [Fact]
        public void NullInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => NumberUtil.Sub(null, "1"));
            Assert.Throws<ArgumentNullException>(() => NumberUtil.Sub("1", null));
        }

        [Fact]
        public void InvalidFormatTest()
        {
            Assert.Throws<ArgumentException>(() => NumberUtil.Sub("abc", "1"));
            Assert.Throws<ArgumentException>(() => NumberUtil.Sub("1", "1.2"));
        }

        [Fact]
        public void OutOfRangeTest()
        {
            var big = new string('1', 100001);
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberUtil.Sub(big, "1"));
        }
        
        // 超大 10万位极限测试
        [Fact]
        public void MaxLengthBorrowTest()
        {
            var a = "1" + new string('0', 99999);
            var b = "1";
            var result = NumberUtil.Sub(a, b);

            Assert.Equal(new string('9', 99999), result);
        }
        
        // 符号边界极端
        [Theory]
        [InlineData("-1", "1", "-2")]
        [InlineData("1", "-1", "2")]
        [InlineData("-999999999", "999999999", "-1999999998")]
        [InlineData("999999999", "-999999999", "1999999998")]
        public void ExtremeSignTests(string n1, string n2, string expected)
        {
            Assert.Equal(expected, NumberUtil.Sub(n1, n2));
        }
        
        // 长度极端差异
        [Theory]
        [InlineData("100000000000000000000", "1", "99999999999999999999")]
        [InlineData("1", "100000000000000000000", "-99999999999999999999")]
        [InlineData("999999999999999999999999", "1", "999999999999999999999998")]
        public void HugeLengthDifferenceTests(string n1, string n2, string expected)
        {
            Assert.Equal(expected, NumberUtil.Sub(n1, n2));
        }
    }
}
