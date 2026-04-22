using System;
using System.Linq;
using Xunit;
using ETool.Core.Util;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomStringFromCharsetTest
    {
        /// <summary>
        /// length 小于 0 应抛 ArgumentOutOfRangeException
        /// </summary>
        [Fact]
        public void GetRandomStringFromCharset_LengthLessThanZero_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RandomUtil.GetRandomStringFromCharset(-1, "ABC"));
        }

        /// <summary>
        /// charset 为 null 应抛 ArgumentNullException
        /// </summary>
        [Fact]
        public void GetRandomStringFromCharset_CharsetNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => RandomUtil.GetRandomStringFromCharset(5, null));
        }

        /// <summary>
        /// charset 为空应抛 ArgumentException
        /// </summary>
        [Fact]
        public void GetRandomStringFromCharset_CharsetEmpty_Throws()
        {
            Assert.Throws<ArgumentException>(() => RandomUtil.GetRandomStringFromCharset(5, ""));
        }

        /// <summary>
        /// 返回字符串长度应等于指定 length
        /// </summary>
        [Fact]
        public void GetRandomStringFromCharset_ReturnsCorrectLength()
        {
            var result = RandomUtil.GetRandomStringFromCharset(10, "ABC");
            Assert.Equal(10, result.Length);
        }

        /// <summary>
        /// 返回的字符必须来自指定字符集
        /// </summary>
        [Fact]
        public void GetRandomStringFromCharset_CharactersWithinCharset()
        {
            const string charset = "XYZ";
            var result = RandomUtil.GetRandomStringFromCharset(100, charset);
            Assert.All(result, c => Assert.Contains(c, charset));
        }

        /// <summary>
        /// 固定 seed 时应可复现
        /// </summary>
        [Fact]
        public void GetRandomStringFromCharset_DeterministicWithSeed()
        {
            var rng1 = new Random(1234);
            var rng2 = new Random(1234);

            var s1 = RandomUtil.GetRandomStringFromCharset(20, "ABCDE", rng1);
            var s2 = RandomUtil.GetRandomStringFromCharset(20, "ABCDE", rng2);

            Assert.Equal(s1, s2);
        }
    }
}
