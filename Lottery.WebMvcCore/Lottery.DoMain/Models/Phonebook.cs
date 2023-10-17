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
        public double TileXac { get; set; }
        public double TileThuong { get; set; }
        public double TileBaSo { get; set; }
        public double BonSo { get; set; }
        public double DaThang { get; set; }
        public double DaXien { get; set; }
        //
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsChu { get; set; }
        public double NCoBaoHaiCon { get; set; }
        public int NTrungBaoHaiCon { get; set; }
        public double NCoHaiConDD { get; set; }
        public int NTrungHaiConDD { get; set; }
        public double NCoDaThang { get; set; }
        public int NTrungDaThang { get; set; }
        public int NCachTrungDaThang { get; set; }
        public double NCoDaXien { get; set; }
        public int NTrungDaXien { get; set; }
        public int NCachTrungDaXien { get; set; }
        public double NCoBaCon { get; set; }
        public int NTrungBaCon { get; set; }
        public double NCoBonCon { get; set; }
        public int NTrungBonCon { get; set; }
        public int NPhanTramTong { get; set; }
        public double TCoBaoHaiCon { get; set; }
        public int TTrungBaoHaiCon { get; set; }
        public double TCoHaiConDD { get; set; }
        public int TTrungHaiConDD { get; set; }
        public double TCoDaThang { get; set; }
        public int TTrungDaThang { get; set; }
        public int TCachTrungDaThang { get; set; }
        public double TCoDaXien { get; set; }
        public int TTrungDaXien { get; set; }
        public int TCachTrungDaXien { get; set; }
        public double TCoBaCon { get; set; }
        public int TTrungBaCon { get; set; }
        public double TCoBonCon { get; set; }
        public int TTrungBonCon { get; set; }
        public int TPhanTramTong { get; set; }
        public double BCoBaoHaiCon { get; set; }
        public int BTrungBaoHaiCon { get; set; }
        public double BCoHaiConDD { get; set; }
        public int BTrungHaiConDD { get; set; }
        public double BCoDaThang { get; set; }
        public int BTrungDaThang { get; set; }
        public int BCachTrungDaThang { get; set; }
        public double BCoBaCon { get; set; }
        public int BTrungBaCon { get; set; }
        public double BCoBonCon { get; set; }
        public int BTrungBonCon { get; set; }
        public int BPhanTramTong { get; set; }
        public double BCoXienHai { get; set; }
        public int BTrungXienHai { get; set; }
        public int BCoXienBa { get; set; }
        public int BTrungXienBa { get; set; }
        public int BCoXienBon { get; set; }
        public int BTrungXienBon { get; set; }
    }
}
