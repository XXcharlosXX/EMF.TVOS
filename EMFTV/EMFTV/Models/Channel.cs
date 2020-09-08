using Newtonsoft.Json;

namespace EMFTV.Models
{
    [JsonObject("channel")]
    public class Channel
    {
        [JsonProperty("networkid")]
        public int Networkid { get; set; }

        [JsonProperty("network")]
        public NetworkType Network { get; set; }

        [JsonProperty("device")]
        public DeviceType Device { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public Description Description { get; set; }

        [JsonProperty("regionalized")]
        public bool Regionalized { get; set; }

        [JsonProperty("streamurl")]
        public string StreamUrl { get; set; }

        [JsonProperty("hubname")]
        public string HubName { get; set; }

        [JsonProperty("sourceid")]
        public int SourceId { get; set; }
    }
    public enum Description { ChristmasStream, ClassicsStream, LiveStream };
    public enum DeviceType { Android, Default, Ios };
}
