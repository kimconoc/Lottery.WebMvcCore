using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Enum
{
    public enum LotteryEnum
    {
        [Description("Bao lô")]
        Lo = 0,
        [Description("Đá xiên")]
        Xien = 1,
        [Description("Đầu")]
        LoDau = 2,
        [Description("Đuôi")]
        LoDuoi = 3,
        [Description("Đầu đuôi")]
        LoDauDuoi = 4,
        [Description("Bao ba con")]
        BaoBaCang = 5,
        [Description("Ba con đầu")]
        BaCangDau = 6,
        [Description("Ba con đuôi")]
        BaCangDuoi = 7,
        [Description("Xỉu chủ")]
        BaCangDauDuoi = 8
    }

    public enum UserAgentEnum
    {
        [Description("Iphone")]
        Iphone = 0,
        [Description("Android")]
        Android = 1,
        [Description("Computer")]
        Computer = 2
    }
}
