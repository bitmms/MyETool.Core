using ETool.Core.Core;
using Xunit;

namespace ETool.Core.Test.Net472.Core.CharUtilTest
{
    public class GetByteByBigEndianUnicodeTest
    {
        [Theory]
        [InlineData('a', 0, 97)]
        [InlineData('中', 78, 45)]
        public void Test(char input, char expectedHigh, char expectedLow)
        {
            var (high, low) = CharUtil.GetByteByBigEndianUnicode(input);
            Assert.True(high == expectedHigh);
            Assert.True(low == expectedLow);
        }
    }
}
