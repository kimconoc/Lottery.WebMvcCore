using Lottery.DoMain.Constant;
using Lottery.DoMain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lottery.WebMvc.MemCached
{
    public class MemCached
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //public MemCached(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}
        public static User GetCurrentUser(HttpContext httpContext)
        {
            var httpRequest = httpContext.Request;
            var cookies = httpRequest.Cookies[Default.Get_Signin_Token];
            return new User();
        }
    }
}
