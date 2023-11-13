using Lottery.Service.ServiceProvider.Interface;
using Lottery.WebMvc.MemCached.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class AdministratorController : BaseController
    {
        public AdministratorController(IProvider provider, IMemCached memCached) : base(provider, memCached)
        {
        }

        public IActionResult UserListing()
        {
            return View();
        }
    }
}
