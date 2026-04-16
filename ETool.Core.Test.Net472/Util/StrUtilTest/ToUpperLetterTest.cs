using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class ToUpperLetterTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.ToUpperLetter(null));
        }
        
        [Theory]
        [InlineData("abcDDdfd", "ABCDDDFD")]
        [InlineData("123456sdsDD", "123456SDSDD")]
        [InlineData("123456 DD", "123456 DD")]
        public void ToUpperLetterTest_(string input, string expected)
        {
            Assert.Equal(expected, StrUtil.ToUpperLetter(input));
        }
    }
}
