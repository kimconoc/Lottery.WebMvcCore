using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class PhonebookController : BaseController
    {
        public IActionResult ListPlayer()
        {
            return View();
        }
        public IActionResult CreatePlayer()
        {
            return View();
        }
    }
}
