using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomStringFromNumberAndLetterTest
    {
        /// <summary>
        /// 长度正确
        /// </summary>
        [Fact]
        public void GetRandomStringFromNumberAndLetter_LengthCorrect()
        {
            var result = RandomUtil.GetRandomStringFromNumberAndLetter(25);
            Assert.Equal(25, result.Length);
        }

        /// <summary>
        /// 只包含字母或数字
        /// </summary>
        [Fact]
        public void GetRandomStringFromNumberAndLetter_OnlyAlphaNumeric()
        {
            var result = RandomUtil.GetRandomStringFromNumberAndLetter(200);

            Assert.All(result, c => Assert.True(CharUtil.IsDigit(c) || CharUtil.IsLetter(c)));
        }

        /// <summary>
        /// 固定 seed 可复现
        /// </summary>
        [Fact]
        public void GetRandomStringFromNumberAndLetter_Deterministic()
        {
            var rng1 = new Random(7);
            var rng2 = new Random(7);

            var s1 = RandomUtil.GetRandomStringFromNumberAndLetter(40, rng1);
            var s2 = RandomUtil.GetRandomStringFromNumberAndLetter(40, rng2);

            Assert.Equal(s1, s2);
        }
    }
}
