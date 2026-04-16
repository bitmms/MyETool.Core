using System.Diagnostics;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.ValidatorUtilTest
{
    public class IsValidIpv4Test
    {
        /// <summary>
        /// 测试合法的标准 IPv4 地址
        /// </summary>
        [Theory]
        [InlineData("0.0.0.0", true, "最小IPv4地址应验证成功")]
        [InlineData("1.1.1.1", true, "标准IPv4地址应验证成功")]
        [InlineData("127.0.0.1", true, "本地回环地址应验证成功")]
        [InlineData("192.168.1.1", true, "标准私网地址应验证成功")]
        [InlineData("192.168.0.1", true, "标准私网地址应验证成功")]
        [InlineData("10.0.0.1", true, "标准私网地址应验证成功")]
        [InlineData("172.16.0.1", true, "标准私网地址应验证成功")]
        [InlineData("255.255.255.255", true, "广播地址应验证成功")]
        [InlineData("8.8.8.8", true, "公网DNS地址应验证成功")]
        [InlineData("1.2.3.4", true, "任意有效地址应验证成功")]
        [InlineData("100.100.100.100", true, "任意有效地址应验证成功")]
        [InlineData("200.200.200.200", true, "任意有效地址应验证成功")]
        public void IsValidIpv4_ValidIpv4Addresses_ReturnsTrue(string input, bool expectedResult, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expectedResult, errorMessage);
        }

        /// <summary>
        /// 全角半角字符测试
        /// </summary>
        [Theory]
        [InlineData("192.168.１2.1", false, "包含全角字符『１』应验证失败")]
        [InlineData("19２.168.12.1", false, "包含全角字符『２』应验证失败")]
        [InlineData("１92.168.12.1", false, "包含全角字符『１』应验证失败")]
        [InlineData("192.１68.12.1", false, "包含全角字符『１』应验证失败")]
        [InlineData("192.168.１２.1", false, "包含多个全角字符应验证失败")]
        [InlineData("19２.168.１.1", false, "多个位置的全角字符应验证失败")]
        [InlineData("１.１.１.１", false, "全角点和全角数字应验证失败")]
        [InlineData("192.168.12.１", false, "末位全角数字应验证失败")]
        public void IsValidIpv4_FullWidthCharacters_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }
        
        /// <summary>
        /// 测试不合法的输入 - 空值和空白
        /// </summary>
        [Theory]
        [InlineData(null, false, "null 应验证失败")]
        [InlineData("", false, "空字符串应验证失败")]
        [InlineData("   ", false, "仅包含空格的字符串应验证失败")]
        [InlineData("\t", false, "制表符应验证失败")]
        [InlineData("\r\n", false, "换行符应验证失败")]
        [InlineData(" ", false, "单个空格应验证失败")]
        public void IsValidIpv4_NullOrWhitespace_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 长度不符（过短）
        /// </summary>
        [Theory]
        [InlineData("1", false, "1位应验证失败")]
        [InlineData("1.1", false, "两段应验证失败")]
        [InlineData("1.1.1", false, "三段应验证失败")]
        [InlineData("1.1.1.", false, "不完整的地址应验证失败")]
        [InlineData(".1.1.1", false, "以点开头应验证失败")]
        public void IsValidIpv4_TooShort_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 长度不符（过长）
        /// </summary>
        [Theory]
        [InlineData("1234567890123456", false, "16位应验证失败")]
        [InlineData("256.256.256.256", false, "每个八位组超过3位应验证失败")]
        [InlineData("1234.5678.9012.3456", false, "每个八位组超过3位应验证失败")]
        [InlineData("999.999.999.999", false, "超出最大长度应验证失败")]
        public void IsValidIpv4_TooLong_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 前导零
        /// </summary>
        [Theory]
        [InlineData("01.1.1.1", false, "首段前导零应验证失败")]
        [InlineData("1.01.1.1", false, "第二段前导零应验证失败")]
        [InlineData("1.1.01.1", false, "第三段前导零应验证失败")]
        [InlineData("1.1.1.01", false, "第四段前导零应验证失败")]
        [InlineData("001.1.1.1", false, "多个前导零应验证失败")]
        [InlineData("192.0168.1.1", false, "中间段前导零应验证失败")]
        [InlineData("010.010.010.010", false, "所有段都有前导零应验证失败")]
        public void IsValidIpv4_LeadingZeros_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 超出范围（>255）
        /// </summary>
        [Theory]
        [InlineData("256.1.1.1", false, "第一段超过255应验证失败")]
        [InlineData("1.256.1.1", false, "第二段超过255应验证失败")]
        [InlineData("1.1.256.1", false, "第三段超过255应验证失败")]
        [InlineData("1.1.1.256", false, "第四段超过255应验证失败")]
        [InlineData("300.300.300.300", false, "所有段都超过255应验证失败")]
        [InlineData("255.255.255.256", false, "最后一段超过255应验证失败")]
        [InlineData("999.999.999.999", false, "远超范围应验证失败")]
        [InlineData("192.168.1.1000", false, "最后一段远超范围应验证失败")]
        public void IsValidIpv4_OutOfRange_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 包含非数字和点的字符
        /// </summary>
        [Theory]
        [InlineData("192.168.1.1a", false, "包含字母应验证失败")]
        [InlineData("a192.168.1.1", false, "以字母开头应验证失败")]
        [InlineData("192.168.1.1.", false, "末尾多余点应验证失败")]
        [InlineData("192.168..1.1", false, "连续的点应验证失败")]
        [InlineData("192 168.1.1", false, "包含空格应验证失败")]
        [InlineData("192-168.1.1", false, "包含连字符应验证失败")]
        [InlineData("192,168,1,1", false, "包含逗号应验证失败")]
        [InlineData("192/168/1/1", false, "包含斜杠应验证失败")]
        [InlineData("192:168:1:1", false, "包含冒号应验证失败")]
        [InlineData("192.168.1.1 ", false, "末尾带空格应验证失败")]
        [InlineData(" 192.168.1.1", false, "开头带空格应验证失败")]
        [InlineData("192.168.①.1", false, "包含全角数字应验证失败")]
        [InlineData("+192.168.1.1", false, "包含正号应验证失败")]
        [InlineData("-192.168.1.1", false, "包含负号应验证失败")]
        public void IsValidIpv4_InvalidCharacters_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 段数不对
        /// </summary>
        [Theory]
        [InlineData("192.168.1", false, "三段应验证失败")]
        [InlineData("192.168.1.1.1", false, "五段应验证失败")]
        [InlineData("192.168.1.1.1.1", false, "六段应验证失败")]
        public void IsValidIpv4_WrongSegmentCount_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 测试不合法的输入 - 空段
        /// </summary>
        [Theory]
        [InlineData(".192.168.1.1", false, "首段为空应验证失败")]
        [InlineData("192..168.1.1", false, "第二段为空应验证失败")]
        [InlineData("192.168..1.1", false, "第三段为空应验证失败")]
        [InlineData("192.168.1.1.", false, "末段为空应验证失败")]
        [InlineData("192.168.1.", false, "末段为空应验证失败")]
        public void IsValidIpv4_EmptySegments_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(result == expected, errorMessage);
        }

        /// <summary>
        /// 边界值测试 - 最小有效地址
        /// </summary>
        [Fact]
        public void IsValidIpv4_MinimumAddress_ReturnsTrue()
        {
            var result = ValidatorUtil.IsValidIpv4("0.0.0.0");
            Assert.True(result, "最小地址 0.0.0.0 应验证成功");
        }

        /// <summary>
        /// 边界值测试 - 最大有效地址
        /// </summary>
        [Fact]
        public void IsValidIpv4_MaximumAddress_ReturnsTrue()
        {
            var result = ValidatorUtil.IsValidIpv4("255.255.255.255");
            Assert.True(result, "最大地址 255.255.255.255 应验证成功");
        }

        /// <summary>
        /// 边界值测试 - 各段最大值（不超过255）
        /// </summary>
        [Theory]
        [InlineData("255.0.0.0", true)]
        [InlineData("0.255.0.0", true)]
        [InlineData("0.0.255.0", true)]
        [InlineData("0.0.0.255", true)]
        public void IsValidIpv4_SegmentMaxValues_ReturnsTrue(string input, bool expected)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 边界值测试 - 各段最大值加1（超过255）
        /// </summary>
        [Theory]
        [InlineData("256.0.0.0", false)]
        [InlineData("0.256.0.0", false)]
        [InlineData("0.0.256.0", false)]
        [InlineData("0.0.0.256", false)]
        public void IsValidIpv4_SegmentExceedsMax_ReturnsFalse(string input, bool expected)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 边界值测试 - 段长度为1、2、3
        /// </summary>
        [Theory]
        [InlineData("1.1.1.1", true, "1位数字应验证成功")]
        [InlineData("10.10.10.10", true, "2位数字应验证成功")]
        [InlineData("100.100.100.100", true, "3位数字应验证成功")]
        [InlineData("255.255.255.255", true, "最大3位数字应验证成功")]
        [InlineData("1000.1.1.1", false, "4位数字应验证失败")]
        public void IsValidIpv4_SegmentLength_ReturnsExpected(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(expected == result, errorMessage);
        }

        /// <summary>
        /// 综合真实场景测试
        /// </summary>
        [Theory]
        [InlineData("127.0.0.1", true, "本地回环地址")]
        [InlineData("192.168.1.1", true, "常见路由器地址")]
        [InlineData("192.168.0.1", true, "常见路由器地址")]
        [InlineData("10.0.0.1", true, "私网地址")]
        [InlineData("172.16.0.1", true, "私网地址")]
        [InlineData("8.8.8.8", true, "Google DNS")]
        [InlineData("1.1.1.1", true, "Cloudflare DNS")]
        [InlineData("255.255.255.255", true, "广播地址")]
        [InlineData("224.0.0.1", true, "组播地址")]
        [InlineData("256.1.1.1", false, "超出范围")]
        [InlineData("192.168.1", false, "缺少段")]
        [InlineData("192.168.1.1.1", false, "过多段")]
        [InlineData("192.168.1.0a", false, "包含非数字")]
        public void IsValidIpv4_RealWorldScenarios_ReturnsExpected(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(expected == result, errorMessage);
        }

        /// <summary>
        /// 特殊情况 - 单个数字段
        /// </summary>
        [Theory]
        [InlineData("9.9.9.9", true)]
        [InlineData("1.2.3.4", true)]
        [InlineData("5.6.7.8", true)]
        public void IsValidIpv4_SingleDigitSegments_ReturnsTrue(string input, bool expected)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// 特殊情况 - 特殊地址
        /// </summary>
        [Theory]
        [InlineData("0.0.0.0", true, "任意地址")]
        [InlineData("255.255.255.255", true, "广播地址")]
        [InlineData("127.0.0.1", true, "回环地址")]
        [InlineData("169.254.0.0", true, "链接本地地址")]
        public void IsValidIpv4_SpecialAddresses_ReturnsTrue(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(expected == result, errorMessage);
        }

        /// <summary>
        /// 综合边界测试
        /// </summary>
        [Theory]
        [InlineData("0.0.0.0", true, "最小")]
        [InlineData("255.55.255.255", true, "最大")]
        [InlineData("128.128.128.128", true, "中间值")]
        [InlineData("256.0.0.0", false, "超出上限")]
        [InlineData("-1.0.0.0", false, "负数")]
        public void IsValidIpv4_BoundaryValues_ReturnsExpected(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(expected == result, errorMessage);
        }

        /// <summary>
        /// 测试所有可能的分隔符错误
        /// </summary>
        [Theory]
        [InlineData("192,168,1,1", false, "使用逗号分隔")]
        [InlineData("192-168-1-1", false, "使用连字符分隔")]
        [InlineData("192/168/1/1", false, "使用斜杠分隔")]
        [InlineData("192:168:1:1", false, "使用冒号分隔")]
        [InlineData("192 168 1 1", false, "使用空格分隔")]
        [InlineData("192.168.1.1.", false, "末尾多余点")]
        [InlineData(".192.168.1.1", false, "开头多余点")]
        public void IsValidIpv4_WrongSeparators_ReturnsFalse(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(expected == result, errorMessage);
        }

        /// <summary>
        /// 测试长度边界
        /// </summary>
        [Theory]
        [InlineData("0.0.0.0", true, "7个字符（最短）")]
        [InlineData("1.1.1.1", true, "7个字符")]
        [InlineData("255.255.255.255", true, "15个字符（最长）")]
        [InlineData("1.1.1", false, "6个字符（过短）")]
        [InlineData("256.256.256.256", false, "15个字符（但值超范围）")]
        public void IsValidIpv4_LengthBoundary_ReturnsExpected(string input, bool expected, string errorMessage)
        {
            var result = ValidatorUtil.IsValidIpv4(input);
            Assert.True(expected == result, errorMessage);
        }

        /// <summary>
        /// 性能压力测试 - 有效IPv4地址
        /// </summary>
        [Fact]
        public void IsValidIpv4_Performance_LargeVolume_ValidIpv4()
        {
            const int expectedMillisecond = 1 * 1000;
            const int count = 100_0000;
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < count; i++)
            {
                ValidatorUtil.IsValidIpv4("192.168.1.100");
            }

            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds < expectedMillisecond,
                $"验证 {count} 个有效IPv4地址耗时 {stopwatch.ElapsedMilliseconds}ms，超出预期的 {expectedMillisecond}ms");
        }

        /// <summary>
        /// 性能压力测试 - 无效IPv4地址（早期失败）
        /// </summary>
        [Fact]
        public void IsValidIpv4_Performance_LargeVolume_InvalidIpv4()
        {
            const int expectedMillisecond = 1 * 1000;
            const int count = 100_0000;
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < count; i++)
            {
                ValidatorUtil.IsValidIpv4("256.256.256.256");
            }

            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds < expectedMillisecond,
                $"验证 {count} 个无效IPv4地址耗时 {stopwatch.ElapsedMilliseconds}ms，超出预期的 {expectedMillisecond}ms");
        }

        /// <summary>
        /// 性能压力测试 - 边界情况（数值接近255的地址）
        /// </summary>
        [Fact]
        public void IsValidIpv4_Performance_LargeVolume_BoundaryValues()
        {
            const int expectedMillisecond = 1 * 1000;
            const int count = 100_0000;
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < count; i++)
            {
                ValidatorUtil.IsValidIpv4("255.255.255.255");
            }

            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds < expectedMillisecond,
                $"验证 {count} 个边界值地址耗时 {stopwatch.ElapsedMilliseconds}ms，超出预期的 {expectedMillisecond}ms");
        }
    }
}
