using System;

using Foundation;
using PodcastRadio.iOS.Helpers;
using UIKit;

namespace PodcastRadio.iOS.Views.Podcast.Cells
{
    public partial class ConnectionCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ConnectionCell");
        public static readonly UINib Nib = UINib.FromName("ConnectionCell", NSBundle.MainBundle);
        protected ConnectionCell(IntPtr handle) : base(handle) {}

        public void Configure(string name)
        {
            _iconImage.Image = UIImage.FromBundle("podcast_weblink");
            UILabelExtensions.SetupLabelAppearance(_nameLabel, name, Colors.MainBlue, 14f);
        }
    }
}
