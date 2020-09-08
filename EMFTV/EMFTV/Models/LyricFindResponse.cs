using System;
using Newtonsoft.Json;

namespace EMFTV.Models
{
    public partial class LyricFindResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }

        [JsonProperty("track")]
        public Track Track { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class Track
    {
        [JsonProperty("lfid")]
        public string Lfid { get; set; }

        [JsonProperty("instrumental")]
        public bool Instrumental { get; set; }

        [JsonProperty("viewable")]
        public bool Viewable { get; set; }

        [JsonProperty("has_lrc")]
        public bool HasLrc { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("artists")]
        public ArtistElement[] Artists { get; set; }

        [JsonProperty("artist")]
        public ArtistName Artist { get; set; }

        [JsonProperty("last_update")]
        public DateTimeOffset LastUpdate { get; set; }

        [JsonProperty("lyrics")]
        public string Lyrics { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("writer")]
        public string Writer { get; set; }
    }

    public partial class ArtistName
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class ArtistElement
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_primary")]
        public bool IsPrimary { get; set; }
    }
}
