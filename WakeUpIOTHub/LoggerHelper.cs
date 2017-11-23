using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

namespace WakeUpIOTHub
{
    public class LoggerHelper
    {
        public RequestHelper requestHelper;
        public string PostInfoMessage2LogUrl = ConfigurationManager.AppSettings["PostInfoMessage2LogUrl"];
        

        public LoggerHelper()
        {
            requestHelper = new RequestHelper();
        }



        /// <summary>
        /// 將樹莓派開門時的資訊透過 WebApi寫 log
        /// </summary>
        /// <param name="info"></param>
        public void PostUWPLogByApi(LogInfo info)
        {
            string logInfoJsonString = JsonConvert.SerializeObject(info);
            string postLogUrl = PostInfoMessage2LogUrl;

            requestHelper.DoPostRequest(postLogUrl, logInfoJsonString);

        }
    }
}
