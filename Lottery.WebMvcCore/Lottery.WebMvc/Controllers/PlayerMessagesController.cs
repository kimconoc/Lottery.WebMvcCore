using Lottery.DoMain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class PlayerMessagesController : BaseController
    {
        public IActionResult MessagesByDay(string region)
        {
            ViewBag.Region = region;
            return View();
        }
    }
}
