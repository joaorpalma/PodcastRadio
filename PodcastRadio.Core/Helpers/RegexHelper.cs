using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PodcastRadio.Core.Helpers
{
    public static class RegexHelper
    {
        public static string GetPodcastId(string guid)
        {
            var podcastId = Regex.Match(guid, @"id(.+?)?m").Groups[1].Value;

            podcastId.Substring(0, podcastId.Length - 1);

            if (!podcastId.All(char.IsDigit))
                return new String(podcastId.Where(Char.IsDigit).ToArray());
            else
                return podcastId.Substring(0, podcastId.Length - 1);
        }
    }
}
