using ETool.Core.Util;
using ETool.Core.Util.Enum;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PasswordUtilTest
{
    public class PasswordUtilTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("Abc123")]
        [InlineData("Abc1 23")]
        [InlineData("Aa1!")]
        [InlineData("Abc1\t23")]
        [InlineData("Abc1密码23")]
        [InlineData("Abc1\\23")]
        [InlineData("bcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890")]
        [InlineData("aaaaaaaa")]
        [InlineData("AAAAAAAA")]
        [InlineData("12345678")]
        [InlineData("!@#$%^&*")]
        public void CheckPasswordStrength_ReturnsInvalid(string password)
        {
            Assert.Equal(PasswordStrength.Invalid, PasswordUtil.CheckPasswordStrength(password));
        }

        [Theory]
        [InlineData("abcd1234")]
        [InlineData("ABCD1234")]
        [InlineData("abcd!@#$")]
        [InlineData("ABCD!@#$")]
        [InlineData("1234!@#$")]
        public void CheckPasswordStrength_ReturnsWeak(string password)
        {
            Assert.Equal(PasswordStrength.Weak, PasswordUtil.CheckPasswordStrength(password));
        }

        [Theory]
        [InlineData("abcdABCD1234")]
        [InlineData("abcd1234!@#")]
        [InlineData("ABCD1234!@#")]
        [InlineData("abcdABCD!@#")]
        [InlineData("10086...eerw")]
        [InlineData("Aa1bbbbb")]
        [InlineData("Aa!bbbbb")]
        public void CheckPasswordStrength_ReturnsMedium(string password)
        {
            Assert.Equal(PasswordStrength.Medium, PasswordUtil.CheckPasswordStrength(password));
        }

        [Theory]
        [InlineData("Abcd1234!")]
        [InlineData("!A1b2C3d4E5f6G7h8I9")]
        [InlineData("P@ssw0rd")]
        [InlineData("aB1!cD2@eF3#gH4$")]
        [InlineData("Aa1!bbbb")]
        [InlineData("Abc123!@#$")]
        [InlineData("Aa1!@#$%^&*()_-+=[]{}.,?:;~")]
        public void CheckPasswordStrength_ReturnsStrong(string password)
        {
            Assert.Equal(PasswordStrength.Strong, PasswordUtil.CheckPasswordStrength(password));
        }
    }
}
