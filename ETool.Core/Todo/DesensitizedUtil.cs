using System.Text.RegularExpressions;

namespace ETool.Core.Todo
{
    /// <summary>
    /// 信息脱敏工具类
    /// </summary>
    public class DesensitizedUtil
    {
        private static readonly Regex EmailRegex = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        /// <summary>
        /// 脱敏电子邮件，只保留邮箱前缀的前三个字符和后两个字符
        /// </summary>
        /// <param name="email">电子邮件</param>
        /// <returns>脱敏后的电子邮件</returns>
        public static string Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !EmailRegex.IsMatch(email))
            {
                return email;
            }

            int atIndex = email.IndexOf('@');
            if (atIndex <= 3)
            {
                return email;
            }

            string prefix = email.Substring(0, 3);
            string suffix = email.Substring(atIndex - 2, 2);
            return prefix + new string('*', atIndex - 5) + suffix + email.Substring(atIndex);
        }
    }
}
