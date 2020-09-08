using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EMFTV.Services
{
    public class EmfServiceBase
    {
        private static readonly HttpClient GZClient = new HttpClient();

        protected async static Task<JObject> GetData(string url, string HeaderKey = null, string HeaderValue = null, bool FormatJSON = true)
        {
            try
            {
                if (HeaderKey != null && HeaderValue != null)
                {
                    GZClient.DefaultRequestHeaders.Clear();
                    GZClient.DefaultRequestHeaders.Add(HeaderKey, HeaderValue);
                }
                string json = await GZClient.GetStringAsync(url);
                GZClient.DefaultRequestHeaders.Clear();
                if (json == null)
                {
                    return null;
                }
                return FormatJson(json);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected static JObject FormatJson(string jsonP)
        {
            JObject jo;
            try
            {
                int i = jsonP.IndexOf('(');
                int j = jsonP.LastIndexOf(')');
                if((j - i - 1) < 0)
                {
                    jo = JObject.Parse(jsonP);
                }
                else
                {
                    string json = jsonP.Substring(i + 1, j - i - 1);
                    jo = JObject.Parse(json);
                }
                return jo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


    }
}