using System;
using System.Collections.Generic;

namespace PodcastRadio.Core.Models
{
    public class PodcastChannel
    {
        public string Link { get; set; }
        public string Summary { get; set; }
        public List<Episode> Episodes { get; set; }
    }

    public class Episode
    {
        public string Title { get; set; }
        public string EpisodeNumber { get; set; }
        public string Duration { get; set; }
        public string Explicit { get; set; }
        public string AudioLink { get; set; }
        public string PublicationDate { get; set; }
        public bool IsPlaying { get; set; }
    }
}
