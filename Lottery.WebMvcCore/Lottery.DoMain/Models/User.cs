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
        public bool IsLoginSuccess { get; set; }
        public int UserAgent { get; set; }
        public bool IsAdmin { get; set; }
    }
}
