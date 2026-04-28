using System;
using System.Collections.Generic;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.CloneUtilTest
{
    public class DeepCloneTest
    {
        [Fact]
        public void Test1()
        {
            var list1 = new List<string>
            {
                "aaa",
                "bbb",
                "CCC",
                "eee"
            };

            var list3 = CloneUtil.DeepClone(list1);

            Assert.Equal(list1[0], list3[0]);
            Assert.Equal(list1[1], list3[1]);
            Assert.Equal(list1[2], list3[2]);
            Assert.Equal(list1[3], list3[3]);
            Assert.Equal(list1.Count, list3.Count);
            Assert.False(list1 == list3);
        }

        [Fact]
        public void Test2()
        {
            List<string> s1 = null;
            List<string> s2 = CloneUtil.DeepClone<List<string>>(s1);

            Assert.True(s1 == s2);
            Assert.True(s1 == null);
            Assert.True(s2 == null);
        }
    }
}
