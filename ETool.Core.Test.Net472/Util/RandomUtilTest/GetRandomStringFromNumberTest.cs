using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomStringFromNumberTest
    {
        /// <summary>
        /// 返回字符串长度应正确
        /// </summary>
        [Fact]
        public void GetRandomStringFromNumber_LengthCorrect()
        {
            var result = RandomUtil.GetRandomStringFromNumber(15);
            Assert.Equal(15, result.Length);
        }

        /// <summary>
        /// 只包含数字
        /// </summary>
        [Fact]
        public void GetRandomStringFromNumber_OnlyDigits()
        {
            var result = RandomUtil.GetRandomStringFromNumber(100);
            Assert.All(result, c => Assert.True(CharUtil.IsDigit(c)));
        }

        /// <summary>
        /// 固定 seed 可复现
        /// </summary>
        [Fact]
        public void GetRandomStringFromNumber_Deterministic()
        {
            var rng1 = new Random(42);
            var rng2 = new Random(42);

            var s1 = RandomUtil.GetRandomStringFromNumber(20, rng1);
            var s2 = RandomUtil.GetRandomStringFromNumber(20, rng2);

            Assert.Equal(s1, s2);
        }
    }
}
