using Lottery.DoMain.Constant;
using Lottery.DoMain.Enum;
using Lottery.DoMain.Models;
using Lottery.Service.ServiceProvider;
using Lottery.Service.ServiceProvider.Interface;
using Lottery.WebMvc.MemCached.Interface;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace Lottery.WebMvc.Controllers
{
    public class BaseController : Controller, IAsyncActionFilter
    {
        /// <summary>Quản lý tài khoản con: admin hệ thống hoặc tài khoản cấp quản lý (Parent = 0).</summary>
        protected static bool CanManageStaffAccounts(User? user) =>
            user != null && (user.IsAdmin || user.Parent == 0);

        private static bool IsRestrictedAdministratorPath(PathString path)
        {
            var p = path.Value ?? string.Empty;
            if (p.Length == 0) return false;
            foreach (var seg in RestrictedAdministratorPathSegments)
            {
                if (p.Equals(seg, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>Không gồm ExecuteChangePassword — route đó dùng cho Account/ChangePass (đổi mật khẩu bản thân).</summary>
        private static readonly string[] RestrictedAdministratorPathSegments =
        {
            "/Administrator/UserListing",
            "/Administrator/AgentListing",
            "/Administrator/AddUser",
            "/Administrator/ExtendExpireDate",
            "/Administrator/ChangePassword",
            "/Administrator/UpdateUser",
            "/Administrator/ExecuteAddUser",
            "/Administrator/ExecuteExtendExpireDate",
            "/Administrator/ExecuteUpdateUser",
            "/Administrator/ExecuteDeleteUser",
            "/Administrator/ExecuteRefreshImeiUser",
        };

        //protected IProvider provider = new Provider();
        protected readonly IProvider _provider;
        protected readonly IMemCached _memCached;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _memCached.Dispose();
                _provider.Dispose();// = null;
                //((IDisposable)provider).Dispose();
            }
            base.Dispose(disposing);
        }
        public BaseController(IProvider provider, IMemCached memCached)
        {
            _provider = provider;
            _memCached = memCached;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userData = _memCached.GetCurrentUser();
            var controller = context.Controller as Controller;
            if ((controller != null && userData == null && !context.HttpContext.Request.Path.Equals("/Account/Login") && !context.HttpContext.Request.Path.Equals("/Account/ExecuteLogin"))
                || (userData != null && !CanManageStaffAccounts(userData) && IsRestrictedAdministratorPath(context.HttpContext.Request.Path)))
            {
                if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    // Xử lý chuyển hướng Ajax bằng cách trả về một phản hồi JSON
                    context.Result = new JsonResult(new { redirectTo = "/Account/Login" });
                }
                else
                {
                    // Xử lý chuyển hướng thông thường
                    context.Result = new RedirectResult("/Account/Login");
                }
                return;
            }

            // Thực thi action
            var resultContext = await next();
            // Đoạn code sau khi action được thực thi
        }

        protected void ExecuteSaveDateSession(DateTime date)
        {
            string jsonDate = JsonConvert.SerializeObject(date);
            HttpContext.Session.SetString(Default.Get_Signin_Date, jsonDate);
        }

        protected DateTime GetDateSession()
        {
            DateTime date = Constant.ConvertStringToDateTime();
            try
            {
                string jsonValue = HttpContext.Session.GetString(Default.Get_Signin_Date);
                if (!string.IsNullOrEmpty(jsonValue))
                {
                    date = JsonConvert.DeserializeObject<DateTime>(jsonValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return date;
        }

        public void RemoveSavedDateSession()
        {
            HttpContext.Session.Remove(Default.Get_Signin_Date);
        }

        protected DataResponse<TRequest> Success_Request<TRequest>(TRequest data)
        {
            return new DataResponse<TRequest>()
            {
                Data = data,
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Thành công"
            };
        }

        protected StatusResponse Success_Request()
        {
            return new StatusResponse()
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Thành công"
            };
        }

        protected StatusResponse Not_Found(string message = "")
        {
            return new StatusResponse()
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = string.IsNullOrEmpty(message) ? "Không tìm thấy dữ liệu" : message
            };
        }

        protected StatusResponse Bad_Request(string message = "")
        {
            return new StatusResponse()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = string.IsNullOrEmpty(message) ? "Dữ liệu định dạng sai" : message
            };
        }

        protected StatusResponse Server_Error(string message = "")
        {
            return new StatusResponse()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = string.IsNullOrEmpty(message) ? "Có lỗi trong quá trình xử lý" : message
            };
        }
    }

}
