using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.MaskUtilTest
{
    public class MaskPhoneNumberTest
    {
        [Fact]
        public void MaskPhoneNumber_Null_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => MaskUtil.MaskPhoneNumber(null));
        }

        [Theory]
        [InlineData("13812345678", '*', "138****5678")]
        [InlineData("13987654321", '#', "139####4321")]
        [InlineData("15000000000", 'X', "150XXXX0000")]
        [InlineData("18611112222", '0', "18600002222")]
        public void MaskPhoneNumber_ValidInput_ShouldReturnMasked(
            string input,
            char maskChar,
            string expected)
        {
            var result = MaskUtil.MaskPhoneNumber(input, maskChar);

            Assert.Equal(expected, result);
            Assert.Equal(11, result.Length); // 长度必须保持不变
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("abcdefghijk")]
        [InlineData("1381234567")] // 少一位
        [InlineData("138123456789")] // 多一位
        [InlineData("23812345678")] // 非合法号段
        public void MaskPhoneNumber_InvalidInput_ShouldThrowArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() =>
                MaskUtil.MaskPhoneNumber(input));
        }


        [Fact]
        public void MaskPhoneNumber_DefaultMaskChar_ShouldUseAsterisk()
        {
            var result = MaskUtil.MaskPhoneNumber("13812345678");

            Assert.Equal("138****5678", result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("abcdefghijk")]
        [InlineData("1381234567")] // 10位
        [InlineData("23812345678")] // 非合法号段
        public void MaskPhoneNumber_InvalidInput_ShouldThrow(string input)
        {
            Assert.Throws<ArgumentException>(() => MaskUtil.MaskPhoneNumber(input));
        }

        [Theory]
        [InlineData("13812345678", '*', "138****5678")]
        [InlineData("13987654321", '#', "139####4321")]
        [InlineData("15000000000", 'X', "150XXXX0000")]
        public void MaskPhoneNumber_ValidInput_ShouldMaskCorrectly(string input, char maskChar, string expected)
        {
            var result = MaskUtil.MaskPhoneNumber(input, maskChar);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MaskPhoneNumber_MultipleCalls_ShouldBeConsistent()
        {
            var input = "13812345678";
            var result1 = MaskUtil.MaskPhoneNumber(input);
            var result2 = MaskUtil.MaskPhoneNumber(input);
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void MaskPhoneNumber_ShouldNotModifyOriginalString()
        {
            var input = "13812345678";
            var result = MaskUtil.MaskPhoneNumber(input);
            Assert.Equal("13812345678", input); // 原字符串不变
            Assert.NotSame(input, result); // 返回新实例
        }

        [Fact]
        public void MaskPhoneNumber_ShouldMaskExactlyFourCharacters()
        {
            var result = MaskUtil.MaskPhoneNumber("13812345678");
            Assert.Equal('*', result[3]);
            Assert.Equal('*', result[4]);
            Assert.Equal('*', result[5]);
            Assert.Equal('*', result[6]);
        }
    }
}
