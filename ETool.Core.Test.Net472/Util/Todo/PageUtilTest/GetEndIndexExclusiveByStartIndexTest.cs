using System;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PageUtilTest
{
    public class GetEndIndexExclusiveByStartIndexTest
    {
        /// <summary>
        /// 测试正常输入下方法是否返回正确的结束索引（不包含）
        /// </summary>
        [Theory]
        [InlineData(11, 10, 21)]
        [InlineData(18, 10, 28)]
        [InlineData(33, 15, 48)]
        [InlineData(0, 10, 10)]
        [InlineData(0, 1, 1)]
        [InlineData(100, 50, 150)]
        public void GetEndIndexExclusiveByStartIndex_ValidInputs_ReturnsCorrectEndIndex(int startIndex, int pageSize, int expected)
        {
            var result = PageUtil.GetEndIndexExclusiveByStartIndex(startIndex, pageSize);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 测试当 startIndex 小于 0 时，应抛出 ArgumentOutOfRangeException，且异常参数名应为 "startIndex"
        /// </summary>
        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(int.MinValue)]
        public void GetEndIndexExclusiveByStartIndex_StartIndexNegative_ThrowsArgumentOutOfRangeException(int startIndex)
        {
            const int pageSize = 10;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetEndIndexExclusiveByStartIndex(startIndex, pageSize));
            Assert.Equal("startIndex", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageSize 小于等于 0 时，应抛出 ArgumentOutOfRangeException，且异常参数名应为 "pageSize"
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetEndIndexExclusiveByStartIndex_PageSizeNotPositive_ThrowsArgumentOutOfRangeException(int pageSize)
        {
            const int startIndex = 0;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetEndIndexExclusiveByStartIndex(startIndex, pageSize));
            Assert.Equal("pageSize", ex.ParamName);
        }

        /// <summary>
        /// 测试当 startIndex + pageSize > int.MaxValue 时，应抛出 OverflowException
        /// </summary>
        [Fact]
        public void GetEndIndexExclusiveByStartIndex_SumOverflowsInt_ThrowsOverflowException()
        {
            const int startIndex = int.MaxValue;
            const int pageSize = 1;
            Assert.Throws<OverflowException>(() => PageUtil.GetEndIndexExclusiveByStartIndex(startIndex, pageSize));
        }

        /// <summary>
        /// 边界测试，应正常返回，不抛出异常
        /// </summary>
        [Theory]
        [InlineData(int.MaxValue - 100, 100, int.MaxValue)]
        [InlineData(int.MaxValue - int.MaxValue / 2, int.MaxValue / 2, int.MaxValue)]
        [InlineData(0, int.MaxValue, int.MaxValue)]
        [InlineData(2_147_483_600, 40, 2_147_483_640)]
        public void GetEndIndexExclusiveByStartIndex_SumEqualsIntMaxValue_ReturnsCorrectly(int startIndex, int pageSize, int expected)
        {
            var result = PageUtil.GetEndIndexExclusiveByStartIndex(startIndex, pageSize);
            Assert.Equal(expected, result);
        }
    }
}
