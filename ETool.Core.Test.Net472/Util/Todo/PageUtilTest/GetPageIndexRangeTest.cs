using System;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PageUtilTest
{
    public class GetPageIndexRangeTest
    {
        /// <summary>
        /// 测试正常输入下 GetPageIndexRange 是否返回正确的 (StartIndex, EndIndex)
        /// </summary>
        [Theory]
        [InlineData(1, 10, 0, 10)]
        [InlineData(2, 10, 10, 20)]
        [InlineData(3, 5, 10, 15)]
        [InlineData(1, 1, 0, 1)]
        [InlineData(100, 20, 1980, 2000)]
        public void GetPageIndexRange_ValidInputs_ReturnsCorrectRange(int pageNumber, int pageSize, int expectedStart, int expectedEnd)
        {
            var (start, end) = PageUtil.GetPageIndexRange(pageNumber, pageSize);
            Assert.Equal((expectedStart, expectedEnd), (start, end));
        }

        /// <summary>
        /// 测试当 pageNumber 小于等于 0 时，抛出 ArgumentOutOfRangeException（参数名 "pageNumber"）
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetPageIndexRange_PageNumberNotPositive_ThrowsArgumentOutOfRangeException(int pageNumber)
        {
            const int pageSize = 10;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetPageIndexRange(pageNumber, pageSize));
            Assert.Equal("pageNumber", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageSize 小于等于 0 时，抛出 ArgumentOutOfRangeException（参数名 "pageSize"）
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetPageIndexRange_PageSizeNotPositive_ThrowsArgumentOutOfRangeException(int pageSize)
        {
            const int pageNumber = 1;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetPageIndexRange(pageNumber, pageSize));
            Assert.Equal("pageSize", ex.ParamName);
        }

        /// <summary>
        /// 测试当 (pageNumber - 1) * pageSize 溢出 int 范围时，抛出 OverflowException
        /// （即 GetStartIndex 溢出）
        /// </summary>
        [Fact]
        public void GetPageIndexRange_StartIndexOverflows_ThrowsOverflowException()
        {
            // (int.MaxValue - 1) / 2 + 1 可能不够大，直接用极端值
            const int pageNumber = int.MaxValue;
            const int pageSize = 2; // (2147483647 - 1) * 2 = 超过 int.MaxValue
            Assert.Throws<OverflowException>(() => PageUtil.GetPageIndexRange(pageNumber, pageSize));
        }

        /// <summary>
        /// 测试当 startIndex + pageSize 溢出 int 范围时，抛出 OverflowException
        /// 即：GetStartIndex 不溢出，但 endIndex 溢出
        /// </summary>
        [Fact]
        public void GetPageIndexRange_EndIndexOverflows_ThrowsOverflowException()
        {
            // 选择 startIndex = int.MaxValue - 1, pageSize = 2 → endIndex = int.MaxValue + 1 → 溢出
            // 如何构造？需要 (pageNumber - 1) * pageSize = int.MaxValue - 1
            // 简单方式：让 startIndex = int.MaxValue - 1, pageSize = 2
            // 所以 pageNumber = ((int.MaxValue - 1) / pageSize) + 1
            // 但为了简单，我们可以反向构造：
            // 设 pageSize = 2，则 startIndex = int.MaxValue - 1 ⇒ (pageNumber - 1) = (int.MaxValue - 1) / 2
            // 由于 int.MaxValue 是奇数，int.MaxValue - 1 是偶数，可整除

            int pageSize = 2;
            int startIndex = int.MaxValue - 1; // 最大合法 startIndex
            int pageNumber = (startIndex / pageSize) + 1; // 因为 startIndex = (pageNumber - 1) * pageSize

            // 验证 startIndex 计算正确
            Assert.Equal(startIndex, (pageNumber - 1) * pageSize);

            // 此时 endIndex = startIndex + pageSize = int.MaxValue + 1 → 溢出
            Assert.Throws<OverflowException>(() => PageUtil.GetPageIndexRange(pageNumber, pageSize));
        }

        /// <summary>
        /// 边界测试：刚好不溢出的情况
        /// - StartIndex = int.MaxValue - 1, EndIndex = int.MaxValue （pageSize = 1）
        /// - 或 StartIndex = 0, EndIndex = int.MaxValue（pageSize = int.MaxValue, pageNumber = 1）
        /// </summary>
        [Theory]
        [InlineData(1, int.MaxValue, 0, int.MaxValue)] // 第一页就占满整个 int 范围
        [InlineData(int.MaxValue, 1, int.MaxValue - 1, int.MaxValue)] // 最后一个元素
        public void GetPageIndexRange_BoundaryNoOverflow_ReturnsCorrectRange(
            int pageNumber, int pageSize, int expectedStart, int expectedEnd)
        {
            var (start, end) = PageUtil.GetPageIndexRange(pageNumber, pageSize);
            Assert.Equal(expectedStart, start);
            Assert.Equal(expectedEnd, end);
        }
    }
}
