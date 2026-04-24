using System.Diagnostics;
using Xunit;

namespace ETool.Core.Test.Net472.Util
{
    public class TimeTestApp
    {
        /// <summary>
        /// 性能压力测试
        /// </summary>
        [Fact]
        public void Test1()
        {
            const int count = 100_0000;
            const int expectedMillisecond = 1 * 1000;
            var stopwatch = Stopwatch.StartNew();
            // TODO
            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds < expectedMillisecond, $"执行 {count} 个测试耗时 {stopwatch.ElapsedMilliseconds}ms，超出预期的 {expectedMillisecond}ms");
        }
    }
}
