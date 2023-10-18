using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class MainController : BaseController
    {
        public IActionResult Menu()
        {
            var userData = GetCurrentUser();
            if (userData != null)
            {
                
            }
            return View();
        }
    }
}
