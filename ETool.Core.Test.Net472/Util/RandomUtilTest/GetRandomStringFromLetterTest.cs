using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomStringFromLetterTest
    {
        /// <summary>
        /// 长度正确
        /// </summary>
        [Fact]
        public void GetRandomStringFromLetter_LengthCorrect()
        {
            var result = RandomUtil.GetRandomStringFromLetter(20);
            Assert.Equal(20, result.Length);
        }

        /// <summary>
        /// 只包含字母
        /// </summary>
        [Fact]
        public void GetRandomStringFromLetter_OnlyLetters()
        {
            var result = RandomUtil.GetRandomStringFromLetter(200);
            Assert.All(result, c => Assert.True(ValidatorUtil.IsLetter(c)));
        }

        /// <summary>
        /// 固定 seed 可复现
        /// </summary>
        [Fact]
        public void GetRandomStringFromLetter_Deterministic()
        {
            var rng1 = new Random(99);
            var rng2 = new Random(99);

            var s1 = RandomUtil.GetRandomStringFromLetter(30, rng1);
            var s2 = RandomUtil.GetRandomStringFromLetter(30, rng2);

            Assert.Equal(s1, s2);
        }
    }
}
