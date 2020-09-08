using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EMFTV.Models
{
    public class ArtistModel
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ShortBio")]
        public string ShortBio { get; set; }

        [JsonProperty("Bio")]
        public string Bio { get; set; }

        [JsonProperty("ImageId")]
        public long ImageId { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("FacebookUrl")]
        public string FacebookUrl { get; set; }

        [JsonProperty("TwitterUrl")]
        public string TwitterUrl { get; set; }

        [JsonProperty("InstagramUrl")]
        public string InstagramUrl { get; set; }

        [JsonProperty("YoutubeUrl")]
        public string YoutubeUrl { get; set; }

        [JsonProperty("WebsiteUrl")]
        public string WebsiteUrl { get; set; }

        [JsonProperty("CleanTitle")]
        public string CleanTitle { get; set; }

        [JsonProperty("IsBioReviewed")]
        public bool IsBioReviewed { get; set; }

        [JsonProperty("BioReviewedOn")]
        public DateTimeOffset BioReviewedOn { get; set; }

        [JsonProperty("BioReviewedBy")]
        public object BioReviewedBy { get; set; }

        [JsonProperty("LastOnAir")]
        public DateTimeOffset LastOnAir { get; set; }

        [JsonProperty("LastOnAirNetworkId")]
        public long LastOnAirNetworkId { get; set; }

        [JsonProperty("Songs")]
        public List<ArtistSong> Songs { get; set; }

        [JsonProperty("Networks")]
        public object[] Networks { get; set; }

        [JsonProperty("Tidbits")]
        public object Tidbits { get; set; }

        [JsonProperty("CreatedOn")]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdatedOn")]
        public DateTimeOffset UpdatedOn { get; set; }

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }
    }
}
