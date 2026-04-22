using System;
using Xunit;
using ETool.Core.Util;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomDoubleTest
    {
        /// <summary>
        /// min > max 应抛异常
        /// </summary>
        [Fact]
        public void GetRandomDouble_MinGreaterThanMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RandomUtil.GetRandomDouble(10, 5));
        }

        /// <summary>
        /// 返回值应在 [min, max) 区间内
        /// </summary>
        [Fact]
        public void GetRandomDouble_ResultWithinRange()
        {
            const double min = 1.5;
            const double max = 5.5;

            for (int i = 0; i < 1000; i++)
            {
                var value = RandomUtil.GetRandomDouble(min, max);
                Assert.True(value >= min);
                Assert.True(value < max);
            }
        }

        /// <summary>
        /// 不应返回 maxValue
        /// </summary>
        [Fact]
        public void GetRandomDouble_NeverEqualsMax()
        {
            const double min = 0;
            const double max = 1;
            for (int i = 0; i < 100_0000; i++)
            {
                var value = RandomUtil.GetRandomDouble(min, max);
                Assert.NotEqual(max, value);
            }
        }

        /// <summary>
        /// 固定 seed 应可复现
        /// </summary>
        [Fact]
        public void GetRandomDouble_DeterministicWithSeed()
        {
            var rng1 = new Random(1234);
            var rng2 = new Random(1234);

            for (int i = 0; i < 20; i++)
            {
                var v1 = RandomUtil.GetRandomDouble(0, 10, rng1);
                var v2 = RandomUtil.GetRandomDouble(0, 10, rng2);
                Assert.Equal(v1, v2);
            }
        }

        /// <summary>
        /// min == max 时应返回该值
        /// </summary>
        [Fact]
        public void GetRandomDouble_MinEqualsMax_ReturnsMin()
        {
            var value = RandomUtil.GetRandomDouble(3.14, 3.14);
            Assert.Equal(3.14, value);
        }

        /// <summary>
        /// 不传 random 应正常工作
        /// </summary>
        [Fact]
        public void GetRandomDouble_WithoutRandom_DoesNotThrow()
        {
            var value = RandomUtil.GetRandomDouble(0, 1);
            Assert.InRange(value, 0, 1);
        }

        /// <summary>
        /// 粗略统计测试（可选）
        /// 验证分布大体均匀
        /// </summary>
        [Fact]
        public void GetRandomDouble_DistributionRoughlyUniform()
        {
            const double min = 0;
            const double max = 10;

            var buckets = 10;
            var counts = new int[buckets];

            const int iterations = 100_0000;

            for (var i = 0; i < iterations; i++)
            {
                var value = RandomUtil.GetRandomDouble(min, max);

                var index = (int)value;
                if (index >= buckets) index = buckets - 1;

                counts[index]++;
            }

            var expected = iterations / buckets;
            var tolerance = (int)(expected * 0.1);

            foreach (var count in counts)
            {
                Assert.InRange(count, expected - tolerance, expected + tolerance);
            }
        }

        /// <summary>
        /// 当 minValue 为 NaN 或无穷大时，应抛出 ArgumentException
        /// </summary>
        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void GetRandomDouble_InvalidMinValue_ThrowsArgumentException(double invalidMin)
        {
            var ex = Assert.Throws<ArgumentException>(() => RandomUtil.GetRandomDouble(invalidMin, 10.0));
            // 验证异常确实是由 minValue 参数引发的
            Assert.Equal("minValue", ex.ParamName);
        }
        
        /// <summary>
        /// 当 maxValue 为 NaN 或无穷大时，应抛出 ArgumentException
        /// </summary>
        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void GetRandomDouble_InvalidMaxValue_ThrowsArgumentException(double invalidMax)
        {
            var ex = Assert.Throws<ArgumentException>(() => RandomUtil.GetRandomDouble(0.0, invalidMax));
            // 验证异常确实是由 maxValue 参数引发的
            Assert.Equal("maxValue", ex.ParamName);
        }
    }
}
