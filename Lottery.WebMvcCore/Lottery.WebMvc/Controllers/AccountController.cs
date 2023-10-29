using Lottery.DoMain.Constant;
using Lottery.DoMain.Models;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Lottery.WebMvc.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login()
        {
            var userData = GetCurrentUser();
            if (userData != null)
            {
                return RedirectToAction("Menu", "Main");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ExecuteLogin(string loginViewModelJson)
        {
            try
            {
                var loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(loginViewModelJson);
                var userBase = provider.PostAsync<User>(ApiUri.POST_UserLogin, loginViewModel);
                if (userBase == null || userBase.Result == null || userBase.Result.Data == null)
                {
                    return Json(Server_Error("Đã có lỗi xảy ra!"));
                }
                var userData = userBase.Result.Data;
                if (!userData.IsLoginSuccess)
                {
                    return Json(Server_Error("Tài khoản đăng nhập không đúng!"));
                }
                else
                {
                    ExecuteSaveCookies(userData);
                    return Json(Success_Request(true));
                }
            }
            catch (Exception ex)
            {
                return Json(Server_Error("Have error when login. Please check with our Administrator!"));
            }  
        }

        public IActionResult Logout()
        {
            RemoteCookies();
            return RedirectToAction("Login", "Account");
        }
    }
}
