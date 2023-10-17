using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Lottery.WebMvc.Models
{
    public class LoginViewModel
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
