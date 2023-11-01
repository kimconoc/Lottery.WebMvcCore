using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Models
{
    public class MessgeByDay
    {
        public List<DetailMessage> DetailMessage { get; set; }
        public Total Total { get; set; }
        public string Message { get; set; }
        public bool IsThu { get; set; }
        public MessgeByDaySession MessgeByDaySession { get; set; }
    }
    public class MessgeByDaySession
    {
        public int IdPlayer { get; set; }
        public string NamePlayer { get; set; }
        public int Region { get; set; }
        public double CachTrungDaThang { get; set; }
        public double CachTrungDaXien { get; set; }
        public DateTime? HandlDate { get; set; }
    }
    public class DetailMessage
    {
        public int ID { get; set; }
        public Summary Xac { get; set; }
        public Summary Trung { get; set; }
        public List<string> TrungDetail { get; set; }
        public List<Detail> Details { get; set; }
        public bool IsTinh { get; set; }
        public string Message { get; set; }
        public DateTime HanldDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Total
    {
        public Summary Xac { get; set; }
        public Summary Trung { get; set; }
        public QuaCo QuaCo { get; set; }
    }

    public class QuaCo
    {
        public double HaiCon { get; set; }
        public double BaCon { get; set; }
        public double BonCon { get; set; }
    }

}
