using System;

namespace ETool.Core.Util
{
    /// <summary>
    /// 断言工具类
    /// </summary>
    public static class AssertUtil
    {
        public static void Xx()
        {
            // 1. 字符的大小写转换使用这个
            Console.WriteLine(char.ToUpperInvariant('a'));
            Console.WriteLine(char.ToLowerInvariant('A'));

            // 2. 字符串的大小写转换使用这个
            Console.WriteLine("aaa".ToUpperInvariant());
            Console.WriteLine("AAA".ToLowerInvariant());

            // 3. 字符比较

            // 4. 字符串比较
            Console.WriteLine(string.Equals("aaa", "AAA", StringComparison.Ordinal));
            Console.WriteLine(string.Equals("aaa", "AAA", StringComparison.OrdinalIgnoreCase));
        }
    }
}
