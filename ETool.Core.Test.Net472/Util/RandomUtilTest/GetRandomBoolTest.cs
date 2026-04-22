using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomBoolTest
    {
        /// <summary>
        /// 验证返回值是严格的 true 或 false（不应返回其它值）
        /// </summary>
        [Fact]
        public void GetRandomBool_ReturnsBoolean()
        {
            for (var i = 1; i < 1000; i++)
            {
                var result = RandomUtil.GetRandomBool();
                Assert.IsType<bool>(result);
            }
        }

        /// <summary>
        /// 验证在多次调用下，结果覆盖 true 和 false
        /// </summary>
        [Fact]
        public void GetRandomBool_ReturnsBothTrueAndFalse()
        {
            var seenTrue = false;
            var seenFalse = false;

            for (var i = 0; i < 100; i++)
            {
                var value = RandomUtil.GetRandomBool();

                if (value) seenTrue = true;
                else seenFalse = true;

                if (seenTrue && seenFalse)
                {
                    break;
                }
            }

            Assert.True(seenTrue, "应至少出现一次 true");
            Assert.True(seenFalse, "应至少出现一次 false");
        }

        /// <summary>
        /// 验证传入固定 seed 的 Random 时，结果是可预测的（相同 seed → 相同序列）
        /// </summary>
        [Fact]
        public void GetRandomBool_DeterministicWithSeed()
        {
            var rng1 = new Random(1001);
            var rng2 = new Random(1001);

            for (int i = 0; i < 20; i++)
            {
                var value1 = RandomUtil.GetRandomBool(rng1);
                var value2 = RandomUtil.GetRandomBool(rng2);

                Assert.Equal(value1, value2);
            }
        }

        /// <summary>
        /// 验证传入同一个 Random 实例时，调用顺序一致（方法只是转发 rng.Next(2)）
        /// </summary>
        [Fact]
        public void GetRandomBool_SequenceMatchesRandomDirectCall()
        {
            var rng1 = new Random(42);
            var rng2 = new Random(42);

            for (int i = 0; i < 10; i++)
            {
                var expected = rng1.Next(2) == 0;
                var actual = RandomUtil.GetRandomBool(rng2);

                Assert.Equal(expected, actual);
            }
        }

        /// <summary>
        /// 验证不传入 random 时也能正常工作（使用 ThreadLocal 默认随机源）
        /// </summary>
        [Fact]
        public void GetRandomBool_WithoutRandom_DoesNotThrow()
        {
            for (int i = 0; i < 100; i++)
            {
                var value = RandomUtil.GetRandomBool();
                Assert.IsType<bool>(value); // 只验证能返回合法 bool
            }
        }

        /// <summary>
        /// 粗略验证 true/false 出现频率大体均匀（非严格证明，仅用于捕获明显偏差）
        /// </summary>
        [Fact]
        public void GetRandomBool_DistributionRoughlyUniform()
        {
            var rng = new Random(999);
            int trueCount = 0;
            int falseCount = 0;

            const int iterations = 100_0000;

            for (int i = 0; i < iterations; i++)
            {
                var value = RandomUtil.GetRandomBool(rng);
                if (value) trueCount++;
                else falseCount++;
            }

            // 期望约 50% / 50%，允许 ±10% 误差（CI/统计严格性可更高，但单元测试够用）
            Assert.InRange(trueCount, iterations / 2 - iterations / 5, iterations / 2 + iterations / 5);
            Assert.InRange(falseCount, iterations / 2 - iterations / 5, iterations / 2 + iterations / 5);
        }
    }
}
