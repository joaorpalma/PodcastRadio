using System;
using Newtonsoft.Json;

namespace PodcastRadio.Core.Models.DTOs
{
    public class PodcastModel
    {
        [JsonProperty("artistId")]
        public string PodcastId { get; set; }

        [JsonProperty("artistName")]
        public string Name { get; set; }

        [JsonProperty("artworkUrl100")]
        public string ArtworkSmall { get; set; }

        [JsonProperty("artworkUrl600")]
        public string ArtworkLarge { get; set; }

        [JsonProperty("collectionName")]
        public string PodcastName { get; set; }

        [JsonProperty("collectionViewUrl")]
        public string PodcastUrl { get; set; }

        [JsonProperty("contentAdvisoryRating")]
        public string AdvisoryRating { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("feedUrl")]
        public string FeedUrl { get; set; }

        [JsonProperty("primaryGenreName")]
        public string PrimaryGenre { get; set; }

        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonProperty("trackCount")]
        public string NumberTracks { get; set; }

        [JsonIgnore]
        public PodcastChannel Channel { get; set; }
    }
}
