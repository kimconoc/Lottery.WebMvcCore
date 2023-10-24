using Lottery.DoMain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Models
{
    public class Phonebook : VBaseModel<int>
    {
        public bool IsCopy { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsChu { get; set; }
        public double NCoBaoHaiCon { get; set; }
        public double NTrungBaoHaiCon { get; set; }
        public double NCoHaiConDD { get; set; }
        public double NTrungHaiConDD { get; set; }
        public double NCoDaThang { get; set; }
        public double NTrungDaThang { get; set; }
        public int NCachTrungDaThang { get; set; }
        public double NCoDaXien { get; set; }
        public double NTrungDaXien { get; set; }
        public int NCachTrungDaXien { get; set; }
        public double NCoBaCon { get; set; }
        public double NTrungBaCon { get; set; }
        public double NCoBonCon { get; set; }
        public double NTrungBonCon { get; set; }
        public double NPhanTramTong { get; set; }
        public double TCoBaoHaiCon { get; set; }
        public double TTrungBaoHaiCon { get; set; }
        public double TCoHaiConDD { get; set; }
        public double TTrungHaiConDD { get; set; }
        public double TCoDaThang { get; set; }
        public double TTrungDaThang { get; set; }
        public int TCachTrungDaThang { get; set; }
        public double TCoDaXien { get; set; }
        public double TTrungDaXien { get; set; }
        public int TCachTrungDaXien { get; set; }
        public double TCoBaCon { get; set; }
        public double TTrungBaCon { get; set; }
        public double TCoBonCon { get; set; }
        public double TTrungBonCon { get; set; }
        public double TPhanTramTong { get; set; }
        public double BCoBaoHaiCon { get; set; }
        public double BTrungBaoHaiCon { get; set; }
        public double BCoHaiConDD { get; set; }
        public double BTrungHaiConDD { get; set; }
        public double BCoDaThang { get; set; }
        public double BTrungDaThang { get; set; }
        public int BCachTrungDaThang { get; set; }
        public double BCoBaCon { get; set; }
        public double BTrungBaCon { get; set; }
        public double BCoBonCon { get; set; }
        public double BTrungBonCon { get; set; }
        public double BPhanTramTong { get; set; }
        public double? BCoXienHai { get; set; }
        public double? BTrungXienHai { get; set; }
        public double? BCoXienBa { get; set; }
        public double? BTrungXienBa { get; set; }
        public double? BCoXienBon { get; set; }
        public double? BTrungXienBon { get; set; }
    }
}
