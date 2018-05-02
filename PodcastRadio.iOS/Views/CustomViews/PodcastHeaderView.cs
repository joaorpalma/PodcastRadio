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
        public static readonly UINib Nib = UINib.FromName("PodcastHeaderView", NSBundle.MainBundle);
        public static readonly float Height = 200;
        public NSLayoutConstraint ImageHeightConstraint => _backgroundHeightConstraint;
        public UIImageView BackgroundImageview => _backgroundImage;
        public PodcastHeaderView(IntPtr handle) : base(handle) {}
        public static PodcastHeaderView Create() => Nib.Instantiate(null, null)[0] as PodcastHeaderView;

        public void Configure(string image)
        {
            _backgroundImage.SetImage(new NSUrl(image), null, SDWebImageOptions.RetryFailed);
        }
    }
}
