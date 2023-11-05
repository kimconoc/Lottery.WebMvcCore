﻿using Lottery.DoMain.Constant;
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
            CountManyDayModel countManyDayModel = new CountManyDayModel()
            {
                FromDate = dateTime,
                ToDate = null,
                UserID = GetCurrentUser().Id,
            };
            var dataBase = provider.PostAsync<List<CountByDay>>(ApiUri.POST_HandlMessageCountByDay, countByDayModel);
            if (dataBase == null || dataBase.Result == null || dataBase.Result.Data == null)
            {
                return Json(Server_Error("Đã có lỗi xảy ra!"));
            }

            var compositeModel = new Tuple<List<CountByDay>, CountManyDayModel>(dataBase.Result.Data, countManyDayModel);
            return View(compositeModel);
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

            var dataBase = provider.PostAsync<List<CountByDay>>(ApiUri.POST_HandlMessageCountManyDay, countManyDayModel);
            if (dataBase == null || dataBase.Result == null || dataBase.Result.Data == null)
            {
                return Json(Server_Error("Đã có lỗi xảy ra!"));
            }

            var compositeModel = new Tuple<List<CountByDay>, CountManyDayModel>(dataBase.Result.Data, countManyDayModel);
            return View(compositeModel);
        }

        public IActionResult GetPartialViewSummary(string summaryModelJson, string countManyDayModelJson)
        {
            var summaryModel = JsonConvert.DeserializeObject<List<CountByDay>>(summaryModelJson);
            var countManyDayModel = JsonConvert.DeserializeObject<CountManyDayModel>(countManyDayModelJson);
            var compositeModel = new Tuple<List<CountByDay>, CountManyDayModel>(summaryModel, countManyDayModel);
            return PartialView("_PartialViewSummary", compositeModel);
        }

        [HttpPost]
        public IActionResult CheckFromDateToDate(string strFromDate, string strToDate)
        {
            DateTime fromDate = Constant.ConvertStringToDateTime(strFromDate);
            DateTime toDate = Constant.ConvertStringToDateTime(strToDate);
            if(fromDate.Date > toDate.Date)
            {
                return Json(Bad_Request("Thời gian không hợp lệ."));
            }    
            return Json(Success_Request(true));
        }
    }
}
