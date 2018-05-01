using System;
using CoreAnimation;
using CoreGraphics;
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

        public static void GradientBackGround(UIImageView gradImage, float alpha)
        {
            if (alpha < 0.3f)
            {
                if (gradImage.Layer.Sublayers != null && gradImage.Layer.Sublayers.Length > 1)
                    gradImage.Layer.Sublayers[1].RemoveFromSuperLayer();

                CGColor[] colors = new CGColor[]
                {
                    new UIColor(red: 0.00f, green:0.00f, blue:0.00f, alpha:(0.15f + alpha)).CGColor,
                    new UIColor(red: 0.00f, green: 0.00f, blue: 0.00f, alpha: (0.05f  + alpha)).CGColor,
                    new UIColor(red: 0.00f, green:0.00f, blue:0.00f, alpha:(0.2f + alpha)).CGColor
                };

                CAGradientLayer gradientLayer = new CAGradientLayer();
                gradientLayer.Frame = new CGRect(gradImage.Frame.X, gradImage.Frame.Y, UIScreen.MainScreen.Bounds.Width, gradImage.Frame.Height);
                gradientLayer.Colors = colors;
                gradImage.Layer.AddSublayer(gradientLayer);
            }
        }
    }
}
