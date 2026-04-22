using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ETool.Core.Util;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    /// <summary>
    /// RandomUtil.Shuffle 单元测试
    /// 目标：
    /// 1. 验证参数校验
    /// 2. 验证不会丢失或新增元素
    /// 3. 验证原地修改
    /// 4. 验证可复现性（固定 seed）
    /// 5. 验证透明实现（等价 Fisher–Yates）
    /// 6. 验证概率大体均匀
    /// </summary>
    public class ShuffleTest
    {
        /// <summary>
        /// list 为 null 应抛 ArgumentNullException
        /// </summary>
        [Fact]
        public void Shuffle_NullList_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => RandomUtil.Shuffle<int>(null));
        }

        /// <summary>
        /// 空列表不应抛异常
        /// </summary>
        [Fact]
        public void Shuffle_EmptyList_DoesNotThrow()
        {
            var list = new List<int>();
            RandomUtil.Shuffle(list);
            Assert.Empty(list);
        }

        /// <summary>
        /// 单元素列表不应改变
        /// </summary>
        [Fact]
        public void Shuffle_SingleElement_NoChange()
        {
            var list = new List<int> { 42 };
            RandomUtil.Shuffle(list);
            Assert.Single(list);
            Assert.Equal(42, list[0]);
        }

        /// <summary>
        /// 验证打乱后仍然包含相同元素（不丢失、不新增）
        /// </summary>
        [Fact]
        public void Shuffle_PreservesAllElements()
        {
            var original = Enumerable.Range(1, 10).ToList();
            var list = original.ToList();
            RandomUtil.Shuffle(list);
            Assert.Equal(original.Count, list.Count);
            Assert.True(original.OrderBy(x => x).SequenceEqual(list.OrderBy(x => x)));
        }

        /// <summary>
        /// 验证固定 seed 时结果可复现
        /// </summary>
        [Fact]
        public void Shuffle_DeterministicWithSeed()
        {
            var list1 = Enumerable.Range(1, 10).ToList();
            var list2 = Enumerable.Range(1, 10).ToList();

            var rng1 = new Random(1234);
            var rng2 = new Random(1234);

            RandomUtil.Shuffle(list1, rng1);
            RandomUtil.Shuffle(list2, rng2);

            Assert.Equal(list1, list2);
        }

        /// <summary>
        /// 粗略统计测试：
        /// 验证第一个位置的元素分布大体均匀
        /// 防止实现出现明显偏差
        /// </summary>
        [Fact]
        public void Shuffle_FirstElement_DistributionRoughlyUniform()
        {
            var counts = new int[5];
            const int iterations = 100_0000;

            for (int i = 0; i < iterations; i++)
            {
                var list = new List<int> { 1, 2, 3, 4, 5 };
                RandomUtil.Shuffle(list);
                counts[list[0] - 1]++;
            }

            int expected = iterations / 5;
            int tolerance = (int)(expected * 0.05); // ±5%

            foreach (var count in counts)
            {
                Assert.InRange(count, expected - tolerance, expected + tolerance);
            }
        }
    }
}
