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
            RefreshAdminAllowCalInSession();
            return View();
        }

        [HttpGet]
        public IActionResult Settings()
        {
            var current = _memCached.GetCurrentUser();
            if (current == null || !current.IsAdmin)
            {
                return RedirectToAction("Login", "Account");
            }
            RefreshAdminAllowCalInSession();
            current = _memCached.GetCurrentUser();
            ViewBag.IsAllowCal = current?.IsAllowCal ?? true;
            ViewBag.LayoutCenterTitle = "Thiết lập";
            return View();
        }

        [HttpPost]
        public IActionResult UpdateAllowCal(bool isAllowCal)
        {
            try
            {
                var current = _memCached.GetCurrentUser();
                if (current == null || !current.IsAdmin)
                {
                    return Json(Bad_Request("Bạn không có quyền thực hiện thao tác này."));
                }
                var dataBase = _provider.PostAsync<bool>(ApiUri.POST_AdminAllowCal, new { UserId = current.Id, IsAllowCal = isAllowCal });
                if (dataBase?.Result == null || !dataBase.Result.IsSuccessful)
                {
                    return Json(Bad_Request(dataBase?.Result?.Message ?? "Cập nhật thất bại."));
                }
                current.IsAllowCal = dataBase.Result.Data;
                _memCached.ExecuteSaveData(current);
                return Json(Success_Request(dataBase.Result.Data));
            }
            catch
            {
                return Json(Server_Error("Hệ thống đang xảy ra lỗi!"));
            }
        }

        private void RefreshAdminAllowCalInSession()
        {
            var current = _memCached.GetCurrentUser();
            if (current == null || !current.IsAdmin)
            {
                return;
            }
            var dataBase = _provider.GetAsync<bool>(string.Format(ApiUri.GET_AdminAllowCal, current.Id));
            if (dataBase?.Result != null && dataBase.Result.IsSuccessful)
            {
                current.IsAllowCal = dataBase.Result.Data;
                _memCached.ExecuteSaveData(current);
            }
        }

        [HttpGet]
        public IActionResult UpdateResultsDay()
        {
            var current = _memCached.GetCurrentUser();
            if (current == null || !current.IsAdmin)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.LayoutCenterTitle = "Cập nhật kết quả";
            return View();
        }

        [HttpPost]
        public IActionResult UpdateAllWinning(string date, int region)
        {
            try
            {
                var current = _memCached.GetCurrentUser();
                if (current == null || !current.IsAdmin)
                {
                    return Json(Bad_Request("Bạn không có quyền thực hiện thao tác này."));
                }
                DateTime handlDate = Constant.ConvertStringToDateTime(date);
                bool result = false;
                var dataBase = _provider.PostAsync<bool>(ApiUri.POST_HandlMessageUpdateAll, new { HandlDate = handlDate, Mien = region });
                if (dataBase != null && dataBase.Result != null)
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

        [HttpPost]
        public IActionResult UpdateResultsDay(string date, int dai)
        {
            try
            {
                var current = _memCached.GetCurrentUser();
                if (current == null || !current.IsAdmin)
                {
                    return Json(Bad_Request("Bạn không có quyền thực hiện thao tác này."));
                }
                bool result = false;
                var dataBase = _provider.GetAsync<bool>(string.Format(ApiUri.Get_UpdateDay + "?date={0}&dai={1}", date, dai));
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
