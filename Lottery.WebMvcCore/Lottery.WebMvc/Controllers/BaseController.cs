using Lottery.Service.ServiceProvider;
using Lottery.Service.ServiceProvider.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebMvc.Controllers
{
    public class BaseController : Controller
    {
        protected IProvider provider = new Provider();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                provider.Dispose();// = null;
                //((IDisposable)provider).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
