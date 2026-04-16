using System;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PageUtilTest
{
    public class GetVisiblePageNumbersByTotalCountAndPageSizeTest
    {
        /// <summary>
        /// 测试正常输入下 GetVisiblePageNumbers 返回正确的页码列表
        /// </summary>
        [Theory]
        [InlineData(3, 97, 10, 5, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(5, 97, 10, 5, new[] { 3, 4, 5, 6, 7 })]
        [InlineData(10, 97, 10, 5, new[] { 6, 7, 8, 9, 10 })]
        [InlineData(1, 0, 10, 5, new int[0])] // totalCount = 0 → totalPages = 0 → empty
        [InlineData(1, 10, 10, 3, new[] { 1 })]
        [InlineData(1, 25, 10, 5, new[] { 1, 2, 3 })]
        [InlineData(2, 25, 10, 5, new[] { 1, 2, 3 })]
        [InlineData(3, 25, 10, 5, new[] { 1, 2, 3 })]
        public void GetVisiblePageNumbers_ValidInputs_ReturnsCorrectPageNumbers(int pageNumber, int totalCount, int pageSize, int visiblePageCount, int[] expected)
        {
            var result = PageUtil.GetVisiblePageNumbers(pageNumber, totalCount, pageSize, visiblePageCount);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 测试当 pageNumber 小于等于 0 时抛出 ArgumentOutOfRangeException，参数名为 "pageNumber"
        /// </summary>
        [Theory]
        [InlineData(0, 100, 10, 5)]
        [InlineData(-1, 100, 10, 5)]
        [InlineData(-10, 100, 10, 5)]
        public void GetVisiblePageNumbers_PageNumberNotPositive_ThrowsArgumentOutOfRangeException(int pageNumber, int totalCount, int pageSize, int visiblePageCount)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                PageUtil.GetVisiblePageNumbers(pageNumber, totalCount, pageSize, visiblePageCount));
            Assert.Equal("pageNumber", ex.ParamName);
        }

        /// <summary>
        /// 测试当 totalCount 小于 0 时抛出 ArgumentOutOfRangeException，参数名为 "totalCount"
        /// </summary>
        [Theory]
        [InlineData(1, -1, 10, 5)]
        [InlineData(1, -100, 10, 5)]
        public void GetVisiblePageNumbers_TotalCountNegative_ThrowsArgumentOutOfRangeException(int pageNumber, int totalCount, int pageSize, int visiblePageCount)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetVisiblePageNumbers(pageNumber, totalCount, pageSize, visiblePageCount));
            Assert.Equal("totalCount", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageSize 小于等于 0 时抛出 ArgumentOutOfRangeException，参数名为 "pageSize"
        /// </summary>
        [Theory]
        [InlineData(1, 100, 0, 5)]
        [InlineData(1, 100, -1, 5)]
        [InlineData(1, 100, -10, 5)]
        public void GetVisiblePageNumbers_PageSizeNotPositive_ThrowsArgumentOutOfRangeException(int pageNumber, int totalCount, int pageSize, int visiblePageCount)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetVisiblePageNumbers(pageNumber, totalCount, pageSize, visiblePageCount));
            Assert.Equal("pageSize", ex.ParamName);
        }

        /// <summary>
        /// 测试当 visiblePageCount 小于等于 0 时抛出 ArgumentOutOfRangeException，参数名为 "visiblePageCount"
        /// </summary>
        [Theory]
        [InlineData(1, 100, 10, 0)]
        [InlineData(1, 100, 10, -1)]
        [InlineData(1, 100, 10, -5)]
        public void GetVisiblePageNumbers_VisiblePageCountNotPositive_ThrowsArgumentOutOfRangeException(int pageNumber, int totalCount, int pageSize, int visiblePageCount)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetVisiblePageNumbers(pageNumber, totalCount, pageSize, visiblePageCount));
            Assert.Equal("visiblePageCount", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageNumber 超过总页数时抛出 ArgumentOutOfRangeException，参数名为 "pageNumber"
        /// </summary>
        [Theory]
        [InlineData(11, 100, 10, 5)] // 总页数 = 10，当前页 = 11 → 无效
        [InlineData(3, 15, 10, 5)]
        public void GetVisiblePageNumbers_PageNumberExceedsTotalPages_ThrowsArgumentOutOfRangeException(int pageNumber, int totalCount, int pageSize, int visiblePageCount)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetVisiblePageNumbers(pageNumber, totalCount, pageSize, visiblePageCount));
            Assert.Equal("pageNumber", ex.ParamName);
        }

        /// <summary>
        /// 边界测试：当 totalCount 为 0 时，总页数为 0，任何 pageNumber >= 1 都应抛出异常（因为无页可显示）
        /// </summary>
        [Fact]
        public void GetVisiblePageNumbers_TotalCountZero_PageNumberOne_ThrowsArgumentOutOfRangeException()
        {
            var list = PageUtil.GetVisiblePageNumbers(1, 0, 10, 5);
            Assert.Empty(list);
        }
    }
}
