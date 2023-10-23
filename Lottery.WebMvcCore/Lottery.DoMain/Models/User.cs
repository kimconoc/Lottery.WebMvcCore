using Lottery.DoMain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Models
{
    public class User : VBaseModel<int>
    {
        public string Account { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool isLoginSuccess { get; set; }
        public int UserAgent { get; set; }
        public double TileXac { get; set; }
        public double TileThuong { get; set; }
        public double DaThang { get; set; }
        public double TileBaSo { get; set; }
        public double DaXien { get; set; }
        public double BonSo { get; set; }
        public List<PhonebookUser> Phonebooks { get; set; }
    }
    public class PhonebookUser
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsChu { get; set; }
    }
}
