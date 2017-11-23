using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WakeUpIOTHub
{
    public class Program
    {
        public static string iotHubConnectionString = ConfigurationManager.AppSettings["IOTHUB_ConnectionString"];
        public static string iotHubDeviceName = ConfigurationManager.AppSettings["IOTHUB_DeviecName"];
        public static LoggerHelper loggerHelper = new LoggerHelper();
        public static LogInfo logInfo = new LogInfo();
        public static RequestHelper requestHelper = new RequestHelper();


        static void Main(string[] args)
        {
            try
            {
                AvoidCertificate();

                string sendingMessage = "WakeUp---IOTHUB!!";
                string deviceName = ConfigurationManager.AppSettings["IOTHUB_DeviecName"];

                SaveLogByApi("tdd-2012-2", "WakeUpIotHub", "Start--SendWakeUpMessage", sendingMessage, DateTime.Now.ToString());

                SendIOTHubMessageByBeaconWebApi(sendingMessage, deviceName);

                SaveLogByApi("tdd-2012-2", "WakeUpIotHub", "End--SendWakeUpMessage", sendingMessage, DateTime.Now.ToString());

            }
            catch (Exception e)
            {

                throw;
            }
       
        }



        private static void SaveLogByApi(string deviceName, string userId, string deviceAction, string logMessage, string deviceDatetime)
        {
            try
            {
                logInfo.DeviceName = deviceName;
                logInfo.DeviceAction = deviceAction;
                logInfo.LogMessage = logMessage;
                logInfo.UserID = userId;
                logInfo.DeviceDateTime = deviceDatetime;

                loggerHelper.PostUWPLogByApi(logInfo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void SendIOTHubMessageByBeaconWebApi(string sendingMessage , string deviceName)
        {
            string postUrl = ConfigurationManager.AppSettings["SendMessage2IOTHubUrl"];
            JObject postJsonBody = JObject.FromObject(
                new
                {
                    deviceName =deviceName,
                    message = sendingMessage
                });


            requestHelper.DoPostRequest(postUrl, postJsonBody.ToString());

            return;

        }


        private static void AvoidCertificate()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            new System.Net.Security.RemoteCertificateValidationCallback(
            delegate (object MySender,
            System.Security.Cryptography.X509Certificates.X509Certificate MyCertificate,
            System.Security.Cryptography.X509Certificates.X509Chain MyChain,
            System.Net.Security.SslPolicyErrors MyErrors)
            {
                if (MySender is System.Net.WebRequest)
                {
                    //忽略憑証檢查，一律回傳true
                    return true;
                }
                return false;
            });

        }
    }

    
}
