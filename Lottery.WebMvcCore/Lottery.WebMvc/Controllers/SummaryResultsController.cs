using Lottery.DoMain.Constant;
using Lottery.DoMain.Models;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Lottery.WebMvc.Controllers
{
    public class SummaryResultsController : BaseController
    {
        public IActionResult SummaryByDay(string strDateTime)
        {
            DateTime dateTime = Constant.ConvertStringToDateTime(strDateTime);
            CountByDayModel countByDayModel = new CountByDayModel()
            {
                HandlDate = dateTime,
                UserID = GetCurrentUser().Id,
            };
            List<CountByDay> listCountByDay = new List<CountByDay>();
            var dataBase = provider.PostAsync<List<CountByDay>>(ApiUri.POST_HandlMessageCountByDay, countByDayModel);
            if (dataBase == null || dataBase.Result == null || dataBase.Result.Data == null)
            {
                return Json(Server_Error("Đã có lỗi xảy ra!"));
            }
            listCountByDay = dataBase.Result.Data;
            Dictionary<DateTime, List<CountByDay>> dic = new Dictionary<DateTime, List<CountByDay>>();
            dic.Add(dateTime, listCountByDay);

            return View(dic);
        }

        public IActionResult SummaryManyDay(string strFromDate ,string strToDate)
        {
            DateTime fromDate = Constant.ConvertStringToDateTime(strFromDate);
            DateTime toDate = Constant.ConvertStringToDateTime(strToDate);
            CountManyDayModel countManyDayModel = new CountManyDayModel()
            {
                FromDate = fromDate,
                ToDate = toDate,
                UserID = GetCurrentUser().Id,
            };

            var dataBase = provider.PostAsync<Dictionary<DateTime, List<CountByDay>>>(ApiUri.POST_HandlMessageCountManyDay, countManyDayModel);
            if (dataBase == null || dataBase.Result == null || dataBase.Result.Data == null)
            {
                return Json(Server_Error("Đã có lỗi xảy ra!"));
            }

            var compositeModel = new Tuple<Dictionary<DateTime, List<CountByDay>>, CountManyDayModel>(dataBase.Result.Data, countManyDayModel);
            return View(compositeModel);
        }

        public IActionResult GetPartialViewSummary(string dicSummaryModelJson)
        {
            var dicSummaryModel = JsonConvert.DeserializeObject<Dictionary<DateTime, List<CountByDay>>>(dicSummaryModelJson);
            return PartialView("_PartialViewSummary", dicSummaryModel);
        }
    }
}
