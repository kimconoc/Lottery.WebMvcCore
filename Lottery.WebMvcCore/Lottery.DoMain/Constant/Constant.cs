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
    }
}
