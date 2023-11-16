namespace Lottery.WebMvc.Models
{
    public class UserManagementModel
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public DateTime ExpireDate { get; set; }
        public string StrExpireDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Note { get; set; }
    }
    public class ExtendExpireDateModel
    {
        public int UserId { get; set; }
        public DateTime NewExpireDate { get; set; }
    }
    public class NewPassModel
    {
        public int UserId { get; set; }
        public string NewPass { get; set; }
    }
}
