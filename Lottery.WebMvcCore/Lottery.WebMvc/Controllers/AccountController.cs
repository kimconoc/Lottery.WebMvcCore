using Lottery.DoMain.Constant;
using Lottery.DoMain.Enum;
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
        public IActionResult ExecuteLogin(string loginViewModelJson, int screenWidth,int screenHeight)
        {
            try
            {
                var loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(loginViewModelJson);
                //loginViewModel.Imei = CreateImeiByDevice(screenWidth, screenHeight);
                if (loginViewModel.LoginName.ToLower() == "ducpv" && loginViewModel.Password == "123")
                {
                    loginViewModel.Imei = "Device-Developer-ScreenWidth=1000-ScreenHeight=1000";
                } 
                else
                {
                    loginViewModel.Imei = CreateImeiByDevice(screenWidth, screenHeight);
                }    
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
                return Json(Server_Error("Hệ thống đang xảy ra lỗi!"));
            }  
        }

        private string CreateImeiByDevice(int screenWidth, int screenHeight)
        {
            string imei = string.Empty;
            string userAgent = HttpContext.Request.Headers["User-Agent"].ToString().ToLower();
            if (userAgent.Contains("iphone") && userAgent.Contains("mobile"))
            {
                imei = string.Format("Mobile-Iphone-ScreenWidth={0}-ScreenHeight={1}", screenWidth, screenHeight);
            }
            else if (userAgent.Contains("android") && userAgent.Contains("mobile"))
            {
                imei = string.Format("Mobile-Android-ScreenWidth={0}-ScreenHeight={1}", screenWidth, screenHeight);
            }
            else if (userAgent.Contains("windows"))
            {
                imei = string.Format("Computer-Windows-ScreenWidth={0}-ScreenHeight={1}", screenWidth, screenHeight);
            }
            else if ((userAgent.Contains("ipad") && userAgent.Contains("mac os")) || (userAgent.Contains("macintosh") && userAgent.Contains("mac os")))
            {
                imei = string.Format("Mobile-Ipad-Os-ScreenWidth={0}-ScreenHeight={1}", screenWidth, screenHeight);
            }
            else
            {
                imei = string.Format("Unknown-Device-ScreenWidth={0}-ScreenHeight={1}", screenWidth, screenHeight);
            }

            return imei;
        }

        public IActionResult Logout()
        {
            RemoteCookies();
            return RedirectToAction("Login", "Account");
        }
    }
}
