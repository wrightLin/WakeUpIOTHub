using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Configuration;

namespace WakeUpIOTHub
{
    public class RequestHelper
    {
        public string BeaconWebApiAuthHeadKey = ConfigurationManager.AppSettings["BeaconWebApiAuthHeadKey"];
        public string BeaconWebApiAuthHeadValue = ConfigurationManager.AppSettings["BeaconWebApiAuthHeadValue"];
        public string PostInfoMessage2LogUrl = ConfigurationManager.AppSettings["PostInfoMessage2LogUrl"];
        public string PostErrorMessage2LogUrl = ConfigurationManager.AppSettings["PostErrorMessage2LogUrl"];


        /// <summary>
        /// Do Http Get Request(BeaconWebApi)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DoGetRequest(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(BeaconWebApiAuthHeadKey, BeaconWebApiAuthHeadValue);
            HttpResponseMessage response = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Do Http Post Request(BeaconWebApi)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DoPostRequest(string url, string jsonString)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(BeaconWebApiAuthHeadKey, BeaconWebApiAuthHeadValue);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return response.Content.ReadAsStringAsync().Result;
        }

    }
}
