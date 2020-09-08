using System;
using System.Net.Http;
using System.Threading.Tasks;
using EMFTV.Global;
using EMFTV.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EMFTV.Services
{
    public class StreamService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        protected async static Task<JObject> GetData(string url)
        {
            try
            {
                string json = await _httpClient.GetStringAsync(url);

                if (json == null)
                {
                    return null;
                }

                return JObject.Parse(json);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <param name="config"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public static async Task GetStreamUrlAsync()
        {
            try
            {
                string platformType = "AppleTV";
                string encoding = "aac";

                var url = $"{Application.Config.StreamServiceApi}?network={Variables.Networks[Variables.CurrentChannel.Networkid]}";
                url += $"&zip_code=";
                url += $"&encoding={encoding}&platform={platformType}";
                url += "&visitor_id=&session_id=&client_ip=";
                Console.WriteLine(url);
                //Variables.StreamRequestUrl = url;
                BroadcastStream streamObject = new BroadcastStream();
                JObject jo = await GetData(url);

                if (jo != null)
                {
                    // Get the parts of the URL
                    streamObject = JsonConvert.DeserializeObject<BroadcastStream>(jo.SelectToken("d").ToString());
                    //var uri = new Uri(streamObject.StreamUrl);
                    //string plainURL = uri.GetLeftPart(UriPartial.Path);
                    //string fullQuerystring = uri.Query.Length > 0 ? uri.Query.Substring(1) : "";
                    //string newQuerystring = "";

                    // Loop through each querystring key/value pair
                    //foreach (string querystring in fullQuerystring.Split("&"))
                    //{
                    //    // Prepend the appropriate delimiter
                    //    newQuerystring += newQuerystring.Length <= 0 ? "?" : "&";

                    //    // Check the variable to see if value needs to be replaced
                    //    var qsKeyVal = querystring.Split("=");
                    //    if (qsKeyVal[0].ToLower().Trim() == "visitor_id")
                    //    {
                    //        newQuerystring += qsKeyVal[0] + "=" + Variables.TealiumVisitorId;
                    //    }
                    //    else if (qsKeyVal[0].ToLower().Trim() == "session_id")
                    //    {
                    //        newQuerystring += qsKeyVal[0] + "=" + Variables.TealiumSessionId;
                    //    }
                    //    else // Passthrough
                    //    {
                    //        newQuerystring += qsKeyVal[0] + "=" + qsKeyVal[1];
                    //    }
                    //}

                    // Return the rebuilt URL
                    Variables.StreamUrl = streamObject.StreamUrl;
                    //return plainURL + newQuerystring;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public enum PlatformType
        {
            ANDROID = 1,
            IPHONE = 2
        }
    }
}
