using System;
using System.Globalization;

namespace PodcastRadio.Core.Helpers
{
    public static class TimeUtils
    {
        public static string[] SetMonthAndYearFormat(DateTime time)
        {
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(time.Month);
            var date = new string[] { time.ToString("dd"), month };
            return date;
        }
    }
}

