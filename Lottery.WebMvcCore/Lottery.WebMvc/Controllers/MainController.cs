using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Menu()
        {
            return View();
        }
    }
}
