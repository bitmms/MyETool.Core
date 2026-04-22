using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomDigitCharTest
    {
        /// <summary>
        /// 验证返回字符始终在 '0' ~ '9' 范围内
        /// </summary>
        [Fact]
        public void GetRandomDigitChar_ResultWithinRange()
        {
            var rng = new Random(123);
            for (var i = 0; i < 10000; i++)
            {
                var value = RandomUtil.GetRandomDigitChar(rng);
                Assert.InRange(value, '0', '9');
            }
        }

        /// <summary>
        /// 验证多次调用能覆盖 0~9 所有数字（不应缺失某个数字）
        /// </summary>
        [Fact]
        public void GetRandomDigitChar_CoversAllDigits()
        {
            var seen = new bool[10];
            var rng = new Random(999);

            for (int i = 0; i < 10000; i++)
            {
                var value = RandomUtil.GetRandomDigitChar(rng);
                seen[value - '0'] = true;
            }

            for (var i = 0; i < 10; i++)
            {
                Assert.True(seen[i], $"未生成数字 {i}");
            }
        }

        /// <summary>
        /// 验证相同 seed 产生相同序列
        /// </summary>
        [Fact]
        public void GetRandomDigitChar_DeterministicWithSeed()
        {
            var rng1 = new Random(1001);
            var rng2 = new Random(1001);

            for (int i = 0; i < 20; i++)
            {
                char value1 = RandomUtil.GetRandomDigitChar(rng1);
                char value2 = RandomUtil.GetRandomDigitChar(rng2);

                Assert.Equal(value1, value2);
            }
        }

        /// <summary>
        /// 验证方法是 Random.Next(10) 的透明包装
        /// </summary>
        [Fact]
        public void GetRandomDigitChar_SequenceMatchesRandomDirectCall()
        {
            var rng1 = new Random(42);
            var rng2 = new Random(42);

            for (int i = 0; i < 20; i++)
            {
                char expected = (char)('0' + rng1.Next(10));
                char actual = RandomUtil.GetRandomDigitChar(rng2);

                Assert.Equal(expected, actual);
            }
        }

        /// <summary>
        /// 验证不传 random 时也能正常工作
        /// </summary>
        [Fact]
        public void GetRandomDigitChar_WithoutRandom_DoesNotThrow()
        {
            for (int i = 0; i < 100; i++)
            {
                char value = RandomUtil.GetRandomDigitChar();
                Assert.InRange(value, '0', '9');
            }
        }

        /// <summary>
        /// 粗略验证 0~9 出现频率大致均匀
        /// </summary>
        [Fact]
        public void GetRandomDigitChar_DistributionRoughlyUniform()
        {
            var counts = new int[10];
            var rng = new Random(777);

            const int iterations = 100_0000;

            for (var i = 0; i < iterations; i++)
            {
                var value = RandomUtil.GetRandomDigitChar(rng);
                counts[value - '0']++;
            }

            // 期望每个数字约 10000 次，允许 ±5%
            int expected = iterations / 10;
            int tolerance = iterations / 5;

            for (int i = 0; i < 10; i++)
            {
                Assert.InRange(counts[i], expected - tolerance, expected + tolerance);
            }
        }
    }
}
