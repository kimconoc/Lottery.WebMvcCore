﻿using Lottery.DoMain.Constant;
using Lottery.DoMain.Enum;
using Lottery.DoMain.Models;
using Lottery.WebMvc.MemCached.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Lottery.WebMvc.MemCached
{
    public class MemCached : IMemCached
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemCached(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void ExecuteSaveData(User userData ,bool isSaveCookies)
        {
            userData.UserAgent = IdentifyUserAgent();
            if (isSaveCookies)
            {
                ExecuteSaveCookies(userData);
            }   
            else
            {
                ExecuteSaveSession(userData);
            }    
        }
        public void RemoveSavedData()
        {
            string value = string.Empty;
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(GetSigninToken(), value, options);
            _httpContextAccessor.HttpContext.Session.Remove(GetSigninToken());
        }

        public User GetCurrentUser()
        {
            User user = null;
            try
            {
                string jsonValue = _httpContextAccessor.HttpContext.Request.Cookies[GetSigninToken()];
                if (!string.IsNullOrEmpty(jsonValue))
                {
                    user = JsonConvert.DeserializeObject<User>(jsonValue);
                }
                else
                {
                    jsonValue = _httpContextAccessor.HttpContext.Session.GetString(GetSigninToken());
                    if (!string.IsNullOrEmpty(jsonValue))
                    {
                        user = JsonConvert.DeserializeObject<User>(jsonValue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        private void ExecuteSaveCookies(User userData)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1),
                HttpOnly = true,
            };
            string jsonUserData = JsonConvert.SerializeObject(userData);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(GetSigninToken(), jsonUserData, cookieOptions);
        }
        private void ExecuteSaveSession(User userData)
        {
            string jsonUserData = JsonConvert.SerializeObject(userData);
            _httpContextAccessor.HttpContext.Session.SetString(GetSigninToken(), jsonUserData);
        }

        private string GetSigninToken()
        {
            return Default.Get_Signin_Token;
        }

        private int IdentifyUserAgent()
        {
            string userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString().ToLower();
            if (userAgent.Contains("iphone") && userAgent.Contains("mobile"))
            {
                return (int)UserAgentEnum.MobileIphone;
            }
            else if (userAgent.Contains("android") && userAgent.Contains("mobile"))
            {
                return (int)UserAgentEnum.MobileAndroid;
            }
            else if (userAgent.Contains("windows"))
            {
                return (int)UserAgentEnum.ComputerWindows;
            }
            else if ((userAgent.Contains("ipad") && userAgent.Contains("mac os")) || (userAgent.Contains("macintosh") && userAgent.Contains("mac os")))
            {
                return (int)UserAgentEnum.IpadOs;
            }
            else
            {
                return (int)UserAgentEnum.UnknownDevice;
            }
        }
    }
}
