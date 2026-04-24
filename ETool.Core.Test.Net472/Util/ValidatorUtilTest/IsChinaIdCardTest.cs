using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsChinaIdCardTest
    {
        /// <summary>
        /// 测试：合法的15位身份证 → 返回 true
        /// </summary>
        [Theory(DisplayName = "合法15位身份证返回true")]
        [InlineData("110101900307123")] // 真实15位格式
        public void Valid_15IdCard_Return_True(string idCard)
        {
            Assert.True(ValidatorUtil.IsChinaIdCard(idCard));
        }

        /// <summary>
        /// 测试：合法的18位身份证 → 返回 true
        /// </summary>
        [Theory(DisplayName = "合法18位身份证返回true")]
        [InlineData("622421198604042314")]
        [InlineData("622421199104296419")]
        [InlineData("620123196611303215")]
        [InlineData("13020219620814854X")]
        [InlineData("21091119900629706X")]
        [InlineData("14118219900522789X")]
        public void Valid_18IdCard_Return_True(string idCard)
        {
            Assert.True(ValidatorUtil.IsChinaIdCard(idCard));
        }

        [Theory(DisplayName = "合法18位身份证，但是大小写异常，返回false")]
        [InlineData("13020219620814854x")]
        [InlineData("21091119900629706x")]
        [InlineData("14118219900522789x")]
        public void Valid_18IdCard_Return_Fasle(string idCard)
        {
            Assert.False(ValidatorUtil.IsChinaIdCard(idCard, false));
        }


        /// <summary>
        /// 测试：非法15位身份证 → 返回 false
        /// </summary>
        [Theory(DisplayName = "非法15位身份证返回false")]
        [InlineData("110101900307")] // 长度不足15
        [InlineData("11010190030712x")] // 包含字母
        [InlineData("1101019003071234")] // 长度16位
        public void Invalid_15IdCard_Return_False(string idCard)
        {
            Assert.False(ValidatorUtil.IsChinaIdCard(idCard));
        }

        /// <summary>
        /// 测试：非法18位身份证 → 返回 false
        /// </summary>
        [Theory(DisplayName = "非法18位身份证返回false")]
        [InlineData("11010119900307123")] // 长度17位
        [InlineData("1101011990030712333")] // 长度19位
        [InlineData("1101011990030712ab")] // 包含小写字母
        [InlineData("11010119900307123X")] // 校验位错误
        public void Invalid_18IdCard_Return_False(string idCard)
        {
            Assert.False(ValidatorUtil.IsChinaIdCard(idCard));
        }
    }
}
