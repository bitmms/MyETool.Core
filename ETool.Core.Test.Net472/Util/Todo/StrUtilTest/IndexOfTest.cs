using ETool.Core.Todo.MyUtil;
using Xunit;

namespace ETool.Core.Test.Net472.Util.StrUtilTest
{
    public class IndexOfTest
    {
        [Fact]
        public void IndexOf_NullSource_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOf(null, "abc", 0, 5));
        }

        [Fact]
        public void IndexOf_NullTarget_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOf("hello world", null, 0, 5));
        }

        [Fact]
        public void IndexOf_EmptyTarget_ReturnsStart()
        {
            Assert.Equal(3, StrUtil.IndexOf("hello", "", 3, 2));
        }

        [Fact]
        public void IndexOf_StartBeyondLength_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOf("hello", "o", 10, 1));
        }

        [Fact]
        public void IndexOf_CountZeroOrNegative_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOf("hello", "e", 1, 0));
            Assert.Equal(-1, StrUtil.IndexOf("hello", "e", 1, -5));
        }

        [Fact]
        public void IndexOf_StartNegative_AdjustedToZero()
        {
            // Should treat start = -1 as start = 0
            Assert.Equal(1, StrUtil.IndexOf("hello", "e", -1, 5));
        }

        [Fact]
        public void IndexOf_CountExceedsAvailableChars_AdjustedAutomatically()
        {
            // source length = 5, start = 2 → max count = 3
            // target "ll" found at index 2
            Assert.Equal(2, StrUtil.IndexOf("hello", "ll", 2, 10));
        }

        [Fact]
        public void IndexOf_CountLessThanTargetLength_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOf("hello", "world", 0, 4)); // target len=5, count=4
        }

        [Fact]
        public void IndexOf_FoundAtBeginning()
        {
            Assert.Equal(0, StrUtil.IndexOf("hello", "he", 0, 5));
        }

        [Fact]
        public void IndexOf_FoundInMiddle()
        {
            Assert.Equal(2, StrUtil.IndexOf("hello", "ll", 0, 5));
        }

        [Fact]
        public void IndexOf_NotFound()
        {
            Assert.Equal(-1, StrUtil.IndexOf("hello", "xyz", 0, 5));
        }

        [Fact]
        public void IndexOf_IgnoreCase_Match()
        {
            Assert.Equal(0, StrUtil.IndexOf("Hello", "HELLO", 0, 5, ignoreCase: true));
            Assert.Equal(1, StrUtil.IndexOf("aBcDe", "bcd", 0, 5, ignoreCase: true));
        }

        [Fact]
        public void IndexOf_IgnoreCase_NoMatch()
        {
            Assert.Equal(-1, StrUtil.IndexOf("Hello", "hella", 0, 5, ignoreCase: true));
        }

        [Fact]
        public void IndexOf_SearchWithinLimitedRange_NotFoundOutside()
        {
            // Search only in "ell" (start=1, count=3) → "lo" not fully in range
            Assert.Equal(-1, StrUtil.IndexOf("hello", "lo", 1, 3));
        }

        [Fact]
        public void IndexOf_SearchWithinLimitedRange_FoundInside()
        {
            // Search in "llo" (start=2, count=3) → "ll" at index 2
            Assert.Equal(2, StrUtil.IndexOf("hello", "ll", 2, 3));
        }

        [Fact]
        public void IndexOf_SingleCharTarget()
        {
            Assert.Equal(4, StrUtil.IndexOf("hello", "o", 0, 5));
        }

        [Fact]
        public void IndexOf_TargetLongerThanSource_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOf("hi", "hello", 0, 2));
        }

        [Fact]
        public void IndexOf_ExactMatchFullString()
        {
            Assert.Equal(0, StrUtil.IndexOf("test", "test", 0, 4));
        }

        [Fact]
        public void IndexOf_EmptySource_NonEmptyTarget_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOf("", "a", 0, 0));
        }

        [Fact]
        public void IndexOf_StartAndCountZero_ValidEmptySearch()
        {
            // Searching in zero-length window: only valid if target is empty
            Assert.Equal(0, StrUtil.IndexOf("abc", "", 0, 1)); // empty target → return start
            Assert.Equal(2, StrUtil.IndexOf("abc", "", 2, 0)); // even if count=0, empty target returns start
        }

        [Fact]
        public void IndexOf_StartAtEndOfSource_CountZero_TargetEmpty_ReturnsStart()
        {
            Assert.Equal(3, StrUtil.IndexOf("abc", "", 3, 0));
        }

        [Fact]
        public void IndexOf_StartEqualsSourceLength_NonEmptyTarget_ReturnsMinus1()
        {
            Assert.Equal(-1, StrUtil.IndexOf("abc", "a", 3, 1));
        }

        [Fact]
        public void IndexOf_UnicodeCharacters_CaseSensitive()
        {
            Assert.Equal(0, StrUtil.IndexOf("Café", "Café", 0, 4));
            Assert.Equal(-1, StrUtil.IndexOf("Café", "café", 0, 4)); // case-sensitive by default
        }

        [Fact]
        public void IndexOf_UnicodeCharacters_IgnoreCase()
        {
            // Note: CharUtil only handles ASCII
            Assert.Equal(-1, StrUtil.IndexOf("Café", "CAFÉ", 0, 4, ignoreCase: true));
        }

        [Fact]
        public void IndexOf_TargetWithSpecialChars()
        {
            Assert.Equal(2, StrUtil.IndexOf("ab$%cd", "$%", 0, 6));
        }

        [Fact]
        public void IndexOf_RepeatedPattern_FindsFirstOccurrence()
        {
            Assert.Equal(0, StrUtil.IndexOf("aaaa", "aa", 0, 4)); // should return first match at 0, not 1 or 2
        }

        [Fact]
        public void IndexOf_OverlappingMatchWithinRange()
        {
            // Search "aaa" in "aaaa", start=1, count=3 → substring = "aaa", target "aa" → first at index 1
            Assert.Equal(1, StrUtil.IndexOf("aaaa", "aa", 1, 3));
        }

        [Fact]
        public void IndexOf_LargeCountButSmallActualWindow()
        {
            // Edge: count is huge, but source is short
            Assert.Equal(0, StrUtil.IndexOf("x", "x", 0, 1000000));
        }

        [Fact]
        public void IndexOf_TargetSameAsSourceSubstringAtBoundary()
        {
            // Ensure it works when match ends exactly at start + count - 1
            // source: "abcdef", search "de" in [2,4) → chars 'c','d','e','f' → "de" starts at 3
            Assert.Equal(3, StrUtil.IndexOf("abcdef", "de", 2, 4));
        }

        [Fact]
        public void IndexOf_EmptySource_EmptyTarget_ReturnsStart()
        {
            Assert.Equal(0, StrUtil.IndexOf("", "", 0, 0));
        }

        [Fact]
        public void IndexOf_StartNegativeAndCountLarge_AdjustsCorrectly()
        {
            // start = -5 → becomes 0; count = 100 → clamped to source length
            Assert.Equal(0, StrUtil.IndexOf("hi", "hi", -5, 100));
        }

        [Fact]
        public void IndexOf_MatchAtVeryEndOfRange()
        {
            // source: "123456789", target: "789", start=3, count=6 → covers "456789", match at 6
            Assert.Equal(6, StrUtil.IndexOf("123456789", "789", 3, 6));
        }

// 可选：如果你的 CharUtil.ToUpperLetter 仅支持 ASCII，可加此测试避免 Unicode 假阳性
        [Fact]
        public void IndexOf_ASCIIOnlyIgnoreCase()
        {
            Assert.Equal(0, StrUtil.IndexOf("AbC", "abc", 0, 3, ignoreCase: true));
            Assert.Equal(-1, StrUtil.IndexOf("AbC", "abd", 0, 3, ignoreCase: true));
        }
    }
}
