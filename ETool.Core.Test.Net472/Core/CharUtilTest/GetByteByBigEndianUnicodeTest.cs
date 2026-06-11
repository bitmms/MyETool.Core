using ETool.Core.Core;
using Xunit;

namespace ETool.Core.Test.Net472.Core.CharUtilTest
{
    public class GetByteByBigEndianUnicodeTest
    {
        [Theory]
        [InlineData('a', 0, 97)]
        [InlineData('中', 78, 45)]
        public void CharTest(char input, byte expectedHigh, byte expectedLow)
        {
            (byte high, byte low) result = CharUtil.GetByteByBigEndianUnicode(input);
            Assert.Equal(result.high, expectedHigh);
            Assert.Equal(result.low, expectedLow);
        }

        [Fact]
        public void CharArrayTest()
        {
            byte[] result = CharUtil.GetByteByBigEndianUnicode(new[] { 'a', '中' });
            Assert.Equal(0, result[0]);
            Assert.Equal(97, result[1]);
            Assert.Equal(78, result[2]);
            Assert.Equal(45, result[3]);
        }
    }
}
