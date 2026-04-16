using System;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PageUtilTest
{
    public class GetStartIndexTest
    {
        /// <summary>
        /// 测试正常输入下 GetStartIndex 是否返回正确的起始索引（从 0 开始）
        /// </summary>
        [Theory]
        [InlineData(1, 10, 0)]
        [InlineData(2, 10, 10)]
        [InlineData(3, 5, 10)]
        [InlineData(1, 1, 0)]
        [InlineData(100, 20, 1980)]
        public void GetStartIndex_ValidInputs_ReturnsCorrectStartIndex(int pageNumber, int pageSize, int expected)
        {
            var result = PageUtil.GetStartIndex(pageNumber, pageSize);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 测试当 pageNumber 小于等于 0 时，应抛出 ArgumentOutOfRangeException，且异常参数名应为 "pageNumber"。
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetStartIndex_PageNumberNotPositive_ThrowsArgumentOutOfRangeException(int pageNumber)
        {
            const int pageSize = 10;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetStartIndex(pageNumber, pageSize));
            Assert.Equal("pageNumber", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageSize 小于等于 0 时，应抛出 ArgumentOutOfRangeException，且异常参数名应为 "pageSize"。
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetStartIndex_PageSizeNotPositive_ThrowsArgumentOutOfRangeException(int pageSize)
        {
            const int pageNumber = 1;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetStartIndex(pageNumber, pageSize));
            Assert.Equal("pageSize", ex.ParamName);
        }

        /// <summary>
        /// 测试当 (pageNumber - 1) * pageSize 的结果超过 int.MaxValue 时，应抛出 OverflowException。
        /// </summary>
        [Fact]
        public void GetStartIndex_ResultOverflowsInt_ThrowsOverflowException()
        {
            const int pageNumber = int.MaxValue;
            const int pageSize = 2;
            Assert.Throws<OverflowException>(() => PageUtil.GetStartIndex(pageNumber, pageSize));
        }

        /// <summary>
        /// 边界测试：刚好不溢出的情况（即结果等于 int.MaxValue）
        /// </summary>
        [Theory]
        [InlineData(int.MaxValue, 1, int.MaxValue - 1)]
        [InlineData(1_073_741_824, 2, int.MaxValue - 1)] // 2^30, 2, (2^30 -1) * 2 = 2^31 - 2 = int.MaxValue - 1
        public void GetStartIndex_ResultEqualsIntMaxValue_DoesNotThrow(int pageNumber, int pageSize, int expected)
        {
            var result = PageUtil.GetStartIndex(pageNumber, pageSize);
            Assert.Equal(expected, result);
        }
    }
}
