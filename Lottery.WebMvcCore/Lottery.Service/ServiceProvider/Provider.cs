using Lottery.DoMain.Constant;
using Lottery.DoMain.FileLog;
using Lottery.DoMain.Models.BaseModel;
using Lottery.Service.ServiceProvider.Interface;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service.ServiceProvider
{
    public class Provider : IProvider
    {
        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                _disposedValue = true;
            }
        }

        private readonly JsonSerializerSettings _serializerSettings;
        private readonly string ApiEndPoint;

        public Provider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
            ApiEndPoint = AppConfigs.GetApiEndPoint();
        }
        public Task<ResponseBase<TResult>> GetAsync<TResult>(string uri, string token = "")
        {
            uri = ApiEndPoint + uri;
            try
            {
                Uri urlapi = new Uri(uri);
                using (var wc = new HttpClient())
                {
                    wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var jsonResult = wc.GetAsync($@"{urlapi}").Result.Content.ReadAsStringAsync().Result;
                    return Task.Run(() => JsonConvert.DeserializeObject<ResponseBase<TResult>>(jsonResult, _serializerSettings));
                }
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            return default;
        }
        public Task<ResponseBase<TResult>> PostAsync<TResult>(string uri, dynamic fromBody, string token = "")
        {
            uri = ApiEndPoint + uri;
            try
            {
                Uri urlapi = new Uri(uri);
                using (var wc = new HttpClient())
                {
                    wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var modelString = JsonConvert.SerializeObject(fromBody);
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    var jsonResult = wc.PostAsync($@"{urlapi}", content).Result.Content.ReadAsStringAsync().Result;
                    return Task.Run(() => JsonConvert.DeserializeObject<ResponseBase<TResult>>(jsonResult, _serializerSettings));
                }
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            return default;
        }

        public Task<ResponseBase<TResult>> PutAsync<TResult>(string uri, dynamic fromBody, string token = "")
        {
            uri = ApiEndPoint + uri;
            try
            {
                Uri urlapi = new Uri(uri);
                using (var wc = new HttpClient())
                {
                    wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var modelString = JsonConvert.SerializeObject(fromBody);
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    var jsonResult = wc.PutAsync($@"{urlapi}", content).Result.Content.ReadAsStringAsync().Result;
                    return Task.Run(() => JsonConvert.DeserializeObject<ResponseBase<TResult>>(jsonResult, _serializerSettings));
                }
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            return default;
        }

        public Task<ResponseBase<bool>> DeleteAsync(string uri, string token = "")
        {
            uri = ApiEndPoint + uri;
            try
            {
                Uri urlapi = new Uri(uri);
                using (var wc = new HttpClient())
                {
                    wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var jsonResult = wc.DeleteAsync($@"{urlapi}").Result.Content.ReadAsStringAsync().Result;
                    return Task.Run(() => JsonConvert.DeserializeObject<ResponseBase<bool>>(jsonResult, _serializerSettings));
                }
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            return default;
        }
    }
}
