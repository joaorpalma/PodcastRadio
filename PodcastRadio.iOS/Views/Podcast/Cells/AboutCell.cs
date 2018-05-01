using System;

using Foundation;
using UIKit;

namespace PodcastRadio.iOS.Views.Podcast.Cells
{
    public partial class AboutCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("AboutCell");
        public static readonly UINib Nib = UINib.FromName("AboutCell", NSBundle.MainBundle);
        protected AboutCell(IntPtr handle) : base(handle) {}
    }
}
