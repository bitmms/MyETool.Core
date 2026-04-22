using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ETool.Core.Util;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomItemTest
    {
        [Fact]
        public void GetRandomItem_NullItems_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => RandomUtil.GetRandomItem<int>(null));
        }

        [Fact]
        public void GetRandomItem_EmptyList_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => RandomUtil.GetRandomItem(new List<int>()));
        }

        [Fact]
        public void GetRandomItem_EmptyEnumerable_ThrowsInvalidOperationException()
        {
            IEnumerable<int> source = Enumerable.Empty<int>();
            Assert.Throws<InvalidOperationException>(() => RandomUtil.GetRandomItem(source));
        }

        [Fact]
        public void GetRandomItem_SingleElement_ReturnsThatElement()
        {
            var list = new List<int> { 42 };
            var result = RandomUtil.GetRandomItem(list);
            Assert.Equal(42, result);
        }

        [Fact]
        public void GetRandomItem_List_DeterministicWithSeed()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            var rng1 = new Random(123);
            var rng2 = new Random(123);

            for (int i = 0; i < 10; i++)
            {
                var expected = list[rng1.Next(list.Count)];
                var actual = RandomUtil.GetRandomItem(list, rng2);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void GetRandomItem_DistributionRoughlyUniform()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var counts = new int[list.Count];

            const int iterations = 100_0000;

            for (int i = 0; i < iterations; i++)
            {
                int value = RandomUtil.GetRandomItem(list);
                counts[value - 1]++;
            }

            int expected = iterations / list.Count;
            int tolerance = (int)(expected * 0.05); // ±5%

            foreach (var count in counts)
            {
                Assert.InRange(count, expected - tolerance, expected + tolerance);
            }
        }
    }
}
