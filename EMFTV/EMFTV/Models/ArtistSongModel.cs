using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EMFTV.Models
{
    public partial class ArtistSong
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("LyricFindId", NullValueHandling = NullValueHandling.Ignore)]
        public string LyricFindId { get; set; }

        [JsonProperty("LyricSnippet", NullValueHandling = NullValueHandling.Ignore)]
        public string LyricSnippet { get; set; }

        [JsonProperty("LyricText", NullValueHandling = NullValueHandling.Ignore)]
        public string LyricText { get; set; }

        [JsonProperty("HookFileUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string HookFileUrl { get; set; }

        [JsonProperty("HookFileId", NullValueHandling = NullValueHandling.Ignore)]
        public string HookFileId { get; set; }

        [JsonProperty("CleanTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string CleanTitle { get; set; }

        [JsonProperty("AppleMusicId", NullValueHandling = NullValueHandling.Ignore)]
        public object AppleMusicId { get; set; }

        [JsonProperty("AmazonMusicId", NullValueHandling = NullValueHandling.Ignore)]
        public string AmazonMusicId { get; set; }

        [JsonProperty("LastOnAir", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastOnAir { get; set; }

        [JsonProperty("LastOnAirNetworkId", NullValueHandling = NullValueHandling.Ignore)]
        public string LastOnAirNetworkId { get; set; }

        [JsonProperty("AppleMusicUrl", NullValueHandling = NullValueHandling.Ignore)]
        public object AppleMusicUrl { get; set; }

        [JsonProperty("AmazonMusicUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string AmazonMusicUrl { get; set; }

        [JsonProperty("Artists", NullValueHandling = NullValueHandling.Ignore)]
        public SongArtist[] Artists { get; set; }

        [JsonProperty("Albums", NullValueHandling = NullValueHandling.Ignore)]
        public Albums[] Albums { get; set; }

        [JsonProperty("Automations", NullValueHandling = NullValueHandling.Ignore)]
        public object Automations { get; set; }

        [JsonProperty("CreatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("CreatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("UpdatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }
    }
    public partial class Albums
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("SongId", NullValueHandling = NullValueHandling.Ignore)]
        public string SongId { get; set; }

        [JsonProperty("AlbumId", NullValueHandling = NullValueHandling.Ignore)]
        public string AlbumId { get; set; }

        [JsonProperty("Album", NullValueHandling = NullValueHandling.Ignore)]
        public Album Album { get; set; }

        [JsonProperty("CreatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("CreatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("UpdatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }
    }

    public partial class Album
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("ImageId", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageId { get; set; }

        [JsonProperty("ImageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrl { get; set; }

        [JsonProperty("ReleaseDate", NullValueHandling = NullValueHandling.Ignore)]
        public object ReleaseDate { get; set; }

        [JsonProperty("LastOnAir", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LastOnAir { get; set; }

        [JsonProperty("LastOnAirNetworkId", NullValueHandling = NullValueHandling.Ignore)]
        public string LastOnAirNetworkId { get; set; }

        [JsonProperty("AppleMusicId", NullValueHandling = NullValueHandling.Ignore)]
        public string AppleMusicId { get; set; }

        [JsonProperty("AmazonMusicId", NullValueHandling = NullValueHandling.Ignore)]
        public string AmazonMusicId { get; set; }

        [JsonProperty("AppleMusicUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string AppleMusicUrl { get; set; }

        [JsonProperty("AmazonMusicUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string AmazonMusicUrl { get; set; }

        [JsonProperty("Songs", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Songs { get; set; }

        [JsonProperty("CreatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("CreatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("UpdatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }
    }
    public partial class SongArtist
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("SongId", NullValueHandling = NullValueHandling.Ignore)]
        public string SongId { get; set; }

        [JsonProperty("ArtistId", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtistId { get; set; }

        [JsonProperty("Artist", NullValueHandling = NullValueHandling.Ignore)]
        public object Artist { get; set; }

        [JsonProperty("CreatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("CreatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("UpdatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }
    }
}