using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.MaskUtilTest
{
    public class MaskIdCardTest
    {
        [Theory(DisplayName = "18位身份证脱敏成功")]
        [InlineData("11010519491231002X", "110************02X")] // 带X
        [InlineData("110101199003071233", "110************233")] // 纯数字
        public void Valid_18Digit_ID_Should_Mask_Correctly(string idCard, string expected)
        {
            var result = MaskUtil.MaskIdCard(idCard);
            Assert.Equal(expected, result);
        }

        [Theory(DisplayName = "15位身份证脱敏成功（如果业务支持）")]
        [InlineData("110101900307123", "110*********123")]
        public void Valid_15Digit_ID_Should_Mask_Correctly(string idCard, string expected)
        {
            var result = MaskUtil.MaskIdCard(idCard);
            Assert.Equal(expected, result);
        }

        [Theory(DisplayName = "自定义掩码字符")]
        [InlineData("110101199003071233", '#', "110############233")]
        [InlineData("110101199003071233", 'x', "110xxxxxxxxxxxx233")]
        public void Custom_Mask_Char_Should_Work(string idCard, char maskChar, string expected)
        {
            var result = MaskUtil.MaskIdCard(idCard, maskChar);
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "null值应抛出ArgumentNullException")]
        public void Null_IdCard_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => MaskUtil.MaskIdCard(null));
        }

        [Theory(DisplayName = "无效身份证格式应抛出ArgumentException")]
        [InlineData("123456789012345678")] // 错误校验位
        [InlineData("110101199013071233")] // 无效日期
        [InlineData("12345")] // 长度错误
        [InlineData("")] // 空字符串
        [InlineData("   ")] // 空白
        public void Invalid_ID_Should_Throw_ArgumentException(string invalidId)
        {
            Assert.Throws<ArgumentException>(() => MaskUtil.MaskIdCard(invalidId));
        }

        [Fact(DisplayName = "脱敏后保留前3位和后3位（18位）")]
        public void Mask_Should_Preserve_First3_And_Last3_For_18Digit()
        {
            var id = "110101199003071233";
            var result = MaskUtil.MaskIdCard(id);

            Assert.StartsWith("110", result);
            Assert.EndsWith("233", result);
            Assert.Equal(18, result.Length);
        }

        [Fact(DisplayName = "脱敏后保留前3位和后3位（15位）")]
        public void Mask_Should_Preserve_First3_And_Last3_For_15Digit()
        {
            var id = "110101900307123";
            var result = MaskUtil.MaskIdCard(id);

            Assert.StartsWith("110", result);
            Assert.EndsWith("123", result);
            Assert.Equal(15, result.Length);
        }

        [Fact(DisplayName = "掩码字符位置验证（18位）")]
        public void Mask_Position_Should_Be_Correct_For_18Digit()
        {
            var id = "11010519491231002X";
            var result = MaskUtil.MaskIdCard(id, '*');

            // 第4-15位应为掩码（索引3-14）
            Assert.Equal("110", result.Substring(0, 3));
            Assert.Equal("02X", result.Substring(15, 3));
            Assert.Equal(new string('*', 12), result.Substring(3, 12));
        }
    }
}
