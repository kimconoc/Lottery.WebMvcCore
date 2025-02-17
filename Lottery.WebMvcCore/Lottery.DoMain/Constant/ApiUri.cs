using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Constant
{
    public class ApiUri
    {
        public const string POST_UserLogin = "User/login";
        public const string PUT_UpdateUser = "User/update-user";
        public const string GET_UserInfo = "User/user-info";
        public const string Get_CommonChanels = "Common/chanels";
        public const string POST_CalculationCal1 = "Calculation/cal-1";
        public const string POST_CalculationCal2 = "Calculation/cal-2";
        public const string POST_CalculationCal3 = "Calculation/cal-3";
        public const string POST_CalculationCalFilter = "Calculation/filter";
        public const string POST_UserUpdatePhonebook = "User/update-phonebook";
        public const string Get_Phonebook = "User/phone-book";
        public const string Get_UpdateDay = "Common/update-day";
        public const string POST_HandlMessagemessageByDay = "HandlMessage/message-by-day";
        public const string Get_HandlMessage = "HandlMessage";
        public const string DELETE_HandlMessage = "HandlMessage";
        public const string DELETE_HandlMessageDelete_Multi = "HandlMessage/delete-multi";
        public const string POST_HandlMessagehandlMessage = "HandlMessage/handl-message";
        public const string POST_HandlMessageCountByDay = "HandlMessage/count-by-day";
        public const string POST_HandlMessageCountManyDay = "HandlMessage/count-many-day";
        public const string GET_AdminListing = "Admin/listing";
        public const string POST_AdminAdd = "Admin/add";
        public const string DELETE_Admin = "Admin";
        public const string POST_AdminRenew = "Admin/Renew";
        public const string POST_AdminReset = "Admin/Reset";
        public const string POST_AdminChangePass = "Admin/change-pass";
        public const string POST_AdminUpdate = "Admin/update";
    }
}
