using System;

using Foundation;
using PodcastRadio.iOS.Helpers;
using UIKit;

namespace PodcastRadio.iOS.Views.Podcast.Cells
{
    public partial class AboutCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("AboutCell");
        public static readonly UINib Nib = UINib.FromName("AboutCell", NSBundle.MainBundle);
        protected AboutCell(IntPtr handle) : base(handle) {}

        public void Configure(string about)
        {
            UILabelExtensions.SetupLabelAppearance(_aboutLabel, about, Colors.Black, 14f);
        }
    }
}
