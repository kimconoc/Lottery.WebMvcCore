﻿using Lottery.DoMain.Constant;
using Lottery.DoMain.Models;
using Lottery.Service.ServiceProvider;
using Lottery.Service.ServiceProvider.Interface;
using Lottery.WebMvc.MemCached.Interface;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlTypes;

namespace Lottery.WebMvc.Controllers
{
    public class AdministratorController : BaseController
    {
        public AdministratorController(IProvider provider, IMemCached memCached) : base(provider, memCached)
        {
        }

        public IActionResult UserListing()
        {
            List<UserManagement> users = new List<UserManagement>();
            var dataBase = _provider.GetAsync<List<UserManagement>>(string.Format(ApiUri.GET_AdminListing));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
            {
                users = dataBase.Result.Data;
            }
            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExecuteAddUser(string userManagementJson)
        {
            try
            {
                var userManagementModel = JsonConvert.DeserializeObject<UserManagementModel>(userManagementJson);
                userManagementModel.ExpireDate = Constant.ConvertStringToDateTime(userManagementModel.StrExpireDate);
                var dataBase = _provider.PostAsync<Object>(ApiUri.POST_AdminAdd, userManagementModel);
                if (dataBase == null || dataBase.Result == null || !dataBase.Result.IsSuccessful)
                {
                    return Json(Server_Error("Đã có lỗi xảy ra!"));
                }
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            catch (Exception ex)
            {
                return Json(Server_Error("Đã có lỗi hệ thông!"));
            }
        }

        public IActionResult ExtendExpireDate(int userId, string name, string account, DateTime expireDate)
        {
            UserManagement userManagement = new UserManagement()
            {
                Id = userId,
                Name = name,
                Account = account,
                ExpireDate = expireDate
            };
            return View(userManagement);
        }

        [HttpPost]
        public IActionResult ExecuteExtendExpireDate(int userId, string strExtendExpireDate)
        {
            try
            {
                ExtendExpireDateModel extendExpireDateModel = new ExtendExpireDateModel()
                {
                    UserId = userId,
                    NewExpireDate = Constant.ConvertStringToDateTime(strExtendExpireDate),

                };

                var dataBase = _provider.PostAsync<Object>(ApiUri.POST_AdminRenew, extendExpireDateModel);
                if (dataBase == null || dataBase.Result == null || !dataBase.Result.IsSuccessful)
                {
                    return Json(Server_Error("Đã có lỗi xảy ra!"));
                }
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            catch (Exception ex)
            {
                return Json(Server_Error("Đã có lỗi hệ thông!"));
            }
        }

        public IActionResult ChangePassword(int userId, string name, string account)
        {
            UserManagement userManagement = new UserManagement()
            {
                Id = userId,
                Name = name,
                Account = account,
            };
            return View(userManagement);
        }

        public IActionResult UpdateUser(int userId, string name, string account, string note)
        {
            UserManagement userManagement = new UserManagement()
            {
                Id = userId,
                Name = name,
                Account = account,
                Note = note,
            };
            return View(userManagement);
        }

        [HttpPost]
        public IActionResult ExecuteChangePassword(int userId, string newPass)
        {
            try
            {
                NewPassModel newPassModel = new NewPassModel()
                {
                    UserId = userId,
                    NewPass = newPass,

                };

                var dataBase = _provider.PostAsync<Object>(ApiUri.POST_AdminChangePass, newPassModel);
                if (dataBase == null || dataBase.Result == null || !dataBase.Result.IsSuccessful)
                {
                    return Json(Server_Error("Đã có lỗi xảy ra!"));
                }
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            catch (Exception ex)
            {
                return Json(Server_Error("Đã có lỗi hệ thông!"));
            }
        }

        [HttpPost]
        public IActionResult ExecuteUpdateUser(int userId, string newNote)
        {
            if (newNote == null)
                newNote = "";
            try
            {
                UserUpdateModel userUpdateModel = new UserUpdateModel()
                {
                    UserId = userId,
                    Note = newNote,

                };

                var dataBase = _provider.PostAsync<Object>(ApiUri.POST_AdminUpdate, userUpdateModel);
                if (dataBase == null || dataBase.Result == null || !dataBase.Result.IsSuccessful)
                {
                    return Json(Server_Error("Đã có lỗi xảy ra!"));
                }
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            catch (Exception ex)
            {
                return Json(Server_Error("Đã có lỗi hệ thông!"));
            }
        }

        [HttpPost]
        public IActionResult ExecuteRefreshImeiUser(int userId)
        {
            var dataBase = _provider.GetAsync<object>(string.Format(ApiUri.POST_AdminReset + "/{0}", userId));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.IsSuccessful)
            {
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            return View(Server_Error());
        }

        [HttpPost]
        public IActionResult ExecuteDeleteUser(int userId)
        {
            var dataBase = _provider.DeleteAsync(string.Format(ApiUri.DELETE_Admin + "/{0}", userId));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.IsSuccessful)
            {
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            return View(Server_Error());
        }
    }
}
