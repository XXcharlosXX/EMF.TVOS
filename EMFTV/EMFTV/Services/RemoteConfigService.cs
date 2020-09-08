using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMFTV.Global;
using EMFTV.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EMFTV.Services
{
    public class RemoteConfigService : EmfServiceBase
    {
        public static readonly string URI = "https://api.corpemf.com/remoteconfig/v1/configs/";
        public static readonly string Key = "Ocp-Apim-Subscription-Key";
        public static readonly string Value = "82a3b83c550f4a5a80028a8745c6590b";
        public async static Task GetAppConfig(string id)
        {
            try
            {
                var uri = URI + id;
                ApplicationConfig AppConfig = new ApplicationConfig();
                JObject jo = await GetData(uri, Key, Value);

                if (jo != null)
                {
                    AppConfig = JsonConvert.DeserializeObject<ApplicationConfig>(jo.ToString());
                    Application.Config = AppConfig;
                    try
                    {
                        foreach (Channel ch in AppConfig.Channels)
                        {
                            if (ch.Title == "Live")
                            {
                                if (ch.Device == Models.DeviceType.Ios)
                                {
                                    Variables.CurrentChannel = ch;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("error");
                    }
                    Application.Config.Channels.Reverse();
                    //Console.WriteLine($"App Config:{AppConfig}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error:{ex}");
            }
        }
        public async static Task GetCloudData(string id)
        {
            try
            {
                var uri = URI + id;
                CloudData CData = new CloudData();
                JObject jo = await GetData(uri, Key, Value);

                if (jo != null)
                {
                    CData = JsonConvert.DeserializeObject<CloudData>(jo.ToString());
                    Application.CData = CData;
                    //Console.WriteLine($"Cdata:{CData}");
                }
                else
                {
                    Console.WriteLine($"Cdata:null");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error:{ex}");
            }
        }
    }
}
