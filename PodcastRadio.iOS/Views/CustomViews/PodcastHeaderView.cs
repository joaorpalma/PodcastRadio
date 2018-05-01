using System;
using System.Collections.Generic;
using Foundation;
using PodcastRadio.iOS.Helpers;
using SDWebImage;
using UIKit;

namespace PodcastRadio.iOS.Views.CustomViews
{
    [Register("PodcastHeaderView")]
    public partial class PodcastHeaderView : UIView
    {
        public static readonly UINib Nib = UINib.FromName("StoreTableViewHeader", NSBundle.MainBundle);
        public static readonly float Height = 250;
        public NSLayoutConstraint ImageHeightConstraint => _backgroundHeightConstraint;
        public UIImageView BackgroundImageview => _backgroundImage;
        public PodcastHeaderView(IntPtr handle) : base(handle) {}
        public static PodcastHeaderView Create() => Nib.Instantiate(null, null)[0] as PodcastHeaderView;

        public void Configure(string image, Dictionary<string, string> locationResources)
        {
            _backgroundImage.SetImage(new NSUrl(image), null, SDWebImageOptions.RetryFailed);
            _pictureImage.Image =_backgroundImage.Image;

            UIImageExtensions.GradientBackGround(_backgroundImage, 0);

            _pictureImage.Layer.CornerRadius = _pictureImage.Frame.Height/2;
            _pictureImage.ClipsToBounds = true;

            UILabelExtensions.SetupLabelAppearance(_trackLabel, locationResources["TrackLabel"], Colors.Black, 10f, UIFontWeight.Medium);
            UILabelExtensions.SetupLabelAppearance(_nameLabel, locationResources["NameDurationLabel"], Colors.Black, 10f, UIFontWeight.Medium);
            UILabelExtensions.SetupLabelAppearance(_playLabel, locationResources["PlayLabel"], Colors.Black, 10f, UIFontWeight.Medium);
        }
    }
}
