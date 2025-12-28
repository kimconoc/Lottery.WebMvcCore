using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Models
{
    public class CountDetailByDay
    {
        public MienNam MienNam { get; set; }
        public MienTrung MienTrung { get; set; }
        public MienBac MienBac { get; set; }
        public double Tong { get; set; }
        public double NoCu { get; set; }
        public double Total { get; set; }
    }

    public class MienBac
    {
        public string StrThu { get; set; }
        public double ThucThu { get; set; }
        public string StrTrung { get; set; }
        public double Cai { get; set; }
    }

    public class MienNam
    {
        public string StrThu { get; set; }
        public double ThucThu { get; set; }
        public string StrTrung { get; set; }
        public double Cai { get; set; }
    }

    public class MienTrung
    {
        public string StrThu { get; set; }
        public double ThucThu { get; set; }
        public string StrTrung { get; set; }
        public double Cai { get; set; }
    }
}
