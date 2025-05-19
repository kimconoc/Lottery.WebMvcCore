using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Constant
{
    public class Constant
    {
        // "yyyy/MM/dd HH:mm"
        public static DateTime ConvertStringToDateTime(string strDateTime)
        {
            DateTime result;
            string format = "yyyy/MM/dd HH:mm";
            if (string.IsNullOrEmpty(strDateTime))
            {
                strDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
            if (!DateTime.TryParseExact(strDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                DateTime now = DateTime.Now;
                result = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            }

            return result;
        }

        public static DateTime ConvertStringToDateTime()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
        }

        public static string FormatPhoneWithDots(string phone)
        {
            // Loại bỏ mọi ký tự không phải số
            string digits = new string(phone.Where(char.IsDigit).ToArray());

            // Kiểm tra đủ độ dài 10 số
            if (digits.Length != 10)
                return phone; // Trả về nguyên gốc nếu không đúng định dạng

            // Định dạng: 0912.86.33.11
            return string.Format("{0}.{1}.{2}.{3}",
                digits.Substring(0, 4),
                digits.Substring(4, 2),
                digits.Substring(6, 2),
                digits.Substring(8, 2)
            );
        }

    }
}
