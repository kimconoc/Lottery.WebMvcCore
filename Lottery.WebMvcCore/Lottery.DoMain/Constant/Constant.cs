using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Constant
{
    public static class Constant
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
                result = DateTime.Now;
            }

            return result;
        }
    }
}
