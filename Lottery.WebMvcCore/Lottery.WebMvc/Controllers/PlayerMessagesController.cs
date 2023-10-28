using Lottery.DoMain.Constant;
using Lottery.DoMain.Enum;
using Lottery.DoMain.Extentions;
using Lottery.DoMain.Models;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Lottery.WebMvc.Controllers
{
    public class PlayerMessagesController : BaseController
    {
        public IActionResult MessagesByDay(int idPlayer, string namePlayer, int region, double cachTrungDaThang, double cachTrungDaXien, DateTime? dateTime)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }  
            MessgeByDayModel messgeByDayModel = new MessgeByDayModel()
            {
                HandlDate = dateTime,
                IDKhach = idPlayer,
                Mien = region
            };
            var messgeByDayBase = provider.PostAsync<MessgeByDay>(ApiUri.POST_HandlMessagemessageByDay, messgeByDayModel);
            if (messgeByDayBase == null || messgeByDayBase.Result == null || messgeByDayBase.Result.Data == null)
            {
                return Json(Server_Error("Đã có lỗi xảy ra!"));
            }
            MessgeByDaySession messgeByDaySession = new MessgeByDaySession()
            {
                IdPlayer = idPlayer,
                NamePlayer = namePlayer,
                Region = region,
                CachTrungDaThang = cachTrungDaThang,
                CachTrungDaXien = cachTrungDaXien,
                HandlDate = dateTime,
            };
            var messgeByDay = messgeByDayBase.Result.Data;
            messgeByDay.MessgeByDayData = messgeByDaySession;
            return View(messgeByDay);
        }

        public IActionResult AddPlayerMessages(int idPlayer, string namePlayer, int region, double cachTrungDaThang, double cachTrungDaXien, DateTime dateTime)
        {
            MessgeByDaySession messgeByDaySession = new MessgeByDaySession()
            {
                IdPlayer = idPlayer,
                NamePlayer = namePlayer,
                Region = region,
                CachTrungDaThang = cachTrungDaThang,
                CachTrungDaXien = cachTrungDaXien,
                HandlDate = dateTime,
            };
            return View(messgeByDaySession);
        }

        [HttpPost]
        public IActionResult CharacterFilteringSyntax(string calculation3Json)
        {
            try
            {
                var calculation3 = JsonConvert.DeserializeObject<Calculation3Model>(calculation3Json);
                var userData = GetCurrentUser();
                calculation3.UserID = userData.Id;
                var dataBase = provider.PostAsync<object>(ApiUri.POST_CalculationCal3, calculation3);
                if (dataBase == null || dataBase.Result == null || dataBase.Result.Data == null)
                {
                    return Json(Server_Error("Đã có lỗi xảy ra!"));
                }
                return Json(Success_Request(true));
            }
            catch (Exception ex)
            {
                return Json(Server_Error("Đã có lỗi hệ thông!"));
            }
        }
    }
}
