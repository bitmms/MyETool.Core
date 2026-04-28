namespace ETool.Core.Util.Enum
{
    /// <summary>
    /// 字符串是否忽略大小写
    /// </summary>
    public enum StringCase
    {
        /// <summary>
        /// 不忽略大小写（严格区分）
        /// </summary>
        Ordinal,

        /// <summary>
        /// 忽略大小写（不区分）（只忽略 26 个字母：A == a）
        /// </summary>
        OrdinalIgnoreCase
    }
}
