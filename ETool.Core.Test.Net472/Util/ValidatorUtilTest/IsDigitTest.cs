using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsDigitTest
    {
        [Theory]
        [InlineData('1', true, "合法")]
        [InlineData('2', true, "合法")]
        [InlineData('3', true, "合法")]
        [InlineData('5', true, "合法")]
        [InlineData('6', true, "合法")]
        [InlineData('8', true, "合法")]
        [InlineData('9', true, "合法")]
        [InlineData('0', true, "合法")]
        [InlineData('０', false, "全角不合法")]
        [InlineData('１', false, "全角不合法")]
        [InlineData('２', false, "全角不合法")]
        [InlineData('３', false, "全角不合法")]
        [InlineData('５', false, "全角不合法")]
        [InlineData('６', false, "全角不合法")]
        [InlineData('８', false, "全角不合法")]
        [InlineData('９', false, "全角 9 不合法")]
        [InlineData('.', false, "特殊字符不合法")]
        [InlineData('．', false, "特殊字符不合法")]
        public void Test(char input, bool expectedResult, string errorMessage)
        {
            var result = ValidatorUtil.IsDigit(input);
            Assert.True(result == expectedResult, errorMessage);
        }
    }
}
