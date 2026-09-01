using Lottery.DoMain.Constant;
using Lottery.DoMain.FileLog;
using Lottery.DoMain.Models.BaseModel;
using Lottery.Service.ServiceProvider.Interface;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
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

        // Reuse one HttpClient — creating new HttpClient per call exhausts sockets and
        // makes the site hang until the process is restarted.
        private static readonly HttpClient SharedHttpClient = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(100) // giữ timeout mặc định cũ của HttpClient
            };
            return client;
        }

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
                using (var request = new HttpRequestMessage(HttpMethod.Get, urlapi))
                {
                    if (!string.IsNullOrEmpty(token))
                        request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                    var response = SharedHttpClient.SendAsync(request).GetAwaiter().GetResult();
                    var jsonResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return Task.FromResult(JsonConvert.DeserializeObject<ResponseBase<TResult>>(jsonResult, _serializerSettings));
                }
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            return Task.FromResult<ResponseBase<TResult>>(null);
        }
        public Task<ResponseBase<TResult>> PostAsync<TResult>(string uri, dynamic fromBody, string token = "")
        {
            uri = ApiEndPoint + uri;
            try
            {
                Uri urlapi = new Uri(uri);
                var modelString = JsonConvert.SerializeObject(fromBody);
                using (var content = new StringContent(modelString, Encoding.UTF8, "application/json"))
                using (var request = new HttpRequestMessage(HttpMethod.Post, urlapi) { Content = content })
                {
                    if (!string.IsNullOrEmpty(token))
                        request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                    var response = SharedHttpClient.SendAsync(request).GetAwaiter().GetResult();
                    var jsonResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return Task.FromResult(JsonConvert.DeserializeObject<ResponseBase<TResult>>(jsonResult, _serializerSettings));
                }
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            return Task.FromResult<ResponseBase<TResult>>(null);
        }

        public Task<ResponseBase<TResult>> PutAsync<TResult>(string uri, dynamic fromBody, string token = "")
        {
            uri = ApiEndPoint + uri;
            try
            {
                Uri urlapi = new Uri(uri);
                var modelString = JsonConvert.SerializeObject(fromBody);
                using (var content = new StringContent(modelString, Encoding.UTF8, "application/json"))
                using (var request = new HttpRequestMessage(HttpMethod.Put, urlapi) { Content = content })
                {
                    if (!string.IsNullOrEmpty(token))
                        request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                    var response = SharedHttpClient.SendAsync(request).GetAwaiter().GetResult();
                    var jsonResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return Task.FromResult(JsonConvert.DeserializeObject<ResponseBase<TResult>>(jsonResult, _serializerSettings));
                }
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            return Task.FromResult<ResponseBase<TResult>>(null);
        }

        public Task<ResponseBase<bool>> DeleteAsync(string uri, string token = "")
        {
            uri = ApiEndPoint + uri;
            try
            {
                Uri urlapi = new Uri(uri);
                using (var request = new HttpRequestMessage(HttpMethod.Delete, urlapi))
                {
                    if (!string.IsNullOrEmpty(token))
                        request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                    var response = SharedHttpClient.SendAsync(request).GetAwaiter().GetResult();
                    var jsonResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return Task.FromResult(JsonConvert.DeserializeObject<ResponseBase<bool>>(jsonResult, _serializerSettings));
                }
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
            return Task.FromResult<ResponseBase<bool>>(null);
        }
    }
}
