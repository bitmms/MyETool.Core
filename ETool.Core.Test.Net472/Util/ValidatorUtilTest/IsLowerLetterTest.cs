using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsLowerLetterTest
    {
        [Theory]
        [InlineData('a', true, "合法")]
        [InlineData('b', true, "合法")]
        [InlineData('c', true, "合法")]
        [InlineData('d', true, "合法")]
        [InlineData('A', false, "大写不合法")]
        [InlineData('B', false, "大写不合法")]
        [InlineData('C', false, "大写不合法")]
        [InlineData('D', false, "大写不合法")]
        [InlineData('ａ', false, "全角不合法")]
        [InlineData('ｂ', false, "全角不合法")]
        [InlineData('ｃ', false, "全角不合法")]
        [InlineData('ｄ', false, "全角不合法")]
        [InlineData('.', false, "特殊字符不合法")]
        [InlineData('．', false, "特殊字符不合法")]
        public void Test(char input, bool expectedResult, string errorMessage)
        {
            var result = ValidatorUtil.IsLowerLetter(input);
            Assert.True(result == expectedResult, errorMessage);
        }
    }
}
