using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Models
{
    public class Cal3Filter
    {
        public string Sys { get; set; }
        public Error Error { get; set; }
    }
    public class Cal3DetailDto
    {
        public Summary Xac { get; set; }
        public Summary Trung { get; set; }
        public List<string> TrungDetail { get; set; }
        public List<Detail> Details { get; set; }
        public bool IsTinh { get; set; }
        public string Message { get; set; }
        public Error Error { get; set; }
    }
    public class Error
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string Message { get; set; }
    }
    public class Detail
    {
        public string Dai { get; set; }
        public List<int> DaiIn { get; set; }
        public List<string> So { get; set; }
        public List<int> SoIn { get; set; }
        public int CachChoi { get; set; }
        public double SoTien { get; set; }
        public double SlTrung { get; set; }
        public int SlBaoLo { get; set; }
    }
    public class Summary
    {
        public double HaiCB { get; set; }
        public double HaiCD { get; set; }
        public double DaT { get; set; }
        public double DaX { get; set; }
        public double BaCon { get; set; }
        public double XiuChu { get; set; }
        public double BonCon { get; set; }
    }
}
