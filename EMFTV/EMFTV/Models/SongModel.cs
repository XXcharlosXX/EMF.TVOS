using System;
using Newtonsoft.Json;

namespace EMFTV.Models
{
    public class SongModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("partitionKey")]
        public long PartitionKey { get; set; }

        [JsonProperty("songId")]
        public string SongId { get; set; }

        [JsonProperty("songUrl")]
        public string SongUrl { get; set; }

        [JsonProperty("albumImage")]
        public string AlbumImage { get; set; }

        [JsonProperty("artistId")]
        public string ArtistId { get; set; }

        [JsonProperty("artistUrl")]
        public string ArtistUrl { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

        [JsonProperty("artistFacebookUrl")]
        public string ArtistFacebookUrl { get; set; }

        [JsonProperty("artistYoutubeUrl")]
        public string ArtistYoutubeUrl { get; set; }

        [JsonProperty("artistTwitterUrl")]
        public string ArtistTwitterUrl { get; set; }

        [JsonProperty("artistWebsiteUrl")]
        public string ArtistWebsiteUrl { get; set; }

        [JsonProperty("appleSongLink")]
        public string AppleSongLink { get; set; }

        [JsonProperty("amazonSongLink")]
        public string AmazonSongLink { get; set; }

        [JsonProperty("appleAlbumLink")]
        public string AppleAlbumLink { get; set; }

        [JsonProperty("amazonAlbumLink")]
        public string AmazonAlbumLink { get; set; }

        [JsonProperty("artistImageUrl")]
        public string ArtistImageUrl { get; set; }

        [JsonProperty("hookFileUrl")]
        public string HookFileUrl { get; set; }

        [JsonProperty("automationId")]
        public string AutomationId { get; set; }

        [JsonProperty("sourceId")]
        public int SourceId { get; set; }

        [JsonProperty("songTitle")]
        public string SongTitle { get; set; }

        [JsonProperty("artistName")]
        public string ArtistName { get; set; }

        [JsonProperty("albumTitle")]
        public string AlbumTitle { get; set; }

        [JsonProperty("songLength")]
        public string SongLength { get; set; }

        [JsonProperty("startDateTime")]
        public DateTime StartDateTime { get; set; }
    }
}
