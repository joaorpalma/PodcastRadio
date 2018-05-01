using System;
namespace PodcastRadio.Core.Helpers
{
    public static class TimeHelper
    {
        public static string SecondsToDuration(int seconds)
        {
            if (seconds < 0) seconds = 0;
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            string time = timeSpan.ToString(@"hh\:mm\:ss\:fff");
            return time;
        }
    }
}
