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
}
