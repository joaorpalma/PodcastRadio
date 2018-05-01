using System;
using CoreGraphics;
using UIKit;

namespace PodcastRadio.iOS.Helpers
{
    public static class UILabelExtensions
    {
        public static void SetupLabelAppearance(UILabel label, string text, UIColor color, nfloat textSize, UIFontWeight fontWeight = UIFontWeight.Regular)
        {
            label.Text = text;
            label.TextColor = color;
            label.Font = UIFont.SystemFontOfSize(textSize, fontWeight);
        }
    }
}
