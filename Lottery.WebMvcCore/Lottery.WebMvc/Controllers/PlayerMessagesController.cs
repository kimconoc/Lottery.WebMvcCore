using Lottery.DoMain.Constant;
using Lottery.DoMain.Enum;
using Lottery.DoMain.Extentions;
using Lottery.DoMain.Models;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lottery.WebMvc.Controllers
{
    public class PlayerMessagesController : BaseController
    {
        public IActionResult MessagesByDay(int IdPlayer, int region)
        {
            ViewBag.Region = region;
            MessgeByDayModel messgeByDayModel = new MessgeByDayModel()
            {
                HandlDate = DateTime.Parse("2023-10-18T10:46:41.434Z"),
                IDKhach = IdPlayer,
                Mien = region
            };
            var messgeByDayBase = provider.PostAsync<MessgeByDay>(ApiUri.POST_HandlMessagemessageByDay, messgeByDayModel);
            if (messgeByDayBase == null || messgeByDayBase.Result == null || messgeByDayBase.Result.Data == null)
            {
                return Json(Server_Error("Đã có lỗi xảy ra!"));
            }
            var messgeByDay = messgeByDayBase.Result.Data;
            messgeByDay.HandlDate = messgeByDayModel.HandlDate;
            return View(messgeByDayBase.Result.Data);
        }

    }
}
