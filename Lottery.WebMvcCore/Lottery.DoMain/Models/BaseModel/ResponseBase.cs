using Newtonsoft.Json;

namespace Lottery.DoMain.Models.BaseModel
{
    /// <summary>Envelope JSON từ KQ.Api — thành công theo Code = 0 (cùng quy ước ErrorCodeMessage.Success).</summary>
    public class ResponseBase<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        /// <summary>Không deserialize từ JSON: API có thể không gửi hoặc gửi sai; luôn suy ra từ <see cref="Code"/>.</summary>
        [JsonIgnore]
        public bool IsSuccessful => Code == 0;
    }
}
