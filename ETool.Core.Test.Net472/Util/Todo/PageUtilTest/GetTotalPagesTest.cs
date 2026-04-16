using System;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PageUtilTest
{
    public class GetTotalPagesTest
    {
        /// <summary>
        /// 测试正常输入情况下 GetTotalPages 方法是否返回正确的总页数
        /// </summary>
        [Theory]
        [InlineData(0, 10, 0)]
        [InlineData(1, 10, 1)]
        [InlineData(10, 10, 1)]
        [InlineData(11, 10, 2)]
        [InlineData(97, 10, 10)]
        [InlineData(100, 10, 10)]
        [InlineData(101, 10, 11)]
        [InlineData(5, 1, 5)]
        [InlineData(1, 1, 1)]
        public void GetTotalPages_ValidInputs_ReturnsCorrectPageCount(int totalCount, int pageSize, int expected)
        {
            var result = PageUtil.GetTotalPages(totalCount, pageSize);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 测试当 totalCount 小于 0 时，方法应抛出 ArgumentOutOfRangeException 异常，并且异常参数名称应为 "totalCount"。
        /// </summary>
        [Fact]
        public void GetTotalPages_TotalCountNegative_ThrowsArgumentOutOfRangeException()
        {
            const int totalCount = -1;
            const int pageSize = 10;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetTotalPages(totalCount, pageSize));
            Assert.Equal("totalCount", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageSize 小于或等于 0 时，方法应抛出 ArgumentOutOfRangeException 异常，并且异常参数名称应为 "pageSize"。
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void GetTotalPages_PageSizeNotPositive_ThrowsArgumentOutOfRangeException(int pageSize)
        {
            const int totalCount = 10;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetTotalPages(totalCount, pageSize));
            Assert.Equal("pageSize", ex.ParamName);
        }
    }
}
