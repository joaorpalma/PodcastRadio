using System;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using CoreImage;
using FFImageLoading;
using Foundation;
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
            
            ImageService.Instance.LoadUrl($"http://www.geonames.org/flags/x/{country}.gif").Retry(3, 200).Into(imageView); 
        }
    }
}
