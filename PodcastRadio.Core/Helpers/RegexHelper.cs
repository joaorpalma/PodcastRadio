using System;
using System.Text.RegularExpressions;

namespace PodcastRadio.Core.Helpers
{
    public static class RegexHelper
    {
        public static string GetPodcastId(string guid)
        {
            var podcastId = Regex.Match(guid, @"id(.+?)?m").Groups[1].Value;
            return podcastId.Substring(0, podcastId.Length-1);
        }
    }
}
