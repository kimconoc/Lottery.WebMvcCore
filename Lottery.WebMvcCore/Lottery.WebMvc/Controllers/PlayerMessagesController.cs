using Lottery.DoMain.Constant;
using Lottery.DoMain.Enum;
using Lottery.DoMain.Extentions;
using Lottery.DoMain.Models;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace Lottery.WebMvc.Controllers
{
    public class PlayerMessagesController : BaseController
    {
        public IActionResult MessagesByDay(int idPlayer, int region, string strDateTime)
        {
            string format = "dd.MM.yyyy";
            DateTime dateTime;
            if (string.IsNullOrEmpty(strDateTime))
            {
                dateTime = DateTime.Now;
            }
            else
            {
                if (!DateTime.TryParseExact(strDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {

                }
            }          
            ViewBag.Region = region;
            MessgeByDayModel messgeByDayModel = new MessgeByDayModel()
            {
                HandlDate = dateTime,//DateTime.Parse("2023-10-18T10:46:41.434Z"),
                IDKhach = idPlayer,
                Mien = region
            };
            var messgeByDayBase = provider.PostAsync<MessgeByDay>(ApiUri.POST_HandlMessagemessageByDay, messgeByDayModel);
            if (messgeByDayBase == null || messgeByDayBase.Result == null || messgeByDayBase.Result.Data == null)
            {
                return Json(Server_Error("Đã có lỗi xảy ra!"));
            }
            var messgeByDay = messgeByDayBase.Result.Data;
            messgeByDay.HandlDate = messgeByDayModel.HandlDate;
            messgeByDay.IdPlayer = idPlayer;
            return View(messgeByDay);
        }

    }
}
