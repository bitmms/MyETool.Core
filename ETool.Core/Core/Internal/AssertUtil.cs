using System;

namespace ETool.Core.Core.Internal
{
    /// <summary>
    /// 断言工具类
    /// </summary>
    internal static class AssertUtil
    {
        /// <summary>
        /// 条件为 true 时抛异常
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="message">异常信息</param>
        public static void IfTrue(bool condition, string message)
        {
            if (condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
