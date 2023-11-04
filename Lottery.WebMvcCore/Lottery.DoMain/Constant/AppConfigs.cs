using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Lottery.DoMain.FileLog;
using System.Reflection;

namespace Lottery.DoMain.Constant
{
    public class AppConfigs
    {
        public static string GetApiEndPoint()
        {
            string apiEndPoint = string.Empty;
            try
            {
                string json = File.ReadAllText("appsettings.json");
                JObject jsonObject = JObject.Parse(json);
                apiEndPoint = (string)jsonObject["AppSettings"]["ApiEndPoint"];
            }
            catch (Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }
           
            return apiEndPoint;
        }
    }
}
