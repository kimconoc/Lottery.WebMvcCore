using Lottery.DoMain.Models;
using Lottery.Service.ServiceProvider;
using Lottery.Service.ServiceProvider.Interface;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Lottery.WebMvc.Controllers
{
    public class BaseController : Controller
    {
        protected IProvider provider = new Provider();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                provider.Dispose();// = null;
                //((IDisposable)provider).Dispose();
            }
            base.Dispose(disposing);
        }
        protected void ExecuteSaveCookies(User userData)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1),
                HttpOnly = true,
            };
            string jsonUserData = JsonConvert.SerializeObject(userData);
            Response.Cookies.Append(GetSigninToken(), jsonUserData, cookieOptions);
        }
        protected void RemoteCookies()
        {
            string value = string.Empty;
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append(GetSigninToken(), value, options);
        }
        protected string GetSigninToken()
        {
            return "LotteryCookie";
        }
        public User GetCurrentUser()
        {
            User user = null;
            try
            {
                string cookieValue = Request.Cookies[GetSigninToken()];
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    user = JsonConvert.DeserializeObject<User>(cookieValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
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
