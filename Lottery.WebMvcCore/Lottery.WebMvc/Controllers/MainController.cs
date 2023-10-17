using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class MainController : BaseController
    {
        public IActionResult Menu()
        {
            return View();
        }
    }
}
