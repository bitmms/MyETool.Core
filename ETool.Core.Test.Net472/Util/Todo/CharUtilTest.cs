using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util
{
    public class CharUtilTest
    {
        [Fact]
        public void IsDigitTest()
        {
            const char c1 = '0';
            const char c2 = '9';
            const char c3 = ' ';
            const char c4 = '.';

            var result1 = CharUtil.IsDigit(c1);
            var result2 = CharUtil.IsDigit(c2);
            var result3 = CharUtil.IsDigit(c3);
            var result4 = CharUtil.IsDigit(c4);

            Assert.True(result1, $"字符 '{c1}' 应该被判断为数字字符");
            Assert.True(result2, $"字符 '{c2}' 应该被判断为数字字符");
            Assert.False(result3, $"字符 '{c3}' 不应该被判断为数字字符");
            Assert.False(result4, $"字符 '{c4}' 不应该被判断为数字字符");
        }

        [Theory]
        [InlineData('a', true, "字母 '0' 应判定为字母")]
        [InlineData('z', true, "字母 '9' 应判定为字母")]
        [InlineData('A', true, "符号 'A' 应判定为字母")]
        [InlineData('Z', true, "符号 'Z' 应判定为字母")]
        [InlineData('ａ', false, "符号 'ａ' 不应判定为字母")]
        [InlineData('ｂ', false, "符号 'ｂ' 不应判定为字母")]
        [InlineData('Ａ', false, "符号 'Ａ' 不应判定为字母")]
        [InlineData('+', false, "符号 '+' 不应判定为字母")]
        [InlineData('-', false, "符号 '-' 不应判定为字母")]
        [InlineData('.', false, "符号 '.' 不应判定为字母")]
        [InlineData(' ', false, "符号 ' ' 不应判定为字母")]
        public void IsLetterTest(char testChar, bool expectedResult, string errorMsg)
        {
            var actualResult = CharUtil.IsLetter(testChar);
            Assert.True(actualResult == expectedResult, errorMsg);
        }
    }
}
