using Lottery.DoMain.Constant;
using Lottery.DoMain.Models;
using Lottery.Service.ServiceProvider;
using Lottery.Service.ServiceProvider.Interface;
using Lottery.WebMvc.MemCached.Interface;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlTypes;

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

        [HttpPost]
        public IActionResult ExecuteAddUser(string userManagementJson)
        {
            try
            {
                var userManagementModel = JsonConvert.DeserializeObject<UserManagementModel>(userManagementJson);
                userManagementModel.ExpireDate = Constant.ConvertStringToDateTime(userManagementModel.StrExpireDate);
                var dataBase = _provider.PostAsync<Object>(ApiUri.POST_AdminAdd, userManagementModel);
                if (dataBase == null || dataBase.Result == null || !dataBase.Result.IsSuccessful)
                {
                    return Json(Server_Error("Đã có lỗi xảy ra!"));
                }
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            catch (Exception ex)
            {
                return Json(Server_Error("Đã có lỗi hệ thông!"));
            }
        }

        [HttpPost]
        public IActionResult ExecuteDeleteUser(int userId)
        {
            var dataBase = _provider.DeleteAsync(string.Format(ApiUri.DELETE_Admin + "/{0}", userId));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.IsSuccessful)
            {
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            return View(Server_Error());
        }
    }
}
