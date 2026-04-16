using System.Collections.Generic;

namespace ETool.Core.Todo.MyUtil
{
    /// <summary>
    /// 模拟数据工具类
    /// </summary>
    public static class MockUtil
    {
        #region 生成模拟的 11 位手机号码

        /// <summary>
        /// 生成模拟的 11 位手机号码
        /// </summary>
        /// <returns>模拟生成的 11 位手机号码</returns>
        public static string MockPhoneNumber()
        {
            char[] resultChars = new char[11];

            resultChars[0] = '1';
            resultChars[1] = Core.MyUtil.RandomUtil.GetRandomDigitChar(3, 9);
            for (int i = 2; i < 11; i++)
            {
                resultChars[i] = Core.MyUtil.RandomUtil.GetRandomDigitChar(0, 9);
            }

            return new string(resultChars);
        }

        #endregion

        #region 生成模拟的中文用户名

        private static readonly List<string> FirstUsernameList = new List<string>
        {
            "迷人", "美丽", "巨大", "可爱", "狡猾", "坚定", "有活力", "极好", "快速", "不错", "明亮", "干净", "帅气", "稳固", "特别", "整洁",
            "华丽", "伟大", "英俊", "炽热", "善良", "诚实", "战略性", "神秘", "开心", "耐心", "漂亮", "富有", "秘密", "聪明", "强大", "智慧"
        };

        private static readonly List<string> LastUsernameList = new List<string>
        {
            "苹果", "鳄梨", "香蕉", "黑莓", "蓝莓", "胡萝卜", "樱桃", "椰子", "葡萄", "柠檬", "莴苣", "芒果", "梨",
            "洋葱", "橙子", "木瓜", "桃子", "菠萝", "覆盆子", "土豆", "南瓜", "草莓", "番茄", "甜瓜", "蘑菇", "西兰花"
        };

        /// <summary>
        /// 生成模拟的中文用户名
        /// </summary>
        /// <returns>模拟生成的中文用户名</returns>
        public static string MockChineseUsername()
        {
            int idx1 = Core.MyUtil.RandomUtil.GetRandomInt(0, FirstUsernameList.Count);
            int idx2 = Core.MyUtil.RandomUtil.GetRandomInt(0, LastUsernameList.Count);
            return $"{FirstUsernameList[idx1]}的{LastUsernameList[idx2]}";
        }

        #endregion

        #region 生成模拟的中文姓名

        private static readonly List<string> FirstChineseNameList = new List<string>
        {
            "王", "李", "张", "刘", "陈", "杨", "赵", "黄", "周", "吴", "徐", "孙", "胡", "朱", "高", "林", "何", "郭", "马", "罗",
            "梁", "宋", "郑", "谢", "韩", "唐", "冯", "于", "董", "萧", "程", "曹", "袁", "邓", "许", "傅", "沈", "曾", "彭", "吕",
            "苏", "卢", "蒋", "蔡", "贾", "丁", "魏", "薛", "叶", "阎", "余", "潘", "杜", "戴", "夏", "钟", "汪", "田", "任", "姜",
            "范", "方", "石", "姚", "谭", "廖", "邹", "熊", "金", "陆", "郝", "孔", "白", "崔", "康", "毛", "邱", "秦", "江", "史",
            "顾", "侯", "邵", "孟", "龙", "万", "段", "雷", "钱", "汤", "常", "武", "乔", "贺", "赖", "龚", "文", "樊", "兰", "殷",
        };

        private static readonly List<string> LastChineseNameList = new List<string>
        {
            "伟", "强", "勇", "军", "峰", "磊", "涛", "杰", "鹏", "辉", "明", "浩", "宇", "轩", "泽", "辰", "航", "瑞", "博", "超",
            "毅", "恒", "俊", "楠", "彬", "诚", "康", "健", "鑫", "阳", "婷", "娜", "丽", "娟", "芳", "萍", "敏", "静", "燕", "玲",
            "钦", "庆", "秋", "泉", "然", "仁", "荣", "锐", "润", "森", "祺", "启", "盛", "书", "舒", "帅", "烁", "朔", "松", "嵩",
            "菊", "欣", "悦", "馨", "妍", "雅", "萱", "雨", "晴", "梦", "甜", "思", "婉", "柔", "淑", "惠", "钰", "瑾", "蓉", "薇",
            "曼", "涵", "汐", "玥", "凝", "珞", "瑜", "恬", "沐", "屿", "柠", "安", "诺", "芮", "晨", "希", "然", "易", "亦", "伊"
        };

        /// <summary>
        /// 生成模拟的中文姓名
        /// </summary>
        /// <returns>模拟生成的中文姓名</returns>
        public static string MockChineseName()
        {
            string first = FirstChineseNameList[Core.MyUtil.RandomUtil.GetRandomInt(0, FirstChineseNameList.Count)];
            string middle = LastChineseNameList[Core.MyUtil.RandomUtil.GetRandomInt(0, LastChineseNameList.Count)];
            string last = Core.MyUtil.RandomUtil.GetRandomBool() ? "" : LastChineseNameList[Core.MyUtil.RandomUtil.GetRandomInt(0, LastChineseNameList.Count)];
            return first + middle + last;
        }

        #endregion
    }
}
