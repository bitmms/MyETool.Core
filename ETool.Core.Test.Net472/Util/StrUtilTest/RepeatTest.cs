using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class RepeatTest
    {
        /// <summary>
        /// 测试基本重复功能
        /// </summary>
        [Fact]
        public void Repeat_WithValidInputs_ReturnsCorrectResult()
        {
            // Arrange
            string input = "hello";
            int count = 3;
            string sep = "-";

            // Act
            string result = StrUtil.Repeat(input, count, sep);

            // Assert
            Assert.Equal("hello-hello-hello", result);
        }

        /// <summary>
        /// 测试默认分隔符（空格）
        /// </summary>
        [Fact]
        public void Repeat_WithDefaultSeparator_ReturnsCorrectResult()
        {
            // Arrange
            string input = "abc";
            int count = 2;

            // Act
            string result = StrUtil.Repeat(input, count);

            // Assert
            Assert.Equal("abc abc", result);
        }

        /// <summary>
        /// 测试重复0次返回空字符串
        /// </summary>
        [Fact]
        public void Repeat_WithZeroCount_ReturnsEmptyString()
        {
            // Arrange
            string input = "hello";
            int count = 0;

            // Act
            string result = StrUtil.Repeat(input, count);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        /// <summary>
        /// 测试重复1次返回原字符串
        /// </summary>
        [Fact]
        public void Repeat_WithCountOne_ReturnsOriginalString()
        {
            // Arrange
            string input = "hello";
            int count = 1;

            // Act
            string result = StrUtil.Repeat(input, count);

            // Assert
            Assert.Equal("hello", result);
        }

        /// <summary>
        /// 测试分隔符为空字符串的情况
        /// </summary>
        [Fact]
        public void Repeat_WithEmptySeparator_ReturnsCorrectResult()
        {
            // Arrange
            string input = "hi";
            int count = 3;
            string sep = "";

            // Act
            string result = StrUtil.Repeat(input, count, sep);

            // Assert
            Assert.Equal("hihihi", result);
        }

        /// <summary>
        /// 测试输入字符串为空字符串的情况
        /// </summary>
        [Fact]
        public void Repeat_WithEmptyString_ReturnsCorrectResult()
        {
            // Arrange
            string input = "";
            int count = 5;
            string sep = "-";

            // Act
            string result = StrUtil.Repeat(input, count, sep);

            // Assert
            Assert.Equal("----", result);
        }

        /// <summary>
        /// 测试s为null时抛出异常
        /// </summary>
        [Fact]
        public void Repeat_WithNullInput_ThrowsArgumentNullException()
        {
            // Arrange
            string input = null;
            int count = 3;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => StrUtil.Repeat(input, count));
        }

        /// <summary>
        /// 测试sep为null时抛出异常
        /// </summary>
        [Fact]
        public void Repeat_WithNullSeparator_ThrowsArgumentNullException()
        {
            // Arrange
            string input = "hello";
            int count = 3;
            string sep = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => StrUtil.Repeat(input, count, sep));
        }

        /// <summary>
        /// 测试count为负数时抛出异常
        /// </summary>
        [Fact]
        public void Repeat_WithNegativeCount_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            string input = "hello";
            int count = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => StrUtil.Repeat(input, count));
        }

        /// <summary>
        /// 测试大count但不超过int.MaxValue的情况
        /// </summary>
        [Fact]
        public void Repeat_WithLargeCount_ReturnsCorrectResult()
        {
            // Arrange
            string input = "a";
            int count = 1000;

            // Act
            string result = StrUtil.Repeat(input, count, "");

            // Assert
            // 验证长度是否正确
            Assert.Equal(count, result.Length);

            // 验证内容是否正确
            Assert.Equal(new string('a', count), result);
        }

        /// <summary>
        /// 测试超过int.MaxValue限制时抛出异常
        /// </summary>
        [Fact]
        public void Repeat_WithCountExceedingLimit_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            string input = "a";
            // 计算一个会导致结果长度超过int.MaxValue的count
            // 假设int.MaxValue = 2147483647
            long maxAllowedLength = int.MaxValue;
            int count = (int)(maxAllowedLength / input.Length) + 2; // 确保超出

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => StrUtil.Repeat(input, count, ""));
        }

        /// <summary>
        /// 使用Theory进行参数化测试
        /// </summary>
        [Theory]
        [InlineData("a", 0, "-", "")]
        [InlineData("a", 1, "-", "a")]
        [InlineData("a", 2, "-", "a-a")]
        [InlineData("abc", 3, "*", "abc*abc*abc")]
        [InlineData("", 5, "-", "----")]
        [InlineData("x", 10, "", "xxxxxxxxxx")]
        [InlineData("hello", 2, " ", "hello hello")]
        public void Repeat_WithVariousInputs_ReturnsExpectedResult(
            string input, int count, string sep, string expected)
        {
            // Act
            string result = StrUtil.Repeat(input, count, sep);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
