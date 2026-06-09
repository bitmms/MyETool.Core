using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.CharUtilTest
{
    public class ToLowerLetterTest
    {
        [Theory]
        [InlineData('A', 'a', "A -> a")]
        [InlineData('B', 'b', "B -> b")]
        [InlineData('C', 'c', "C -> c")]
        [InlineData('.', '.', ". -> c")]
        [InlineData('Ａ', 'ａ', "Ａ -> Ａ")]
        [InlineData('ａ', 'ａ', "ａ -> ａ")]
        public void Test(char input, char expectedResult, string errorMessage)
        {
            var result = CharUtil.ToLowerLetter(input);
            Assert.True(result == expectedResult, errorMessage);
        }
    }
}
