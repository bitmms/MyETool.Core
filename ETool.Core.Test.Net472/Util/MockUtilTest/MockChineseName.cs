using System.Collections.Generic;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.MockUtilTest
{
    public class MockChineseName
    {
        private static readonly IReadOnlyList<string> FirstChineseNameList = new List<string>
        {
            "王", "李", "张", "刘", "陈", "杨", "赵", "黄", "周", "吴", "徐", "孙", "胡", "朱", "高", "林", "何", "郭", "马", "罗",
            "梁", "宋", "郑", "谢", "韩", "唐", "冯", "于", "董", "萧", "程", "曹", "袁", "邓", "许", "傅", "沈", "曾", "彭", "吕",
            "苏", "卢", "蒋", "蔡", "贾", "丁", "魏", "薛", "叶", "阎", "余", "潘", "杜", "戴", "夏", "钟", "汪", "田", "任", "姜",
            "范", "方", "石", "姚", "谭", "廖", "邹", "熊", "金", "陆", "郝", "孔", "白", "崔", "康", "毛", "邱", "秦", "江", "史",
            "顾", "侯", "邵", "孟", "龙", "万", "段", "雷", "钱", "汤", "常", "武", "乔", "贺", "赖", "龚", "文", "樊", "兰", "殷",
        };

        private static readonly IReadOnlyList<string> LastChineseNameList = new List<string>
        {
            "伟", "强", "勇", "军", "峰", "磊", "涛", "杰", "鹏", "辉", "明", "浩", "宇", "轩", "泽", "辰", "航", "瑞", "博", "超",
            "毅", "恒", "俊", "楠", "彬", "诚", "康", "健", "鑫", "阳", "婷", "娜", "丽", "娟", "芳", "萍", "敏", "静", "燕", "玲",
            "钦", "庆", "秋", "泉", "然", "仁", "荣", "锐", "润", "森", "祺", "启", "盛", "书", "舒", "帅", "烁", "朔", "松", "嵩",
            "菊", "欣", "悦", "馨", "妍", "雅", "萱", "雨", "晴", "梦", "甜", "思", "婉", "柔", "淑", "惠", "钰", "瑾", "蓉", "薇",
            "曼", "涵", "汐", "玥", "凝", "珞", "瑜", "恬", "沐", "屿", "柠", "安", "诺", "芮", "晨", "希", "然", "易", "亦", "伊"
        };

        [Fact]
        public void MockChineseName_Should_Return_Valid_Name()
        {
            for (int i = 0; i < 10000; i++)
            {
                var name = MockUtil.MockChineseName();

                Assert.False(string.IsNullOrWhiteSpace(name));
                Assert.True(name.Length == 2 || name.Length == 3, $"生成结果长度非法：{name}");

                // 第一个字必须是姓
                var first = name.Substring(0, 1);
                Assert.Contains(first, FirstChineseNameList);

                // 后面的字必须来自名字列表
                for (int j = 1; j < name.Length; j++)
                {
                    var part = name.Substring(j, 1);
                    Assert.Contains(part, LastChineseNameList);
                }
            }
        }

        [Fact]
        public void MockChineseName_Should_Generate_Different_Values()
        {
            var set = new HashSet<string>();

            for (int i = 0; i < 100; i++)
            {
                set.Add(MockUtil.MockChineseName());
            }

            Assert.True(set.Count > 1, "多次生成结果完全相同，随机逻辑可能有问题。");
        }

        [Fact]
        public void MockChineseName_Should_Generate_Both_2_And_3_Char_Names()
        {
            bool hasTwoCharName = false;
            bool hasThreeCharName = false;

            for (int i = 0; i < 5000; i++)
            {
                var name = MockUtil.MockChineseName();

                if (name.Length == 2) hasTwoCharName = true;
                if (name.Length == 3) hasThreeCharName = true;

                if (hasTwoCharName && hasThreeCharName)
                {
                    break;
                }
            }

            Assert.True(hasTwoCharName, "长时间生成后仍未出现 2 字姓名。");
            Assert.True(hasThreeCharName, "长时间生成后仍未出现 3 字姓名。");
        }
    }
}
