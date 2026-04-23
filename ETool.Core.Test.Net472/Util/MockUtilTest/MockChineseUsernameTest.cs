using System.Collections.Generic;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.MockUtilTest
{
    public class MockChineseUsernameTest
    {
        private static readonly IReadOnlyList<string> FirstUsernameList = new List<string>
        {
            "迷人", "美丽", "巨大", "可爱", "狡猾", "坚定", "有活力", "极好", "快速", "不错", "明亮", "干净", "帅气", "稳固", "特别", "整洁",
            "华丽", "伟大", "英俊", "炽热", "善良", "诚实", "战略性", "神秘", "开心", "耐心", "漂亮", "富有", "秘密", "聪明", "强大", "智慧"
        };

        private static readonly IReadOnlyList<string> LastUsernameList = new List<string>
        {
            "苹果", "鳄梨", "香蕉", "黑莓", "蓝莓", "胡萝卜", "樱桃", "椰子", "葡萄", "柠檬", "莴苣", "芒果", "梨",
            "洋葱", "橙子", "木瓜", "桃子", "菠萝", "覆盆子", "土豆", "南瓜", "草莓", "番茄", "甜瓜", "蘑菇", "西兰花"
        };

        [Fact]
        public void MockChineseUsername_Should_Return_Valid_Format()
        {
            for (var i = 0; i < 10000; i++)
            {
                var username = MockUtil.MockChineseUsername();

                Assert.False(string.IsNullOrWhiteSpace(username));

                var parts = username.Split('的');
                Assert.Equal(2, parts.Length);
                Assert.Contains(parts[0], FirstUsernameList);
                Assert.Contains(parts[1], LastUsernameList);
            }
        }

        [Fact]
        public void MockChineseUsername_Should_Generate_Multiple_Different_Values()
        {
            var resultSet = new HashSet<string>();

            for (var i = 0; i < 100; i++)
            {
                resultSet.Add(MockUtil.MockChineseUsername());
            }

            Assert.True(resultSet.Count > 1, "多次生成的用户名完全相同，疑似随机逻辑异常");
        }
    }
}
