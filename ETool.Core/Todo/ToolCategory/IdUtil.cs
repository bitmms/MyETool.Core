using System;
using System.Threading;

namespace ETool.Core.Todo.ToolCategory
{
    /// <summary>
    /// 唯一ID工具
    /// </summary>
    public class IdUtil
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static int _objectIdCounter = 0;

        /// <summary>
        /// 生成UUID
        /// </summary>
        /// <returns>生成的UUID</returns>
        /// <summary>
        /// 生成MongoDB ObjectId
        /// </summary>
        /// <returns>生成的MongoDB ObjectId</returns>
        public static string ObjectId()
        {
            byte[] timestamp = BitConverter.GetBytes((int)(DateTime.UtcNow - Epoch).TotalSeconds);
            byte[] machineIdentifier = BitConverter.GetBytes(Environment.MachineName.GetHashCode());
            byte[] processIdentifier = BitConverter.GetBytes(System.Diagnostics.Process.GetCurrentProcess().Id);
            byte[] increment = BitConverter.GetBytes(Interlocked.Increment(ref _objectIdCounter));

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestamp);
                Array.Reverse(machineIdentifier);
                Array.Reverse(processIdentifier);
                Array.Reverse(increment);
            }

            byte[] objectId = new byte[12];
            Buffer.BlockCopy(timestamp, 0, objectId, 0, 4);
            Buffer.BlockCopy(machineIdentifier, 2, objectId, 4, 2);
            Buffer.BlockCopy(processIdentifier, 0, objectId, 6, 2);
            Buffer.BlockCopy(increment, 1, objectId, 8, 3);

            return Convert.ToBase64String(objectId);
        }

        private static readonly long EpochTicks = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        private const long WorkerIdBits = 5L;
        private const long DatacenterIdBits = 5L;
        private const long SequenceBits = 12L;
        private const long MaxWorkerId = -1L ^ (-1L << (int)WorkerIdBits);
        private static readonly long maxDatacenterId = -1L ^ (-1L << (int)DatacenterIdBits);
        private static readonly long sequenceMask = -1L ^ (-1L << (int)SequenceBits);

        private static long lastTimestamp = -1L;
        private static long sequence = 0L;
        private static readonly object lockObj = new object();

        private static readonly Random random = new Random();

        private static readonly long workerId = random.Next((int)MaxWorkerId);
        private static readonly long datacenterId = random.Next((int)maxDatacenterId);

        /// <summary>
        /// 生成Snowflake ID
        /// </summary>
        /// <returns>生成的Snowflake ID</returns>
        public static long SnowflakeId()
        {
            lock (lockObj)
            {
                long timestamp = DateTime.UtcNow.Ticks - EpochTicks;

                if (timestamp < lastTimestamp)
                {
                    throw new Exception("Clock moved backwards, refusing to generate Snowflake ID");
                }

                if (timestamp == lastTimestamp)
                {
                    sequence = (sequence + 1) & sequenceMask;

                    if (sequence == 0)
                    {
                        timestamp = NextMillis(lastTimestamp);
                    }
                }
                else
                {
                    sequence = 0L;
                }

                lastTimestamp = timestamp;

                return (timestamp << (int)(WorkerIdBits + SequenceBits)) |
                       (datacenterId << (int)SequenceBits) |
                       (workerId << (int)SequenceBits) |
                       sequence;
            }
        }

        private static long NextMillis(long lastTimestamp)
        {
            long timestamp = DateTime.UtcNow.Ticks - EpochTicks;

            while (timestamp <= lastTimestamp)
            {
                timestamp = DateTime.UtcNow.Ticks - EpochTicks;
            }

            return timestamp;
        }
    }
}
