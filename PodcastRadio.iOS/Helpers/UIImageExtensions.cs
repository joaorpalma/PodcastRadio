using System;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using CoreImage;
using Foundation;
using SDWebImage;
using UIKit;

namespace PodcastRadio.iOS.Helpers
{
    public static class UIImageExtensions
    {
        public static void GetCountryFlag(UIImageView imageView, string country)
        {
            country = country.ToLower();

            if (country.Length == 3)
                country = country.Remove(country.Length - 1);
            
            imageView.SetImage(new NSUrl($"http://www.geonames.org/flags/x/{country}.gif"), null, SDWebImageOptions.RetryFailed);
        }
    }
}
