using Lottery.DoMain.Constant;
using Lottery.DoMain.Models;
using Lottery.Service.ServiceProvider.Interface;
using Lottery.WebMvc.MemCached.Interface;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Formats.Asn1.AsnWriter;

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

        [HttpPost]
        public IActionResult UpdateResultsDay(string date)
        {
            try
            {
                bool result = false;
                var dataBase = _provider.GetAsync<bool>(string.Format(ApiUri.Get_UpdateDay + "?dayte={0}", date));
                if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
                {
                    result = dataBase.Result.Data;
                }
                return Json(Success_Request(result));
            }
            catch 
            {
                return Json(Server_Error("Hệ thống đang xảy ra lỗi!"));
            }
        }

    }
}
