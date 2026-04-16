using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.CharUtilTest
{
    public class ToUpperLetterTest
    {
        [Theory]
        [InlineData('a', 'A', "a -> A")]
        [InlineData('b', 'B', "b -> B")]
        [InlineData('c', 'C', "c -> C")]
        [InlineData('.', '.', ". -> c")]
        [InlineData('Ａ', 'Ａ', "Ａ -> Ａ")]
        [InlineData('ａ', 'ａ', "ａ -> ａ")]
        public void Test(char input, char expectedResult, string errorMessage)
        {
            var result = CharUtil.ToUpperLetter(input);
            Assert.True(result == expectedResult, errorMessage);
        }
    }
}
