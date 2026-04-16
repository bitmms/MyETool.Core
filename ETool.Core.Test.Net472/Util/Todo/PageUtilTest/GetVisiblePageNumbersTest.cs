using System;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PageUtilTest
{
    public class GetVisiblePageNumbersTest
    {
        /// <summary>
        /// 测试正常输入下返回正确的可见页码列表
        /// </summary>
        [Theory]
        [InlineData(1, 10, 5, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(3, 10, 5, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(5, 10, 5, new[] { 3, 4, 5, 6, 7 })]
        [InlineData(10, 10, 5, new[] { 6, 7, 8, 9, 10 })]
        [InlineData(1, 3, 7, new[] { 1, 2, 3 })] // visiblePageCount > totalPages
        [InlineData(2, 5, 3, new[] { 1, 2, 3 })]
        [InlineData(4, 5, 3, new[] { 3, 4, 5 })]
        [InlineData(3, 5, 3, new[] { 2, 3, 4 })]
        [InlineData(1, 1, 5, new[] { 1 })]
        [InlineData(1, 2, 1, new[] { 1 })]
        [InlineData(2, 2, 1, new[] { 2 })]
        public void GetVisiblePageNumbers_ValidInputs_ReturnsExpectedArray(
            int pageNumber, int totalPages, int visiblePageCount, int[] expected)
        {
            var result = PageUtil.GetVisiblePageNumbers(pageNumber, totalPages, visiblePageCount);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 测试 totalPages 等于 0 时返回空数组
        /// </summary>
        [Fact]
        public void GetVisiblePageNumbers_TotalPagesZero_ReturnsEmptyArray()
        {
            var result = PageUtil.GetVisiblePageNumbers(1, 0, 5);
            Assert.Empty(result);
            Assert.IsType<int[]>(result);
        }

        /// <summary>
        /// 测试 pageNumber 小于等于 0 时抛出 ArgumentOutOfRangeException（参数名 "pageNumber"）
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetVisiblePageNumbers_PageNumberNotPositive_ThrowsArgumentOutOfRangeException(int pageNumber)
        {
            const int totalPages = 10;
            const int visiblePageCount = 5;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetVisiblePageNumbers(pageNumber, totalPages, visiblePageCount));
            Assert.Equal("pageNumber", ex.ParamName);
        }

        /// <summary>
        /// 测试 totalPages 小于 0 时抛出 ArgumentOutOfRangeException（参数名 "totalPages"）
        /// </summary>
        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetVisiblePageNumbers_TotalPagesNegative_ThrowsArgumentOutOfRangeException(int totalPages)
        {
            const int pageNumber = 1;
            const int visiblePageCount = 5;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetVisiblePageNumbers(pageNumber, totalPages, visiblePageCount));
            Assert.Equal("totalPages", ex.ParamName);
        }

        /// <summary>
        /// 测试 pageNumber 大于 totalPages 时抛出 ArgumentOutOfRangeException（参数名 "pageNumber"）
        /// </summary>
        [Theory]
        [InlineData(6, 5)]
        [InlineData(100, 10)]
        public void GetVisiblePageNumbers_PageNumberExceedsTotalPages_ThrowsArgumentOutOfRangeException(int pageNumber, int totalPages)
        {
            const int visiblePageCount = 5;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetVisiblePageNumbers(pageNumber, totalPages, visiblePageCount));
            Assert.Equal("pageNumber", ex.ParamName);
        }

        /// <summary>
        /// 测试 visiblePageCount 小于等于 0 时抛出 ArgumentOutOfRangeException（参数名 "visiblePageCount"）
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void GetVisiblePageNumbers_VisiblePageCountNotPositive_ThrowsArgumentOutOfRangeException(int visiblePageCount)
        {
            const int pageNumber = 1;
            const int totalPages = 10;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetVisiblePageNumbers(pageNumber, totalPages, visiblePageCount));
            Assert.Equal("visiblePageCount", ex.ParamName);
        }

        /// <summary>
        /// 额外边界测试：奇数 vs 偶数 visiblePageCount 行为
        /// 根据实现，偶数时当前页偏左（left--）
        /// </summary>
        [Fact]
        public void GetVisiblePageNumbers_EvenVisiblePageCount_CurrentPageBiasedLeft()
        {
            Assert.Equal(new[] { 4, 5, 6, 7 }, PageUtil.GetVisiblePageNumbers(5, 10, 4));
            Assert.Equal(new[] { 1, 2, 3, 4 }, PageUtil.GetVisiblePageNumbers(1, 10, 4));
            Assert.Equal(new[] { 7, 8, 9, 10 }, PageUtil.GetVisiblePageNumbers(10, 10, 4));
        }

        /// <summary>
        /// 确保返回数组长度始终为 min(totalPages, visiblePageCount)
        /// </summary>
        [Theory]
        [InlineData(1, 3, 10)] // totalPages=3, visible=10 → length=3
        [InlineData(2, 5, 3)] // totalPages=5, visible=3 → length=3
        [InlineData(1, 0, 5)] // totalPages=0 → length=0
        public void GetVisiblePageNumbers_ResultLengthIsCorrect(int pageNumber, int totalPages, int visiblePageCount)
        {
            var result = PageUtil.GetVisiblePageNumbers(pageNumber, totalPages, visiblePageCount);
            var expectedLength = Math.Min(totalPages, visiblePageCount);
            Assert.Equal(expectedLength, result.Length);
        }
    }
}
