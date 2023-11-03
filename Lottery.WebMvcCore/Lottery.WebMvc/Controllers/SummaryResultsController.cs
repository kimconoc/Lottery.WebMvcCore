using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class SummaryResultsController : BaseController
    {
        public IActionResult SummaryByDay(string strDateTime)
        {
            return View();
        }
    }
}
