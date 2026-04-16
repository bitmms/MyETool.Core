using System;
using System.Collections.Generic;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PageUtilTest
{
    public class GetPageDisplayRangesListTest
    {
        public static IEnumerable<object[]> ValidInputTestData =>
            new List<object[]>
            {
                new object[] { 0, 10, Array.Empty<(int, int, int)>() },
                new object[] { 5, 10, new[] { (1, 1, 5) } },
                new object[] { 10, 10, new[] { (1, 1, 10) } },
                new object[] { 25, 10, new[] { (1, 1, 10), (2, 11, 20), (3, 21, 25) } },
                new object[] { 21, 5, new[] { (1, 1, 5), (2, 6, 10), (3, 11, 15), (4, 16, 20), (5, 21, 21) } },
                new object[] { 1, 1, new[] { (1, 1, 1) } }
            };

        /// <summary>
        /// 测试正常输入下返回正确的分页范围列表
        /// </summary>
        [Theory]
        [MemberData(nameof(ValidInputTestData))]
        public void GetPageDisplayRangesList_ValidInputs_ReturnsCorrectRanges(int totalCount, int pageSize, (int pageNumber, int DisplayStart, int DisplayEnd)[] expected)
        {
            var result = PageUtil.GetPageDisplayRangesList(totalCount, pageSize);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 测试当 totalCount 小于 0 时抛出 ArgumentOutOfRangeException，参数名为 "totalCount"
        /// </summary>
        [Theory]
        [InlineData(-1, 10)]
        [InlineData(-100, 1)]
        public void GetPageDisplayRangesList_TotalCountNegative_ThrowsArgumentOutOfRangeException(int totalCount, int pageSize)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetPageDisplayRangesList(totalCount, pageSize));
            Assert.Equal("totalCount", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageSize 小于等于 0 时抛出 ArgumentOutOfRangeException，参数名为 "pageSize"
        /// </summary>
        [Theory]
        [InlineData(10, 0)]
        [InlineData(10, -1)]
        [InlineData(0, -5)]
        public void GetPageDisplayRangesList_PageSizeNotPositive_ThrowsArgumentOutOfRangeException(int totalCount, int pageSize)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetPageDisplayRangesList(totalCount, pageSize));
            Assert.Equal("pageSize", ex.ParamName);
        }

        /// <summary>
        /// 边界测试：大数值但不溢出（验证 DisplayEnd 计算是否正确）
        /// </summary>
        [Fact]
        public void GetPageDisplayRangesList_LargeNumbers_NoOverflow()
        {
            // 最大安全值：确保 (i-1)*pageSize + 1 不溢出
            const int totalCount = int.MaxValue - 10;
            const int pageSize = 1000;

            var result = PageUtil.GetPageDisplayRangesList(totalCount, pageSize);

            Assert.NotEmpty(result);
            Assert.True(result[0].DisplayStart <= result[0].DisplayEnd);
            Assert.Equal(totalCount, result[result.Count - 1].DisplayEnd);
        }

        /// <summary>
        /// 验证 DisplayStart 和 DisplayEnd 始终满足：1 ≤ DisplayStart ≤ DisplayEnd ≤ totalCount（当 totalCount > 0）
        /// </summary>
        [Theory]
        [InlineData(100, 7)]
        [InlineData(999, 100)]
        [InlineData(1, 10)]
        public void GetPageDisplayRangesList_RangesAreValid(int totalCount, int pageSize)
        {
            var ranges = PageUtil.GetPageDisplayRangesList(totalCount, pageSize);

            int expectedPage = 1;
            int runningStart = 1;

            foreach (var (pageNumber, start, end) in ranges)
            {
                Assert.Equal(expectedPage, pageNumber);
                Assert.Equal(runningStart, start);
                Assert.True(start <= end, $"Start ({start}) > End ({end})");
                Assert.True(end <= totalCount, $"End ({end}) > totalCount ({totalCount})");

                runningStart = end + 1;
                expectedPage++;
            }

            // 验证最后一条记录确实到 totalCount
            if (totalCount > 0)
            {
                Assert.NotEmpty(ranges);
                Assert.Equal(totalCount, ranges[ranges.Count - 1].DisplayEnd);
            }
        }
    }
}
