namespace Lottery.WebMvc.Models
{
    public class Calculation3Model
    {
        public int? IdMessage { get; set; }
        public int UserID { get; set; }
        public int IdKhach { get; set; }
        public int Mien { get; set; }
        public DateTime HandlByDate { get; set; }
        public double CachTrungDaThang { get; set; }
        public double CachTrungDaXien { get; set; }
        public bool IsSave { get; set; }
        public string SynTax { get; set; }
    }
}
