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
                if (!userData.isLoginSuccess)
                {
                    return Json(Server_Error("Tài khoản đăng nhập không đúng!"));
                }
                else
                {
                    ExecuteSaveCookies(userData);
                    return RedirectToAction("Main", "Menu");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Have error when login. Please check with our Administrator!";
            }

            return Json(Server_Error());
        }

        private void ExecuteSaveCookies(User userData)
        {
            
        }
        private void RemoteCookies()
        {
            
        }
    }
}
