using ETool.Core.Core;
using Xunit;

namespace ETool.Core.Test.Net472.Core.CharUtilTest
{
    public class GetByteByLittleEndianUnicodeTest
    {
        [Theory]
        [InlineData('a', 97, 0)]
        [InlineData('中', 45, 78)]
        public void Test(char input, char expectedLow, char expectedHigh)
        {
            var (low, high) = CharUtil.GetByteByLittleEndianUnicode(input);
            Assert.True(low == expectedLow);
            Assert.True(high == expectedHigh);
        }
    }
}
