using Lottery.DoMain.Constant;
using Lottery.DoMain.Models;
using Lottery.Service.ServiceProvider;
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
            List<UserManagement> users = new List<UserManagement>();
            var dataBase = _provider.GetAsync<List<UserManagement>>(string.Format(ApiUri.GET_AdminListing));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
            {
                users = dataBase.Result.Data;
            }
            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }
    }
}
