using System;
using System.Collections.Generic;

namespace ETool.Core.Todo.MyUtil
{
    /// <summary>
    /// 分页工具类
    /// </summary>
    public static class PageUtil
    {
        /// <summary>
        /// 根据总数计算总页数
        /// </summary>
        /// <param name="totalCount">元素的总数量</param>
        /// <param name="pageSize">每页元素的数量</param>
        /// <returns>总页数</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>totalCount</c> 小于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageSize</c> 小于等于 0</exception>
        /// <example>
        /// <code>
        /// GetTotalPages(0, 10)   → 0
        /// GetTotalPages(1, 10)   → 1
        /// GetTotalPages(10, 10)  → 1
        /// GetTotalPages(11, 10)  → 2
        /// GetTotalPages(97, 10)  → 10
        /// </code>
        /// </example>
        public static int GetTotalPages(int totalCount, int pageSize)
        {
            if (totalCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(totalCount),
                    totalCount,
                    $"元素总数 '{nameof(totalCount)}' 必须大于等于 0，实际值：{totalCount}");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    pageSize,
                    $"每页大小 '{nameof(pageSize)}' 必须大于 0，实际值：{pageSize}");
            }

            return totalCount == 0 ? 0 : (totalCount - 1) / pageSize + 1;
        }

        /// <summary>
        /// 计算分页的开始索引（包含）
        /// </summary>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">每页包含的元素数量</param>
        /// <returns>分页的开始索引（包含）</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageNumber</c> 小于等于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageSize</c> 小于等于 0</exception>
        /// <exception cref="OverflowException">计算得出的开始索引超出了 int 的范围</exception>
        /// <example>
        /// <code>
        /// GetStartIndex(1, 10) → 0
        /// GetStartIndex(2, 10) → 10
        /// GetStartIndex(3, 5)  → 10
        /// </code>
        /// </example>
        public static int GetStartIndex(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageNumber),
                    pageNumber,
                    $"当前页码 '{nameof(pageNumber)}' 必须大于 0，实际值：{pageNumber}");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    pageSize,
                    $"每页大小 '{nameof(pageSize)}' 必须大于 0，实际值：{pageSize}");
            }

            var startIndexLong = (long)(pageNumber - 1) * pageSize;
            if (startIndexLong > int.MaxValue)
            {
                throw new OverflowException($"分页开始索引溢出超出 int 范围：(pageNumber - 1) * pageSize = ({pageNumber} - 1) * {pageSize} = {startIndexLong} > int.MaxValue = {int.MaxValue}");
            }

            return (int)startIndexLong;
        }

        /// <summary>
        /// 计算分页的结束索引（不包含）
        /// </summary>
        /// <param name="startIndex">分页的开始索引（包含）</param>
        /// <param name="pageSize">每页包含的元素数量</param>
        /// <returns>分页的结束索引（不包含）</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>startIndex</c> 小于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageSize</c> 小于等于 0</exception>
        /// <exception cref="OverflowException">计算得出的结束索引超出了 int 的范围</exception>
        /// <example>
        /// <code>
        /// GetEndIndexExclusiveByStartIndex(11, 10) → 21
        /// GetEndIndexExclusiveByStartIndex(18, 10) → 28
        /// GetEndIndexExclusiveByStartIndex(33, 15) → 48
        /// </code>
        /// </example>
        public static int GetEndIndexExclusiveByStartIndex(int startIndex, int pageSize)
        {
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(startIndex),
                    startIndex,
                    $"分页开始索引 '{nameof(startIndex)}' 必须大于等于 0，实际值：{startIndex}");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    pageSize,
                    $"每页大小 '{nameof(pageSize)}' 必须大于 0，实际值：{pageSize}");
            }

            var endIndexLong = (long)startIndex + pageSize;
            if (endIndexLong > int.MaxValue)
            {
                throw new OverflowException($"分页结束索引溢出超出 int 范围：startIndex + pageSize = {startIndex} + {pageSize} = {endIndexLong} > int.MaxValue = {int.MaxValue}");
            }

            return (int)endIndexLong;
        }

        /// <summary>
        /// 计算分页的结束索引（不包含）
        /// </summary>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">每页包含的元素数量</param>
        /// <returns>分页的结束索引（不包含）</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageNumber</c> 小于等于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageSize</c> 小于等于 0</exception>
        /// <exception cref="OverflowException">计算得出的结束索引超出了 int 的范围</exception>
        /// <example>
        /// <code>
        /// GetEndIndexExclusive(1, 10) → 10
        /// GetEndIndexExclusive(2, 10) → 20
        /// GetEndIndexExclusive(3, 5)  → 15
        /// </code>
        /// </example>
        public static int GetEndIndexExclusive(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageNumber),
                    pageNumber,
                    $"当前页码 '{nameof(pageNumber)}' 必须大于 0，实际值：{pageNumber}");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    pageSize,
                    $"每页大小 '{nameof(pageSize)}' 必须大于 0，实际值：{pageSize}");
            }

            var endIndexLong = (long)pageNumber * pageSize; // 由于 int.MaxValue * int.MaxValue < long.MaxValue，所以这里不用担心 long 溢出
            if (endIndexLong > int.MaxValue)
            {
                throw new OverflowException($"分页结束索引溢出超出 int 范围：pageNumber * pageSize = {pageNumber} * {pageSize} = {endIndexLong} > int.MaxValue = {int.MaxValue}");
            }

            return (int)endIndexLong;
        }

        /// <summary>
        /// 计算分页的开始索引（包含）和结束索引（不包含）
        /// </summary>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">每页包含的元素数量</param>
        /// <returns>包含开始索引和结束索引的元组</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageNumber</c> 小于等于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageSize</c> 小于等于 0</exception>
        /// <exception cref="OverflowException">内部计算的开始索引或结束索引超出了 int 的范围</exception>
        /// <example>
        /// <code>
        /// GetPageIndexRange(1, 10) → (0, 10)
        /// GetPageIndexRange(2, 10) → (10, 20)
        /// GetPageIndexRange(3, 5)  → (10, 15)
        /// </code>
        /// </example>
        public static (int StartIndex, int EndIndex) GetPageIndexRange(int pageNumber, int pageSize)
        {
            var startIndex = GetStartIndex(pageNumber, pageSize);
            var endIndex = GetEndIndexExclusiveByStartIndex(startIndex, pageSize);

            return (startIndex, endIndex);
        }

        /// <summary>
        /// 获取分页控件中应显示的可见页码列表
        /// </summary>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="totalPages">总页数</param>
        /// <param name="visiblePageCount">要显示的页码数量</param>
        /// <returns>按顺序排列的可见页码数组</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageNumber</c> 小于等于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>totalPages</c> 小于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageNumber</c> 大于 <c>totalPages</c></exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>visiblePageCount</c> 小于等于 0</exception>
        /// <example>
        /// <code>
        /// GetVisiblePageNumbers(1, 10, 5) → [1, 2, 3, 4, 5]
        /// GetVisiblePageNumbers(3, 10, 5) → [1, 2, 3, 4, 5]
        /// GetVisiblePageNumbers(5, 10, 5) → [3, 4, 5, 6, 7]
        /// GetVisiblePageNumbers(10, 10, 5) → [6, 7, 8, 9, 10]
        /// GetVisiblePageNumbers(1, 0, 5) → []
        /// GetVisiblePageNumbers(1, 3, 7) → [1, 2, 3]
        /// </code>
        /// </example>
        public static int[] GetVisiblePageNumbers(int pageNumber, int totalPages, int visiblePageCount)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageNumber),
                    pageNumber,
                    $"当前页码 '{nameof(pageNumber)}' 必须大于 0，实际值：{pageNumber}");
            }

            if (totalPages < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(totalPages),
                    totalPages,
                    $"总页数 '{nameof(totalPages)}' 必须大于等于 0，实际值：{totalPages}");
            }

            if (totalPages == 0)
            {
                return Array.Empty<int>();
            }

            if (pageNumber > totalPages)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageNumber),
                    pageNumber,
                    $"当前页码 '{nameof(pageNumber)}' 不能大于总页数 {totalPages}，实际值：{pageNumber}");
            }

            if (visiblePageCount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(visiblePageCount),
                    visiblePageCount,
                    $"要显示的页码数量 '{nameof(visiblePageCount)}' 必须大于 0，实际值：{visiblePageCount}");
            }

            var result = new int[Math.Min(totalPages, visiblePageCount)];

            // 总页数不超过要显示的数量
            if (visiblePageCount >= totalPages)
            {
                for (var i = 0; i < totalPages; i++)
                {
                    result[i] = i + 1;
                }

                return result;
            }

            var left = visiblePageCount >> 1; // 当前页左侧有多少页
            var right = visiblePageCount >> 1; // 当前页右侧有多少页
            if (visiblePageCount % 2 == 0)
            {
                left--; // 使当前页视觉偏左，符合主流 UI 习惯
            }

            var startPage = pageNumber - left; // 开始页码
            var endPage = pageNumber + right; // 结束页码

            if (startPage <= 0)
            {
                var shift = 1 - startPage;
                startPage += shift;
            }

            if (endPage > totalPages)
            {
                var shift = endPage - totalPages;
                startPage -= shift;
            }

            for (var i = 0; i < visiblePageCount; i++)
            {
                result[i] = startPage + i;
            }

            return result;
        }

        /// <summary>
        /// 获取分页控件中应显示的可见页码列表
        /// </summary>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="totalCount">元素的总数量</param>
        /// <param name="pageSize">每页元素的数量</param>
        /// <param name="visiblePageCount">要显示的页码数量</param>
        /// <returns>按顺序排列的可见页码数组</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageNumber</c> 小于等于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>totalCount</c> 小于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageSize</c> 小于等于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageNumber</c> 大于根据 <c>totalCount</c> 和 <c>pageSize</c> 计算出的总页数</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>visiblePageCount</c> 小于等于 0</exception>
        /// <example>
        /// <code>
        /// GetVisiblePageNumbers(3, 97, 10, 5) → [1, 2, 3, 4, 5]
        /// GetVisiblePageNumbers(5, 97, 10, 5) → [3, 4, 5, 6, 7]
        /// GetVisiblePageNumbers(10, 97, 10, 5) → [6, 7, 8, 9, 10]
        /// GetVisiblePageNumbers(1, 0, 10, 5) → []
        /// </code>
        /// </example>
        public static int[] GetVisiblePageNumbers(int pageNumber, int totalCount, int pageSize, int visiblePageCount)
        {
            return GetVisiblePageNumbers(pageNumber, GetTotalPages(totalCount, pageSize), visiblePageCount);
        }

        /// <summary>
        /// 获取用于 UI 显示的分页范围列表
        /// </summary>
        /// <param name="totalCount">元素的总数量</param>
        /// <param name="pageSize">每页元素的数量</param>
        /// <returns>
        /// 用于 UI 显示的分页范围列表
        /// 一个列表，其中每个元素为具名元组 (pageNumber, DisplayStart, DisplayEnd)：
        /// <list type="bullet">
        ///   <item><description><c>pageNumber</c>：从 1 开始的页码</description></item>
        ///   <item><description><c>DisplayStart</c>：该页第一条记录的 1-based 序号</description></item>
        ///   <item><description><c>DisplayEnd</c>：该页最后一条记录的 1-based 序号（inclusive）</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>totalCount</c> 小于 0</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>pageSize</c> 小于等于 0</exception>
        public static List<(int pageNumber, int DisplayStart, int DisplayEnd)> GetPageDisplayRangesList(int totalCount, int pageSize)
        {
            if (totalCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(totalCount),
                    totalCount,
                    $"元素总数 '{nameof(totalCount)}' 必须大于等于 0，实际值：{totalCount}");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    pageSize,
                    $"每页大小 '{nameof(pageSize)}' 必须大于 0，实际值：{pageSize}");
            }

            if (totalCount == 0)
            {
                return new List<(int pageNumber, int DisplayStart, int DisplayEnd)>();
            }

            var pages = GetTotalPages(totalCount, pageSize);
            var list = new List<(int pageNumber, int DisplayStart, int DisplayEnd)>(pages);
            for (var i = 1; i <= pages; i++)
            {
                // 因为 (pages - 1) * pageSize + X = totalCount 并且 X >=1 && X <= pageSize
                // 所以 (pages - 1) * pageSize < totalCount
                // 所以 start = (i - 1) * pageSize + 1 <= (pages - 1) * pageSize + 1 < totalCount + 1
                // 所以 start <= totalCount <= int.Max
                // 所以 start 不会溢出
                var start = (i - 1) * pageSize + 1;
                // 将可能出现溢出的 i * pageSize 转化为不会出现溢出的 (pages - 1) * pageSize
                var end = Math.Min(totalCount - pageSize, (i - 1) * pageSize) + pageSize;
                list.Add((i, start, end));
            }

            return list;
        }
    }
}
