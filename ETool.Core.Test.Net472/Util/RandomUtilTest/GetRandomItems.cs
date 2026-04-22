using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ETool.Core.Util;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    /// <summary>
    /// RandomUtil.GetRandomItems 单元测试
    /// 目标：
    /// 1. 验证参数校验逻辑
    /// 2. 验证返回数量正确
    /// 3. 验证不重复（不放回抽样）
    /// 4. 验证可复现性（固定 seed）
    /// 5. 验证算法透明性
    /// 6. 验证概率大体均匀
    /// </summary>
    public class GetRandomItemsTest
    {
        /// <summary>
        /// items 为 null 应抛 ArgumentNullException
        /// </summary>
        [Fact]
        public void GetRandomItems_NullItems_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => RandomUtil.GetRandomItems<int>(null, 1));
        }

        /// <summary>
        /// k 小于 0 应抛 ArgumentOutOfRangeException
        /// </summary>
        [Fact]
        public void GetRandomItems_KLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => RandomUtil.GetRandomItems(list, -1));
        }

        /// <summary>
        /// k == 0 应返回空集合
        /// </summary>
        [Fact]
        public void GetRandomItems_KEqualsZero_ReturnsEmptyList()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = RandomUtil.GetRandomItems(list, 0);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        /// <summary>
        /// 空集合应抛 InvalidOperationException
        /// </summary>
        [Fact]
        public void GetRandomItems_EmptyCollection_ThrowsInvalidOperationException()
        {
            var list = new List<int>();
            Assert.Throws<InvalidOperationException>(() => RandomUtil.GetRandomItems(list, 1));
        }

        /// <summary>
        /// k > 集合数量时，应返回全部元素
        /// </summary>
        [Fact]
        public void GetRandomItems_KGreaterThanCount_ReturnsAllItems()
        {
            var list = new List<int> { 1, 2, 3 };

            var result = RandomUtil.GetRandomItems(list, 10);

            Assert.Equal(3, result.Count);
            Assert.True(result.All(x => list.Contains(x)));
        }

        /// <summary>
        /// 返回数量应等于 k（当 k 小于等于 N）
        /// </summary>
        [Fact]
        public void GetRandomItems_ReturnsCorrectCount()
        {
            var list = Enumerable.Range(1, 100).ToList();

            var result = RandomUtil.GetRandomItems(list, 10);

            Assert.Equal(10, result.Count);
        }

        /// <summary>
        /// 结果中不应包含重复元素（不放回抽样）
        /// </summary>
        [Fact]
        public void GetRandomItems_NoDuplicates()
        {
            var list = Enumerable.Range(1, 100).ToList();
            var result = RandomUtil.GetRandomItems(list, 20);
            Assert.Equal(result.Count, result.Distinct().Count());
        }

        /// <summary>
        /// 相同 seed 的 Random 应产生相同结果序列
        /// </summary>
        [Fact]
        public void GetRandomItems_DeterministicWithSeed()
        {
            var source = Enumerable.Range(1, 20).ToList();

            var rng1 = new Random(1234);
            var rng2 = new Random(1234);

            var result1 = RandomUtil.GetRandomItems(source, 5, rng1);
            var result2 = RandomUtil.GetRandomItems(source, 5, rng2);

            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// 粗略统计测试：
        /// 验证每个元素被选中的概率大致接近 k/N
        /// 用于防止明显偏差或算法错误
        /// </summary>
        [Fact]
        public void GetRandomItems_DistributionRoughlyUniform()
        {
            var source = Enumerable.Range(1, 5).ToList();
            int k = 1;

            var counts = new int[source.Count];
            const int iterations = 100_0000;

            for (int i = 0; i < iterations; i++)
            {
                var result = RandomUtil.GetRandomItems(source, k);
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
