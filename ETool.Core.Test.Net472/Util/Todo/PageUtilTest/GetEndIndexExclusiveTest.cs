using System;
using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PageUtilTest
{
    public class GetEndIndexExclusiveTest
    {
        /// <summary>
        /// 测试正常输入下 GetEndIndexExclusive 是否返回正确的结束索引（不包含）
        /// </summary>
        [Theory]
        [InlineData(1, 10, 10)]
        [InlineData(2, 10, 20)]
        [InlineData(3, 5, 15)]
        [InlineData(1, 1, 1)]
        [InlineData(100, 20, 2000)]
        public void GetEndIndexExclusive_ValidInputs_ReturnsCorrectEndIndex(int pageNumber, int pageSize, int expected)
        {
            var result = PageUtil.GetEndIndexExclusive(pageNumber, pageSize);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 测试当 pageNumber 小于等于 0 时，应抛出 ArgumentOutOfRangeException，且异常参数名应为 "pageNumber"。
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetEndIndexExclusive_PageNumberNotPositive_ThrowsArgumentOutOfRangeException(int pageNumber)
        {
            const int pageSize = 10;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetEndIndexExclusive(pageNumber, pageSize));
            Assert.Equal("pageNumber", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageSize 小于等于 0 时，应抛出 ArgumentOutOfRangeException，且异常参数名应为 "pageSize"。
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetEndIndexExclusive_PageSizeNotPositive_ThrowsArgumentOutOfRangeException(int pageSize)
        {
            const int pageNumber = 1;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => PageUtil.GetEndIndexExclusive(pageNumber, pageSize));
            Assert.Equal("pageSize", ex.ParamName);
        }

        /// <summary>
        /// 测试当 pageNumber * pageSize 的结果超过 int.MaxValue 时，应抛出 OverflowException。
        /// </summary>
        [Fact]
        public void GetEndIndexExclusive_ResultOverflowsInt_ThrowsOverflowException()
        {
            // int.MaxValue = 2,147,483,647
            // 选择 pageNumber 和 pageSize 使得乘积 > int.MaxValue
            const int pageNumber = int.MaxValue;
            const int pageSize = 2;
            Assert.Throws<OverflowException>(() => PageUtil.GetEndIndexExclusive(pageNumber, pageSize));
        }

        /// <summary>
        /// 边界测试：刚好不溢出的情况（即结果等于 int.MaxValue）
        /// 注意：GetEndIndexExclusive 返回的是 pageNumber * pageSize，
        /// 所以要满足 pageNumber * pageSize == int.MaxValue
        /// 由于 int.MaxValue 是奇数（2147483647），无法被 2 整除，但可以取 pageSize=1
        /// </summary>
        [Theory]
        [InlineData(int.MaxValue, 1, int.MaxValue)]
        [InlineData(1_073_741_823, 2, 2_147_483_646)]
        public void GetEndIndexExclusive_ResultEqualsOrBelowIntMaxValue_DoesNotThrow(int pageNumber, int pageSize, int expected)
        {
            var result = PageUtil.GetEndIndexExclusive(pageNumber, pageSize);
            Assert.Equal(expected, result);
        }
    }
}
