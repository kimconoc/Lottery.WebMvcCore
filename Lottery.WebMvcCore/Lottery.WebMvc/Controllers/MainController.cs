using Lottery.Service.ServiceProvider.Interface;
using Lottery.WebMvc.MemCached.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class MainController : BaseController
    {
        public MainController(IProvider provider, IMemCached memCached) : base(provider, memCached)
        {
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}
