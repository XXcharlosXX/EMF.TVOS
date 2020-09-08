using Newtonsoft.Json;

namespace EMFTV.Models
{
    public class BroadcastStream
    {
        [JsonProperty("preRollUrl")]
        public string PreRollUrl { get; set; }
        [JsonProperty("preRollType")]
        public string PreRollType { get; set; }
        [JsonProperty("streamUrl")]
        public string StreamUrl { get; set; }
        [JsonProperty("streamType")]
        public string StreamType { get; set; }
        [JsonProperty("streamLocation")]
        public string StreamLocation { get; set; }
        [JsonProperty("streamRfFreq")]
        public string StreamFrequency { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
