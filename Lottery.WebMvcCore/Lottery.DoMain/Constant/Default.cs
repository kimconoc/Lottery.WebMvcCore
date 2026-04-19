using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Constant
{
    public class Default
    {
        public const string VersionApp = "1.2.2";
        public const int CachTrungDaXien = 2;

        /// <summary>Header back khi quản lý tài khoản từ UserListing.</summary>
        public const string AdministratorAccount_Header_UserListingContext = "Quản Lý Tài Khoản";
        /// <summary>Header back khi thao tác đại lý từ AgentListing.</summary>
        public const string AdministratorAccount_Header_AgentListingContext = "Quản Lý Đại Lý";
        public const string AdministratorAccount_Return_UserListing = "/Administrator/UserListing";
        public const string AdministratorAccount_Return_AgentListing = "/Administrator/AgentListing";
        public const string Link_Empty_Data = "~/lottery/empty-order/empty-order.png";
        public const string Get_Signin_Token = "LotteryCookie";
        public const string Get_Signin_Date = "LotteryDate";
    }
}
