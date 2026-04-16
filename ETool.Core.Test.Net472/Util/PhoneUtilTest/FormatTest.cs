using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.PhoneUtilTest
{
    public class FormatTest
    {
        /// <summary>
        /// 测试合法手机号
        /// </summary>
        [Theory]
        [InlineData("13812345678", "138", "1234", "5678")]
        [InlineData("19999999999", "199", "9999", "9999")]
        [InlineData("13711112525", "137", "1111", "2525")]
        public void Format_ValidPhone_SplitSuccess(string phone, string prefix, string middle, string suffix)
        {
            var result = PhoneUtil.Format(phone);

            Assert.Equal(prefix, result.Prefix);
            Assert.Equal(middle, result.Middle);
            Assert.Equal(suffix, result.Suffix);
        }
        
        /// <summary>
        /// 测试 null → 必须抛出 ArgumentNullException
        /// </summary>
        [Fact]
        public void Format_Null_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => PhoneUtil.Format(null));
        }
        
        /// <summary>
        /// 测试非法手机号 → 抛出 ArgumentException
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123456")]
        [InlineData("123456789012")]
        [InlineData("138abc12345")]
        [InlineData("１3812345678")]
        [InlineData("138-1234-5678")]
        [InlineData("12711112222")]
        public void Format_InvalidPhone_ThrowArgumentException(string phone)
        {
            Assert.Throws<ArgumentException>(() => PhoneUtil.Format(phone));
        }
    }
}
