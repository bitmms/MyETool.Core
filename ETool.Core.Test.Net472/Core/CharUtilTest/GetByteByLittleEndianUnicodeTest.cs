using ETool.Core.Core;
using Xunit;

namespace ETool.Core.Test.Net472.Core.CharUtilTest
{
    public class GetByteByLittleEndianUnicodeTest
    {
        [Theory]
        [InlineData('a', 97, 0)]
        [InlineData('中', 45, 78)]
        public void CharTest(char input, byte expectedLow, byte expectedHigh)
        {
            (byte low, byte high) result = CharUtil.GetByteByLittleEndianUnicode(input);
            Assert.Equal(result.low, expectedLow);
            Assert.Equal(result.high, expectedHigh);
        }

        [Fact]
        public void CharArrayTest()
        {
            byte[] result = CharUtil.GetByteByLittleEndianUnicode(new[] { 'a', '中' });
            Assert.Equal(97, result[0]);
            Assert.Equal(0, result[1]);
            Assert.Equal(45, result[2]);
            Assert.Equal(78, result[3]);
        }
    }
}
