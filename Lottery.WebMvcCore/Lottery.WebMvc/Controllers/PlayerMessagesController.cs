using Lottery.DoMain.Constant;
using Lottery.DoMain.Enum;
using Lottery.DoMain.Extentions;
using Lottery.DoMain.Models;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography.Xml;

namespace Lottery.WebMvc.Controllers
{
    public class PlayerMessagesController : BaseController
    {
        public IActionResult MessagesByDay(int idPlayer, string namePlayer, int region, double cachTrungDaThang, double cachTrungDaXien, string strDateTime)
        {
            DateTime dateTime = Constant.ConvertStringToDateTime(strDateTime);
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
            messgeByDay.MessgeByDaySession = messgeByDaySession;
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
        public IActionResult ExecuteSyntaxPlayer(string calculation3Json)
        {
            try
            {
                var calculation3 = JsonConvert.DeserializeObject<Calculation3Model>(calculation3Json);
                var userData = GetCurrentUser();
                calculation3.UserID = userData.Id;
                var dataBase = provider.PostAsync<Cal3DetailDto>(ApiUri.POST_CalculationCal3, calculation3);
                if (dataBase == null || dataBase.Result == null || dataBase.Result.Data == null)
                {
                    return Json(Server_Error("Đã có lỗi xảy ra!"));
                }
                return Json(Success_Request(dataBase.Result.Data));
            }
            catch (Exception ex)
            {
                return Json(Server_Error("Đã có lỗi hệ thông!"));
            }
        }

        public IActionResult ShowMessagesDetail(string messgeByDaySessionModelJson, int iDMessage)
        {
            var messgeByDaySessionModel = JsonConvert.DeserializeObject<MessgeByDaySession>(messgeByDaySessionModelJson);
            DetailMessage detailMessageModel = new DetailMessage();
            var dataBase = provider.GetAsync<DetailMessage>(string.Format(ApiUri.Get_HandlMessage + "/{0}", iDMessage));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
            {
                detailMessageModel = dataBase.Result.Data;
            }

            Cal3DetailDto cal3DetailDtoModel = new Cal3DetailDto();
            cal3DetailDtoModel.Xac = detailMessageModel.Xac;
            cal3DetailDtoModel.Trung = detailMessageModel.Trung;
            cal3DetailDtoModel.TrungDetail = detailMessageModel.TrungDetail;
            cal3DetailDtoModel.Details = detailMessageModel.Details;
            cal3DetailDtoModel.Message = detailMessageModel.Message;

            var compositeModel = new Tuple<MessgeByDaySession, Cal3DetailDto>(messgeByDaySessionModel, cal3DetailDtoModel);
            return View(compositeModel);
        }

        public IActionResult GetPartialViewMessagesDetail(string cal3DetailDtoModelJson)
        {
            var cal3DetailDtoModel = JsonConvert.DeserializeObject<Cal3DetailDto>(cal3DetailDtoModelJson);
            return PartialView("_PartialViewMessagesDetail", cal3DetailDtoModel);
        }

        public IActionResult ListMessages(string messgeByDaySessionModelJson)
        {
            var messgeByDaySessionModel = JsonConvert.DeserializeObject<MessgeByDaySession>(messgeByDaySessionModelJson);
            if(messgeByDaySessionModel.HandlDate == null)
            {
                messgeByDaySessionModel.HandlDate = Constant.ConvertStringToDateTime(messgeByDaySessionModel.StrHandlDate);
            }    
            MessgeByDayModel messgeByDayModel = new MessgeByDayModel()
            {
                HandlDate = messgeByDaySessionModel.HandlDate,
                IDKhach = messgeByDaySessionModel.IdPlayer,
                Mien = messgeByDaySessionModel.Region
            };
            List<DetailMessage> listMessage = new List<DetailMessage>();
            var dataBase = provider.PostAsync<List<DetailMessage>>(ApiUri.POST_HandlMessagehandlMessage, messgeByDayModel);
            if (dataBase == null || dataBase.Result == null || dataBase.Result.Data == null)
            {
                return Json(Server_Error("Đã có lỗi xảy ra!"));
            }
            listMessage = dataBase.Result.Data;

            var compositeModel = new Tuple<MessgeByDaySession, List<DetailMessage>>(messgeByDaySessionModel, listMessage);
            return View(compositeModel);
        }

    }
}
