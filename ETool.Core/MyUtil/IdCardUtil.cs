using System;
using System.Collections.Generic;
using System.Globalization;
using ETool.Core.Todo.MyUtil;
using ETool.Core.Util;

namespace ETool.Core.MyUtil
{
    /// <summary>
    /// 身份证号码工具类【中国大陆身份证号码：18位、15位】
    /// </summary>
    /// <remarks>参考标准：<see href="https://openstd.samr.gov.cn/bzgk/gb/newGbInfo?hcno=080D6FBF2BB468F9007657F26D60013E">《GB11643-1999》</see></remarks>
    public static class IdCardUtil
    {
        // 省份编码
        private static readonly HashSet<string> MainlandProvinceCodes = new HashSet<string>()
        {
            "11", "12", "13", "14", "15", // 华北
            "21", "22", "23", // 东北
            "31", "32", "33", "34", "35", "36", "37", // 华东
            "41", "42", "43", "44", "45", "46", // 华南
            "50", "51", "52", "53", "54", // 西南
            "61", "62", "63", "64", "65", // 西北
        };

        // 18位身份证有效省份编码（含大陆+港澳台）【注意：港澳台地区有自身独立的身份证件体系，大陆为便利港澳台居民在内地生活，推出了「港澳台居民居住证」（格式兼容身份证（18位+校验码））】
        private static readonly HashSet<string> Valid18IdProvinceCodes = new HashSet<string>(MainlandProvinceCodes)
        {
            "71", // 台湾
            "81", // 香港
            "82", // 澳门
        };

        // 15位身份证有效省份编码（仅大陆，无港澳台）
        private static readonly HashSet<string> Valid15IdProvinceCodes = MainlandProvinceCodes;

        /// <summary>
        /// 身份证前两位省级编码到中文名称的映射（仅中国大陆）
        /// </summary>
        private static readonly Dictionary<string, string> ProvinceCodeToName = new Dictionary<string, string>
        {
            // 华北
            { "11", "北京市" },
            { "12", "天津市" },
            { "13", "河北省" },
            { "14", "山西省" },
            { "15", "内蒙古自治区" },
            // 东北
            { "21", "辽宁省" },
            { "22", "吉林省" },
            { "23", "黑龙江省" },
            // 华东
            { "31", "上海市" },
            { "32", "江苏省" },
            { "33", "浙江省" },
            { "34", "安徽省" },
            { "35", "福建省" },
            { "36", "江西省" },
            { "37", "山东省" },
            // 华南
            { "41", "河南省" },
            { "42", "湖北省" },
            { "43", "湖南省" },
            { "44", "广东省" },
            { "45", "广西壮族自治区" },
            { "46", "海南省" },
            // 西南
            { "50", "重庆市" },
            { "51", "四川省" },
            { "52", "贵州省" },
            { "53", "云南省" },
            { "54", "西藏自治区" },

            // 西北
            { "61", "陕西省" },
            { "62", "甘肃省" },
            { "63", "青海省" },
            { "64", "宁夏回族自治区" },
            { "65", "新疆维吾尔自治区" },

            // 港澳台
            { "71", "台湾省" },
            { "81", "香港特别行政区" },
            { "82", "澳门特别行政区" }
        };

        // 加权因子（18位身份证前17位的加权系数）
        private static readonly int[] CheckWeights = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

        // 校验码映射表（余数0-10对应校验码）
        private static readonly char[] CheckCodes = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        /// <summary>
        /// 检验指定字符串是否符合中国 18 位身份证的格式规范
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <param name="ignoreCase">是否忽略大小写（默认忽略）</param>
        /// <returns>如果字符串符合返回 true，否则返回 false</returns>
        public static bool IsValidChinaIdCard18(string s, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(s) || s.Length != 18)
            {
                return false;
            }

            // 前17位必须是数字
            for (var i = 0; i < 17; i++)
            {
                if (s[i] < '0' || s[i] > '9')
                {
                    return false;
                }
            }

            // 第18位：数字或 X/x
            if (ignoreCase)
            {
                if (!(s[17] == 'x' || s[17] == 'X' || s[17] >= '0' && s[17] <= '9'))
                {
                    return false;
                }
            }
            else
            {
                if (!(s[17] == 'X' || s[17] >= '0' && s[17] <= '9'))
                {
                    return false;
                }
            }

            // 省份码校验
            if (!Valid18IdProvinceCodes.Contains(s.Substring(0, 2)))
            {
                return false;
            }

            // 出生日期格式校验
            if (!DateTime.TryParseExact(s.Substring(6, 8), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var datetime))
            {
                return false;
            }

            // 合理出生日期范围，这里有以下假定：
            //  1. 拥有 18 位身份证号码人群的出生日期从 1900-01-01 开始
            //  2. 办理 18 位身份证号码的最早时间是出生当天
            if (datetime < new DateTime(1900, 1, 1) || datetime > DateTime.Today)
            {
                return false;
            }

            // 校验码计算
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += (s[i] - '0') * CheckWeights[i];
            }

            return CheckCodes[sum % 11] == CharUtil.ToUpperLetter(s[17]);
        }

        /// <summary>
        /// 检验指定字符串是否符合中国 15 位身份证的格式规范
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <returns>如果字符串符合返回 true，否则返回 false</returns>
        public static bool IsValidChinaIdCard15(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length != 15)
            {
                return false;
            }

            // 必须是数字
            foreach (var c in s)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            // 省份码校验
            if (!Valid15IdProvinceCodes.Contains(s.Substring(0, 2)))
            {
                return false;
            }

            // 出生日期格式校验：假定拥有 15 位身份证号码的人的出生日期为：1900-01-01 到 1999-12-31 之间
            return DateTime.TryParseExact("19" + s.Substring(6, 6), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        /// <summary>
        /// 检验指定字符串是否符合中国身份证号码的格式规范【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">待校验的字符串</param>
        /// <param name="ignoreCase">是否忽略大小写（默认忽略）</param>
        /// <returns>如果字符串符合返回 true，否则返回 false</returns>
        /// <remarks>
        /// <para>1. 只校验格式的合法性，格式正确 ≠ 身份真实</para>
        /// <para>2. 校验逻辑包含：长度、数字格式、省份编码、出生日期、校验码</para>
        /// </remarks>
        public static bool IsValidChinaIdCard(string s, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            switch (s.Length)
            {
                case 18:
                    return IsValidChinaIdCard18(s, ignoreCase);
                case 15:
                    return IsValidChinaIdCard15(s);
                default:
                    return false;
            }
        }

        /// <summary>
        /// 将身份证号码转化为 15 位
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <returns>如果字符串符合身份证号码格式规范返回 15 位身份证号码，否则返回空</returns>
        /// <remarks>
        /// 注意：自从 2000 年出生的人无法使用 15 位身份证号码正确表示出生日期，这里仅做 “格式转换”
        /// </remarks>
        public static string ToIdCard15(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return "";
            }

            if (s.Length == 15)
            {
                return s;
            }

            return s.Substring(0, 6) + s.Substring(8, 9);
        }

        /// <summary>
        /// 将身份证号码转化为 18 位
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <returns>如果字符串符合身份证号码格式规范返回 18 位身份证号码，否则返回空</returns>
        public static string ToIdCard18(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return "";
            }

            if (s.Length == 18)
            {
                return s;
            }

            var idCard17 = s.Substring(0, 6) + "19" + s.Substring(6, 9);
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += (idCard17[i] - '0') * CheckWeights[i];
            }

            return idCard17 + CheckCodes[sum % 11];
        }

        /// <summary>
        /// 根据身份证号码获取对应的出生日期（年）【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>合法则返回出生日期（1900-9999），非法返回-1</returns>
        public static int GetBirthdayYear(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return -1;
            }

            if (s.Length == 18)
            {
                return (s[6] - '0') * 1000 +
                       (s[7] - '0') * 100 +
                       (s[8] - '0') * 10 +
                       (s[9] - '0');
            }

            return 1900 + (s[6] - '0') * 10 + (s[7] - '0');
        }

        /// <summary>
        /// 根据身份证号码获取对应的出生日期（月）【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>合法则返回出生日期（1-12），非法返回-1</returns>
        public static int GetBirthdayMonth(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return -1;
            }

            if (s.Length == 18)
            {
                return (s[10] - '0') * 10 + (s[11] - '0');
            }

            return (s[8] - '0') * 10 + (s[9] - '0');
        }

        /// <summary>
        /// 根据身份证号码获取对应的出生日期（日）【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>合法则返回出生日期（1-31），非法返回-1</returns>
        public static int GetBirthdayDay(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return -1;
            }

            if (s.Length == 18)
            {
                return (s[12] - '0') * 10 + (s[13] - '0');
            }

            return (s[10] - '0') * 10 + (s[11] - '0');
        }

        /// <summary>
        /// 根据身份证号码获取对应的出生日期（年月日）【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>合法则返回出生日期（yyyyMMdd），非法返回空</returns>
        public static string GetBirthday(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return "";
            }

            if (s.Length == 18)
            {
                return s.Substring(6, 8);
            }

            return "19" + s.Substring(6, 6);
        }

        /// <summary>
        /// 根据身份证号码获取对应的性别【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>男性：1，女性：0，非法：-1</returns>
        public static int GetGender(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return -1;
            }

            return (s[s.Length == 18 ? 16 : 14] - '0') % 2 == 1 ? 1 : 0;
        }

        /// <summary>
        /// 根据身份证号码获取对应的年龄【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>合法返回年龄（中国法定周岁），否则返回 -1</returns>
        /// <remarks>
        /// <para>1. 生日当天即满对应周岁</para>
        /// <para>2. 特殊场景：闰年2月29日出生者，平年以2月28日为生日（实务通用规则）</para>
        /// <para>3. 示例1：2005-01-01 出生，2006-01-01 为 1 周岁，2006-01-02 为 1 周岁</para>
        /// <para>4. 示例2：1996-02-29 出生，1997-02-28 为 1 周岁，1997-03-01 为 1 周岁</para>
        /// <para>5. 示例3：1996-02-29 出生，2000-02-28 为 3 周岁，2000-02-29 为 4 周岁，2000-03-01 为 4 周岁</para>
        /// </remarks>
        public static int GetAge(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return -1;
            }

            // 提取身份证中的出生年月日（已校验合法，范围：1900-01-01 至 当前日期）
            var birthYear = GetBirthdayYear(s);
            var birthMonth = GetBirthdayMonth(s);
            var birthDay = GetBirthdayDay(s);
            var today = DateTime.Today;

            // 本年出生：无论哪天出生，年龄均为0
            if (birthYear == today.Year)
            {
                return 0;
            }

            // 基础年龄 = 当前年 - 出生年（此时基础年龄≥1）
            var age = today.Year - birthYear;

            // 核心判断：生日是否已过
            bool isBirthdayPassed;
            // 场景1：出生日是闰年2月29日，且当前是平年的2月28日 → 视为生日已过
            if (birthMonth == 2 && birthDay == 29 && !DateTime.IsLeapYear(today.Year) && today.Month == 2 && today.Day == 28)
            {
                isBirthdayPassed = true;
            }
            // 场景2：普通日期判断（月份更大，或月份相同且日期≥生日）
            else
            {
                isBirthdayPassed = today.Month > birthMonth || (today.Month == birthMonth && today.Day >= birthDay);
            }

            // 生日未过，年龄减1
            if (!isBirthdayPassed)
            {
                age--;
            }

            return age;
        }

        /// <summary>
        /// 根据身份证号码获取对应的省级编码【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>合法则返回省级编码，非法返回空</returns>
        public static string GetProvinceCode(string s)
        {
            return !IsValidChinaIdCard(s) ? "" : s.Substring(0, 2);
        }

        /// <summary>
        /// 根据身份证号码获取对应的省级名称【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>合法则返回省级名称，非法返回空</returns>
        public static string GetProvinceName(string s)
        {
            return !IsValidChinaIdCard(s) ? "" : ProvinceCodeToName[s.Substring(0, 2)];
        }

        /// <summary>
        /// 身份证号码脱敏处理【兼容15、18位身份证号码】
        /// </summary>
        /// <param name="idCard">待脱敏的身份证号码字符串</param>
        /// <param name="start">起始索引</param>
        /// <param name="count">替换的数量</param>
        /// <param name="maskChar">用于替换的填充字符</param>
        /// <returns>脱敏后的字符串</returns>
        public static string Mask(string idCard, int start, int count, char maskChar = '*')
        {
            if (StrUtil.IsNull(idCard))
            {
                return "";
            }

            if (!IsValidChinaIdCard(idCard))
            {
                return idCard;
            }

            return StrUtil.FillChars(idCard, start, count, maskChar);
        }

        /// <summary>
        /// 根据身份证号码获取身份证信息对象 IdCardInfo
        /// </summary>
        /// <param name="s">身份证号码</param>
        /// <returns>身份证信息对象 IdCardInfo；若输入无效，返回 IsValid=false 的默认实例</returns>
        public static IdCardInfo GetIdCardInfo(string s)
        {
            if (!IsValidChinaIdCard(s))
            {
                return new IdCardInfo();
            }

            return new IdCardInfo
            {
                IdCard = s,
                IsValid = true,
                IdCard15 = ToIdCard15(s),
                IdCard18 = ToIdCard18(s),
                Birthday = GetBirthday(s),
                BirthdayYear = GetBirthdayYear(s),
                BirthdayMonth = GetBirthdayMonth(s),
                BirthdayDay = GetBirthdayDay(s),
                Gender = GetGender(s),
                Age = GetAge(s),
                ProvinceCode = GetProvinceCode(s),
                ProvinceName = GetProvinceName(s)
            };
        }

        /// <summary>
        /// 身份证信息封装类
        /// </summary>
        public class IdCardInfo
        {
            /// <summary>
            /// 身份证号码（原始输入）
            /// </summary>
            public string IdCard { get; set; } = "";

            /// <summary>
            /// 身份证号码（15位）
            /// </summary>
            public string IdCard15 { get; set; } = "";

            /// <summary>
            /// 身份证号码（18位）
            /// </summary>
            public string IdCard18 { get; set; } = "";

            /// <summary>
            /// 是否为合法身份证号码
            /// </summary>
            public bool IsValid { get; set; } = false;

            /// <summary>
            /// 出生年份（1900-9999），非法时为 -1
            /// </summary>
            public int BirthdayYear { get; set; } = -1;

            /// <summary>
            /// 出生月份（1-12），非法时为 -1
            /// </summary>
            public int BirthdayMonth { get; set; } = -1;

            /// <summary>
            /// 出生日（1-31），非法时为 -1
            /// </summary>
            public int BirthdayDay { get; set; } = -1;

            /// <summary>
            /// 完整出生日期字符串（yyyyMMdd），如 "19900101"；非法时为空
            /// </summary>
            public string Birthday { get; set; } = "";

            /// <summary>
            /// 性别：1=男，0=女，-1=未知/非法
            /// </summary>
            public int Gender { get; set; } = -1;

            /// <summary>
            /// 年龄：合法age>=0，非法age=-1
            /// </summary>
            public int Age { get; set; } = -1;

            /// <summary>
            /// 省级行政区编码（前两位），非法时为空
            /// </summary>
            public string ProvinceCode { get; set; } = "";

            /// <summary>
            /// 省级行政区名称，如 "北京市"；非法时为空
            /// </summary>
            public string ProvinceName { get; set; } = "";
        }
    }
}
