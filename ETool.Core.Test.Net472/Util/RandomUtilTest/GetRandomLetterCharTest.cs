using System;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.RandomUtilTest
{
    public class GetRandomLetterCharTest
    {
        /// <summary>
        /// 验证返回字符始终为英文字母
        /// </summary>
        [Fact]
        public void GetRandomLetterChar_ResultIsLetter()
        {
            var rng = new Random(123);

            for (var i = 0; i < 1000; i++)
            {
                var value = RandomUtil.GetRandomLetterChar(rng);
                Assert.True((value >= 'a' && value <= 'z') || (value >= 'A' && value <= 'Z'), $"生成了非法字符: {value}");
            }
        }

        /// <summary>
        /// 验证能够生成所有 52 个字母
        /// </summary>
        [Fact]
        public void GetRandomLetterChar_CoversAllLetters()
        {
            var seen = new bool[52];
            var rng = new Random(999);

            for (var i = 0; i < 100_0000; i++)
            {
                var value = RandomUtil.GetRandomLetterChar(rng);
                if (value >= 'a' && value <= 'z')
                {
                    seen[value - 'a'] = true;
                }
                else
                {
                    seen[26 + (value - 'A')] = true;
                }
            }

            for (int i = 0; i < 52; i++)
            {
                Assert.True(seen[i], $"未生成字母索引 {i}");
            }
        }

        /// <summary>
        /// 相同 seed 应产生相同序列
        /// </summary>
        [Fact]
        public void GetRandomLetterChar_DeterministicWithSeed()
        {
            var rng1 = new Random(1001);
            var rng2 = new Random(1001);

            for (var i = 0; i < 20; i++)
            {
                var value1 = RandomUtil.GetRandomLetterChar(rng1);
                var value2 = RandomUtil.GetRandomLetterChar(rng2);
                Assert.Equal(value1, value2);
            }
        }

        /// <summary>
        /// 验证方法是 Random.Next(52) 的透明包装
        /// </summary>
        [Fact]
        public void GetRandomLetterChar_SequenceMatchesRandomDirectCall()
        {
            var rng1 = new Random(42);
            var rng2 = new Random(42);

            for (int i = 0; i < 20; i++)
            {
                int index = rng1.Next(52);
                char expected = index < 26 ? (char)('a' + index) : (char)('A' + (index - 26));
                char actual = RandomUtil.GetRandomLetterChar(rng2);

                Assert.Equal(expected, actual);
            }
        }

        /// <summary>
        /// 不传入 Random 时也应正常工作
        /// </summary>
        [Fact]
        public void GetRandomLetterChar_WithoutRandom_DoesNotThrow()
        {
            for (int i = 0; i < 100; i++)
            {
                char value = RandomUtil.GetRandomLetterChar();

                Assert.True(
                    (value >= 'a' && value <= 'z') ||
                    (value >= 'A' && value <= 'Z')
                );
            }
        }

        /// <summary>
        /// 粗略验证大小写比例接近 1:1
        /// </summary>
        [Fact]
        public void GetRandomLetterChar_DistributionRoughlyBalanced()
        {
            var rng = new Random(777);
            int lowerCount = 0;
            int upperCount = 0;

            const int iterations = 100_0000;

            for (int i = 0; i < iterations; i++)
            {
                char value = RandomUtil.GetRandomLetterChar(rng);

                if (value >= 'a')
                    lowerCount++;
                else
                    upperCount++;
            }

            int expected = iterations / 2;
            int tolerance = iterations / 5;

            Assert.InRange(lowerCount, expected - tolerance, expected + tolerance);
            Assert.InRange(upperCount, expected - tolerance, expected + tolerance);
        }
    }
}
