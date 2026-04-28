using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsDateStringTest
    {
        [Theory]
        [InlineData("2017-12-56", "yyyy-MM-dd")]
        [InlineData("2017-12-56", "yyyy-MM-dd", null)]
        [InlineData("2017-12-   56", "yyyy-MM-   dd", null)]
        [InlineData(null, "", "")]
        [InlineData("", null)]
        public void Return_False(string s, params string[] formats)
        {
            Assert.False(ValidatorUtil.IsDateTimeString(s, formats));
        }

        [Theory]
        [InlineData("2017-12-16", "yyyy-MM-dd")]
        [InlineData("2017-12-16", "yyyy-MM-dd", null)]
        [InlineData("2017-12-   16", "yyyy-MM-   dd", null)]
        public void Return_True(string s, params string[] formats)
        {
            Assert.True(ValidatorUtil.IsDateTimeString(s, formats));
        }
    }
}
