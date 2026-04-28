using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ETool.Core.Util
{
    /// <summary>
    /// 深度克隆工具类
    /// </summary>
    public static class CloneUtil
    {
        /// <summary>
        /// 深度克隆对象（同步）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">需要克隆的对象</param>
        /// <returns>克隆后的新对象</returns>
        public static T DeepClone<T>(T obj)
        {
            // 空对象直接返回默认值
            if (obj is null) return default;
            // 使用 DataContractSerializer 进行深度克隆
            var serializer = new DataContractSerializer(typeof(T));
            // 声明内存流
            using var ms = new MemoryStream();
            // 序列化对象到内存流
            serializer.WriteObject(ms, obj);
            // 重置流位置，准备反序列化
            ms.Position = 0;
            // 反序列化生成新对象并返回
            return (T)serializer.ReadObject(ms);
        }

        /// <summary>
        /// 深度克隆对象（异步）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">需要克隆的对象</param>
        /// <returns>异步任务，结果为克隆后的新对象</returns>
        /// <remarks>
        /// <code>
        ///  注意：
        ///   1. 序列化属于 CPU 密集型操作，未提供原生的异步序列化 API
        ///   2. 本方法通过 Task.Run 将同步操作包装为异步，防止阻塞调用线程
        ///   3. 这是同步操作转为异步模式的标准、官方推荐做法
        /// </code>
        /// </remarks>
        public static async Task<T> DeepCloneAsync<T>(T obj)
        {
            return await Task.Run(() => DeepClone(obj));
        }
    }
}
