using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class Equals
    {
        // 区分大小写
        [Theory]
        [InlineData("abcDDdfd", "abcDDdfd")] // 相同内容
        [InlineData("", "")] // 空字符串
        [InlineData(null, null)] // 两个 null
        [InlineData("Hello", "Hello")] // 普通字符串
        [InlineData("123", "123")] // 数字字符串
        [InlineData("!@#$", "!@#$")] // 符号字符串
        public void Equals_CaseSensitive_SameContent_ReturnsTrue(string s1, string s2)
        {
            Assert.True(StrUtil.Equals(s1, s2));
        }

        [Theory]
        [InlineData("abc", "ABC")] // 大小写不同
        [InlineData("Hello", "hello")] // 首字母大小写不同
        [InlineData("abc", "abd")] // 字符不同
        [InlineData("abc", "abcd")] // 长度不同
        [InlineData("abc", null)] // 一个为 null
        [InlineData(null, "abc")] // 另一个为 null
        [InlineData("", " ")] // 空字符串与空格（内容不同）
        [InlineData("Hello", "Hello ")] // 尾部空格
        public void Equals_CaseSensitive_DifferentContent_ReturnsFalse(string s1, string s2)
        {
            Assert.False(StrUtil.Equals(s1, s2));
        }

        // 忽略大小写
        [Theory]
        [InlineData("abc", "ABC")] // 全小写 vs 全大写
        [InlineData("Hello", "hELLo")] // 混合大小写
        [InlineData("AbC123", "aBc123")] // 带数字
        [InlineData("!@#Aa", "!@#aA")] // 带符号
        [InlineData("", "")] // 空字符串
        [InlineData(null, null)] // 两个 null
        [InlineData("same", "same")] // 完全相同
        public void Equals_IgnoreCase_SameIgnoringCase_ReturnsTrue(string s1, string s2)
        {
            Assert.True(StrUtil.Equals(s1, s2, ignoreCase: true));
        }

        [Theory]
        [InlineData("abc", "abd")] // 不同字符
        [InlineData("abc", "abcd")] // 长度不同
        [InlineData("abc", null)] // 一个为 null
        [InlineData(null, "abc")] // 另一个为 null
        [InlineData("Hello", "World")] // 完全不同
        [InlineData("abc", "ABC ")] // 尾部空格导致长度不同
        [InlineData("ABC", "aBcD")] // 长度不同且内容不同
        public void Equals_IgnoreCase_DifferentContent_ReturnsFalse(string s1, string s2)
        {
            Assert.False(StrUtil.Equals(s1, s2, ignoreCase: true));
        }

        // 特殊场景：非字母字符不受大小写影响，但验证逐字符比较正确性
        [Theory]
        [InlineData("123", "123")] // 数字相同
        [InlineData("123", "124")] // 数字不同
        [InlineData("!@#", "!@#")] // 符号相同
        [InlineData("!@#", "!@$")] // 符号不同
        [InlineData("中文", "中文")] // Unicode 字符相同
        [InlineData("中文", "汉字")] // Unicode 字符不同
        public void Equals_NonLetterCharacters_BehaveAsExpected(string s1, string s2)
        {
            Assert.Equal(s1 == s2, StrUtil.Equals(s1, s2, ignoreCase: false));
            Assert.Equal(s1 == s2, StrUtil.Equals(s1, s2, ignoreCase: true));
        }
    }
}
