namespace ETool.Core.Util.Enum
{
    /// <summary>
    /// 密码复杂度等级枚举
    /// </summary>
    /// <remarks>
    /// 密码需要满足的要求有：
    /// <para>1. 非空、非NULL、长度合规且仅包含允许的字符</para>
    /// <para>2. 至少包含有1个特殊符号</para>
    /// <para>3. 至少包含有1个数字</para>
    /// <para>4. 至少包含有1个大写字母</para>
    /// <para>5. 至少包含有1个小写字母</para>
    /// </remarks>
    public enum PasswordStrength
    {
        /// <summary>
        /// 无效，不满足[1] 或者 满足[1]且[2][3][4][5]满足0项 或者 满足[1]且[2][3][4][5]满足1项
        /// </summary>
        Invalid,

        /// <summary>
        /// 弱，满足 [1] 且 [2][3][4][5] 满足 2 项
        /// </summary>
        Weak,

        /// <summary>
        /// 中，满足 [1] 且 [2][3][4][5] 满足 3 项
        /// </summary>
        Medium,

        /// <summary>
        /// 强，满足 [1][2][3][4][5]
        /// </summary>
        Strong
    }
}
