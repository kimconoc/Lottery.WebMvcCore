using Lottery.DoMain.Constant;
using Lottery.DoMain.Models;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Lottery.WebMvc.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login()
        {
            var userBase = provider.PostAsync<User>(ApiUri.POST_UserLogin, new LoginViewModel { LoginName = "phuongpv",Password = "123"});
            if (userBase == null || userBase.Result == null || userBase.Result.Data == null)
            {
                ViewBag.Message = "Đã có lỗi xảy ra!";
                return View();
            }
            var userData = userBase.Result.Data;
            return View();
        }
    }
}
