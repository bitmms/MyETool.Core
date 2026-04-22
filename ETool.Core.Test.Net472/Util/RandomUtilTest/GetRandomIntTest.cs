using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomIntTest
    {
        // 异常测试
        [Fact]
        public void GetRandomInt_MinGreaterThanMax_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RandomUtil.GetRandomInt(10, 5));
        }

        // 边界测试：min == max
        [Fact]
        public void GetRandomInt_MinEqualsMax_ReturnsMin()
        {
            var result = RandomUtil.GetRandomInt(5, 5);
            Assert.Equal(5, result);
        }

        // 区间正确性测试
        [Theory]
        [InlineData(1, 10)]
        [InlineData(1, 100)]
        [InlineData(1, 1000)]
        [InlineData(1, 10000)]
        [InlineData(1, 100000)]
        [InlineData(1, 1000000)]
        public void GetRandomInt_ResultWithinRange(int min, int max)
        {
            var random = new Random(123);
            for (var i = 0; i < 1000; i++)
            {
                var value = RandomUtil.GetRandomInt(min, max, random);
                Assert.InRange(value, min, max - 1);
            }
        }

        // 可预测性测试
        [Fact]
        public void GetRandomInt_DeterministicWithSeed()
        {
            var rng1 = new Random(1001);
            var rng2 = new Random(1001);

            for (var i = 0; i < 20; i++)
            {
                var value1 = RandomUtil.GetRandomInt(0, 100, rng1);
                var value2 = RandomUtil.GetRandomInt(0, 100, rng2);
                Assert.Equal(value1, value2);
            }
        }

        // 验证 RandomUtil.GetRandomInt() 是一个确定性、状态透明、行为正确的 Random.Next 包装器，且在传入固定 seed 的 Random 时，输出序列与直接调用 Random.Next 完全一致
        [Fact]
        public void GetRandomInt_SequenceMatchesRandomDirectCall()
        {
            var rng1 = new Random(41112);
            var rng2 = new Random(41112);

            for (var i = 0; i < 10; i++)
            {
                var expected = rng1.Next(0, 50);
                var actual = RandomUtil.GetRandomInt(0, 50, rng2);
                Assert.Equal(expected, actual);
            }
        }

        // 验证“随机性大体均匀”，防止明显偏差
        [Fact]
        public void GetRandomInt_DistributionRoughlyUniform()
        {
            const int min = 6;
            const int max = 10;
            const int iterations = 100_0000;

            const int rangeSize = max - min + 1;
            var counts = new int[rangeSize];

            for (var i = 0; i < iterations; i++)
            {
                var value = RandomUtil.GetRandomInt(min, max + 1);
                counts[value - min]++;
            }

            var expected = iterations / rangeSize;
            var tolerance = (int)(expected * 0.05); // 允许 ±5%

            foreach (var count in counts)
            {
                Assert.InRange(count, expected - tolerance, expected + tolerance);
            }
        }
    }
}
