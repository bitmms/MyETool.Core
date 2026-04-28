using System.Collections.Generic;
using System.Linq;
using ETool.Core.Util;
using Xunit;

namespace ETool.Core.Test.Net472.Util.MockUtilTest
{
    public class MockPhoneNumberTest
    {
        [Fact]
        public void Test()
        {
            for (var i = 1; i < 10_0000; i++)
            {
                Assert.True(ValidatorUtil.IsPhoneNumber(MockUtil.MockPhoneNumber()));
            }
        }

        [Fact]
        public void MockPhoneNumber_Should_Match_Format_Rules()
        {
            // 安排 (Arrange) & 执行 (Act)
            // 循环 10000 次，确保随机数发生器覆盖各种边界情况
            for (var i = 0; i < 10000; i++)
            {
                var phoneNumber = MockUtil.MockPhoneNumber(); // 替换为你的类名

                // 断言 (Assert)

                // 1. 长度必须是 11 位
                Assert.Equal(11, phoneNumber.Length);

                // 2. 第一位必须是 '1'
                Assert.StartsWith("1", phoneNumber);

                // 3. 必须全部是数字
                foreach (var t in phoneNumber)
                {
                    Assert.True(ValidatorUtil.IsDigit(t), $"生成的手机号包含非数字字符: {phoneNumber}");
                }

                // 4. 第二位必须在 '3' 到 '9' 之间
                var secondChar = phoneNumber[1];
                Assert.True(secondChar >= '3' && secondChar <= '9', $"第二位数字超出了 3-9 的范围: {phoneNumber}");
            }
        }

        [Fact]
        public void MockPhoneNumber_Should_Generate_Random_Results()
        {
            // 安排 (Arrange)
            var generatedNumbers = new HashSet<string>();
            int generateCount = 100;

            // 执行 (Act)
            for (int i = 0; i < generateCount; i++)
            {
                generatedNumbers.Add(MockUtil.MockPhoneNumber());
            }

            // 断言 (Assert)
            // 验证 100 次生成中，去重后的数量大于 1。
            // 如果每次生成的号码都一模一样，说明随机逻辑有严重缺陷（比如随机种子被固定了）。
            // 正常情况下 100 次生成的号码几乎不可能出现重复，这里用 > 50 是为了极其保守地防止极低概率的测试用例闪断(Flaky Test)。
            Assert.True(generatedNumbers.Count > 50, "生成的手机号不够随机，存在大量重复");
        }
    }
}
