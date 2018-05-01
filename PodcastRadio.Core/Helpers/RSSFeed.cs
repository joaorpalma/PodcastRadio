using System;
namespace PodcastRadio.Core.Helpers
{
    public static class RSSFeed
    {
        public static string TopTenPodcasts => "https://rss.itunes.apple.com/api/v1/us/podcasts/top-podcasts/all/10/explicit.rss";
    }
}
