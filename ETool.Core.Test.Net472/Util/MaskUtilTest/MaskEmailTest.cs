using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.MaskUtilTest
{
    public class MaskEmailTest
    {
        [Fact]
        public void ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => MaskUtil.MaskEmail(null));
            Assert.Throws<ArgumentException>(() => MaskUtil.MaskEmail("11213213@"));
            Assert.Throws<ArgumentException>(() => MaskUtil.MaskEmail("@1111"));
        }


        [Theory]
        [InlineData("123456@qq.com", '*', "12****@****om")]
        [InlineData("lierniu@forfix.com", '_', "li_____@________om")]
        [InlineData("11213213@1111", '+', "11++++++@++11")]
        public void TestMaskEmail(string email, char maskChar, string expected)
        {
            Assert.Equal(expected, MaskUtil.MaskEmail(email, maskChar));
        }
    }
}
