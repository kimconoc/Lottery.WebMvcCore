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
                || (userData != null && !userData.IsAdmin && (context.HttpContext.Request.Path.Equals("/Administrator/UserListing") || context.HttpContext.Request.Path.Equals("/Administrator/AddUser") || context.HttpContext.Request.Path.Equals("/Administrator/ExtendExpireDate") || context.HttpContext.Request.Path.Equals("/Administrator/ChangePassword"))))
            {
                context.Result = new RedirectResult("/Account/Login");
                return;
            }

            // Thực thi action
            var resultContext = await next();
            // Đoạn code sau khi action được thực thi
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
