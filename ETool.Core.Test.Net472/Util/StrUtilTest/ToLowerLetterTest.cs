using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class ToLowerLetterTest
    {
        [Fact]
        public void Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StrUtil.ToLowerLetter(null));
        }

        [Theory]
        [InlineData("abcDDdfd", "abcdddfd")]
        [InlineData("123456DD", "123456dd")]
        [InlineData("123456 DD", "123456 dd")]
        public void ToLowerLetter_(string input, string expected)
        {
            Assert.Equal(expected, StrUtil.ToLowerLetter(input));
        }
    }
}
