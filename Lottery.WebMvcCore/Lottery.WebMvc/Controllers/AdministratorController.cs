using Lottery.DoMain.Constant;
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
        private static string SanitizeAccountManageReturnUrl(string? returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return Default.AdministratorAccount_Return_UserListing;
            }
            var t = returnUrl.Trim();
            if (t.StartsWith(Default.AdministratorAccount_Return_AgentListing, StringComparison.OrdinalIgnoreCase)
                && t.IndexOf("://", StringComparison.Ordinal) < 0)
            {
                return Default.AdministratorAccount_Return_AgentListing;
            }
            return Default.AdministratorAccount_Return_UserListing;
        }

        private static string AccountManageHeaderTitleForReturnUrl(string safeReturnUrl) =>
            safeReturnUrl.Equals(Default.AdministratorAccount_Return_AgentListing, StringComparison.OrdinalIgnoreCase)
                ? Default.AdministratorAccount_Header_AgentListingContext
                : Default.AdministratorAccount_Header_UserListingContext;

        private void SetAccountManageFlowViewData(string? returnUrl)
        {
            var safe = SanitizeAccountManageReturnUrl(returnUrl);
            ViewBag.AccountManageReturnUrl = safe;
            ViewBag.AccountManageHeaderTitle = AccountManageHeaderTitleForReturnUrl(safe);
        }

        public AdministratorController(IProvider provider, IMemCached memCached) : base(provider, memCached)
        {
        }

        public IActionResult UserListing()
        {
            List<UserManagement> users = new List<UserManagement>();
            var current = _memCached.GetCurrentUser();
            if (current == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var dataBase = _provider.GetAsync<List<UserManagement>>(string.Format(ApiUri.GET_AdminListing, current.Id));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
            {
                users = dataBase.Result.Data;
            }
            ViewBag.LayoutCenterTitle = Default.AdministratorAccount_Header_UserListingContext;
            return View(users);
        }

        public IActionResult AgentListing()
        {
            var current = _memCached.GetCurrentUser();
            if (current == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!current.IsAdmin)
            {
                return RedirectToAction("Menu", "Main");
            }
            List<UserManagement> users = new List<UserManagement>();
            var dataBase = _provider.GetAsync<List<UserManagement>>(string.Format(ApiUri.GET_AdminListingAgents, current.Id));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
            {
                users = dataBase.Result.Data;
            }
            ViewBag.LayoutCenterTitle = Default.AdministratorAccount_Header_AgentListingContext;
            return View(users);
        }

        /// <summary>Thông báo (log) theo đại lý — chỉ admin.</summary>
        public IActionResult AgentUserLogs(int agentUserId)
        {
            var current = _memCached.GetCurrentUser();
            if (current == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!current.IsAdmin)
            {
                return RedirectToAction("Menu", "Main");
            }
            List<UserAgentLogItem> logs = new List<UserAgentLogItem>();
            var dataBase = _provider.GetAsync<List<UserAgentLogItem>>(string.Format(ApiUri.GET_CommonAgentLogs, agentUserId));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
            {
                logs = dataBase.Result.Data;
            }
            ViewBag.AgentUserId = agentUserId;
            ViewData["HideLayoutTopBar"] = true;
            return View(logs);
        }

        /// <summary>Danh sách tài khoản con do đại lý quản lý — giống UserListing nhưng owner = đại lý.</summary>
        public IActionResult AgentManagedUsers(int ownerUserId)
        {
            var current = _memCached.GetCurrentUser();
            if (current == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!current.IsAdmin)
            {
                return RedirectToAction("Menu", "Main");
            }
            List<UserManagement> users = new List<UserManagement>();
            var dataBase = _provider.GetAsync<List<UserManagement>>(string.Format(ApiUri.GET_AdminListing, ownerUserId));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
            {
                users = dataBase.Result.Data;
            }
            ViewBag.OwnerUserId = ownerUserId;
            ViewData["HideLayoutTopBar"] = true;
            return View(users);
        }

        [HttpPost]
        public IActionResult ExecuteDeleteLogs([FromBody] DeleteLogsRequestModel request)
        {
            var current = _memCached.GetCurrentUser();
            if (current == null)
            {
                return Json(Server_Error("Phiên đăng nhập không hợp lệ."));
            }
            if (!current.IsAdmin)
            {
                return Json(Server_Error("Không có quyền."));
            }
            if (request?.Ids == null || request.Ids.Count == 0)
            {
                return Json(Bad_Request("Chưa chọn log để xóa."));
            }
            var dataBase = _provider.PostAsync<object>(ApiUri.POST_CommonDeleteLogs, new { Ids = request.Ids });
            if (dataBase == null || dataBase.Result == null || !dataBase.Result.IsSuccessful)
            {
                var msg = dataBase?.Result?.Message;
                return Json(Server_Error(string.IsNullOrEmpty(msg) ? "Đã có lỗi xảy ra!" : msg));
            }
            return Json(Success_Request(dataBase.Result.IsSuccessful));
        }

        public IActionResult AddUser()
        {
            var current = _memCached.GetCurrentUser();
            ViewBag.ShowQuanLyCheckbox = current?.IsAdmin == true;
            return View();
        }

        [HttpPost]
        public IActionResult ExecuteAddUser(string userManagementJson)
        {
            try
            {
                var userManagementModel = JsonConvert.DeserializeObject<UserManagementModel>(userManagementJson);
                var currentUser = _memCached.GetCurrentUser();
                if (currentUser == null)
                {
                    return Json(Server_Error("Phiên đăng nhập không hợp lệ."));
                }
                userManagementModel.ExpireDate = Constant.ConvertStringToDateTime(userManagementModel.StrExpireDate);
                if (currentUser.IsAdmin)
                {
                    userManagementModel.Parent = userManagementModel.IsQuanLy ? 0 : currentUser.Id;
                    userManagementModel.IsAdmin = true;
                }
                else
                {
                    userManagementModel.Parent = currentUser.Id;
                    userManagementModel.IsAdmin = false;
                }
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

        public IActionResult ExtendExpireDate(int userId, string name, string account, DateTime expireDate, int parent, string? returnUrl = null)
        {
            SetAccountManageFlowViewData(returnUrl);
            UserManagement userManagement = new UserManagement()
            {
                Id = userId,
                Name = name,
                Account = account,
                ExpireDate = expireDate,
                Parent = parent
            };
            return View(userManagement);
        }

        [HttpPost]
        public IActionResult ExecuteExtendExpireDate(int userId, string strExtendExpireDate, int parent)
        {
            try
            {
                var current = _memCached.GetCurrentUser();
                ExtendExpireDateModel extendExpireDateModel = new ExtendExpireDateModel()
                {
                    UserId = userId,
                    NewExpireDate = Constant.ConvertStringToDateTime(strExtendExpireDate),
                    IsAdmin = current.IsAdmin,
                    Parent = parent,
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

        public IActionResult ChangePassword(int userId, string name, string account, string? returnUrl = null)
        {
            SetAccountManageFlowViewData(returnUrl);
            UserManagement userManagement = new UserManagement()
            {
                Id = userId,
                Name = name,
                Account = account,
            };
            return View(userManagement);
        }

        public IActionResult UpdateUser(int userId, string name, string account, string note, string? returnUrl = null)
        {
            SetAccountManageFlowViewData(returnUrl);
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
        public IActionResult ExecuteDeleteUser(int userId, int parent)
        {
            var current = _memCached.GetCurrentUser();
            var dataBase = _provider.DeleteAsync(string.Format(ApiUri.DELETE_Admin + "/{0}/{1}/{2}", userId, parent, current.IsAdmin));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.IsSuccessful)
            {
                return Json(Success_Request(dataBase.Result.IsSuccessful));
            }
            return View(Server_Error());
        }
    }
}
