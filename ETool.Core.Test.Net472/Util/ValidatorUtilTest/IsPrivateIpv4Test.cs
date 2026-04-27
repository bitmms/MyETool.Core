using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsPrivateIpv4Test
    {
        [Fact]
        public void Is_PrivateIpv4_Return_True()
        {
            Assert.True(ValidatorUtil.IsPrivateIpv4("192.168.1.1"));
            Assert.True(ValidatorUtil.IsPrivateIpv4("10.2.6.5"));
            Assert.True(ValidatorUtil.IsPrivateIpv4("172.16.5.99"));
            Assert.True(ValidatorUtil.IsPrivateIpv4("172.31.5.99"));
        }
        
        [Fact]
        public void Is_PrivateIpv4_Return_False()
        {
            Assert.False(ValidatorUtil.IsPrivateIpv4("0.0.0.0"));
            Assert.False(ValidatorUtil.IsPrivateIpv4(""));
            Assert.False(ValidatorUtil.IsPrivateIpv4(null));
            Assert.False(ValidatorUtil.IsPrivateIpv4("4.4.4.4"));
        }
    }
}
