namespace ETool.Core.Enum
{
    /// <summary>
    /// 密码复杂度等级枚举
    /// </summary>
    /// <remarks>
    /// 密码需要满足的要求有：
    /// <para>1. 非空、非NULL、仅包含数字、字母或允许的特殊字符（见 AllowedSpecialChars），且长度合规 [min, max]</para>
    /// <para>2. 至少包含有1个特殊符号</para>
    /// <para>3. 至少包含有1个数字</para>
    /// <para>4. 至少包含有1个大写字母</para>
    /// <para>5. 至少包含有1个小写字母</para>
    /// <para>6. 通过弱口令检测（包括字典弱口令、连续序列、全重复等）</para>
    /// </remarks>
    public enum PasswordStrength
    {
        /// <summary>
        /// 无效，不满足 [1]
        /// </summary>
        Invalid,

        /// <summary>
        /// 极弱，满足 [1] 且 [2][3][4][5] 仅满足 1 项，不考虑 [6]
        /// </summary>
        Weak,

        /// <summary>
        /// 较弱，满足 [1] 且 [2][3][4][5] 仅满足 2 项，不考虑 [6]
        /// </summary>
        Weakish,

        /// <summary>
        /// 中等，满足 [1] 且 [2][3][4][5] 仅满足 3 项，不考虑 [6]
        /// </summary>
        Medium,

        /// <summary>
        /// 较高，满足 [1][2][3][4][5] 但不满足 [6]
        /// </summary>
        Strong,

        /// <summary>
        /// 极高，满足 [1][2][3][4][5][6]
        /// </summary>
        VeryStrong
    }
}
