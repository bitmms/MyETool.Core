using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsUpperLetterTest
    {
        [Theory]
        [InlineData('a', false, "小写不合法")]
        [InlineData('b', false, "小写不合法")]
        [InlineData('c', false, "小写不合法")]
        [InlineData('d', false, "小写不合法")]
        [InlineData('A', true, "合法")]
        [InlineData('B', true, "合法")]
        [InlineData('C', true, "合法")]
        [InlineData('D', true, "合法")]
        [InlineData('ａ', false, "全角不合法")]
        [InlineData('ｂ', false, "全角不合法")]
        [InlineData('ｃ', false, "全角不合法")]
        [InlineData('ｄ', false, "全角不合法")]
        [InlineData('.', false, "特殊字符不合法")]
        [InlineData('．', false, "特殊字符不合法")]
        public void Test(char input, bool expectedResult, string errorMessage)
        {
            var result = ValidatorUtil.IsUpperLetter(input);
            Assert.True(result == expectedResult, errorMessage);
        }
    }
}
