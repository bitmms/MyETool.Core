using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ETool.Core.Util;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    /// <summary>
    /// RandomUtil.GetRandomItemsWithReplacement 单元测试
    /// 目标：
    /// 1. 验证参数校验
    /// 2. 验证返回数量
    /// 3. 验证允许重复
    /// 4. 验证可复现性
    /// 5. 验证透明实现（等价 random.Next）
    /// 6. 验证概率结构
    /// </summary>
    public class GetRandomItemsWithReplacementTest
    {
        /// <summary>
        /// items 为 null 应抛 ArgumentNullException
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_NullItems_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => RandomUtil.GetRandomItemsWithReplacement<int>(null, 1));
        }

        /// <summary>
        /// k 小于 0 应抛 ArgumentOutOfRangeException
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_KLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => RandomUtil.GetRandomItemsWithReplacement(list, -1));
        }

        /// <summary>
        /// k == 0 应返回空集合
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_KEqualsZero_ReturnsEmptyList()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = RandomUtil.GetRandomItemsWithReplacement(list, 0);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        /// <summary>
        /// 空集合应抛 InvalidOperationException
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_EmptyCollection_ThrowsInvalidOperationException()
        {
            var list = new List<int>();
            Assert.Throws<InvalidOperationException>(() => RandomUtil.GetRandomItemsWithReplacement(list, 1));
        }

        /// <summary>
        /// 返回数量应等于 k
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_ReturnsCorrectCount()
        {
            var list = Enumerable.Range(1, 5).ToList();
            var result = RandomUtil.GetRandomItemsWithReplacement(list, 10);
            Assert.Equal(10, result.Count);
        }

        /// <summary>
        /// 允许重复元素（放回抽样特性）
        /// 当集合只有一个元素时，结果应全部为该元素
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_AllowsDuplicates()
        {
            var list = new List<int> { 42 };
            var result = RandomUtil.GetRandomItemsWithReplacement(list, 5);
            Assert.Equal(5, result.Count);
            Assert.All(result, x => Assert.Equal(42, x));
        }

        /// <summary>
        /// 固定 seed 时应可复现
        /// 相同 seed → 相同结果序列
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_DeterministicWithSeed()
        {
            var source = Enumerable.Range(1, 10).ToList();
            var rng1 = new Random(1234);
            var rng2 = new Random(1234);
            var result1 = RandomUtil.GetRandomItemsWithReplacement(source, 5, rng1);
            var result2 = RandomUtil.GetRandomItemsWithReplacement(source, 5, rng2);
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// 验证透明实现：
        /// 应等价于循环 k 次调用 random.Next(list.Count)
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_MatchesManualImplementation()
        {
            var source = Enumerable.Range(1, 10).ToList();

            var rng1 = new Random(999);
            var rng2 = new Random(999);

            var expected = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                expected.Add(source[rng1.Next(source.Count)]);
            }

            var actual = RandomUtil.GetRandomItemsWithReplacement(source, 5, rng2);

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// 粗略统计测试：
        /// 验证每个元素被选中的概率大致均匀
        /// 用于防止明显偏差
        /// </summary>
        [Fact]
        public void GetRandomItemsWithReplacement_DistributionRoughlyUniform()
        {
            var source = Enumerable.Range(1, 5).ToList();
            var counts = new int[source.Count];

            const int iterations = 100_0000;

            for (int i = 0; i < iterations; i++)
            {
                var result = RandomUtil.GetRandomItemsWithReplacement(source, 1);
                counts[result[0] - 1]++;
            }

            int expected = iterations / source.Count;
            int tolerance = (int)(expected * 0.05); // ±5%

            foreach (var count in counts)
            {
                Assert.InRange(count, expected - tolerance, expected + tolerance);
            }
        }
    }
}
