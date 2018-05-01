using System;

using Foundation;
using UIKit;

namespace PodcastRadio.iOS.Views.Podcast.Cells
{
    public partial class EpisodeCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EpisodeCell");
        public static readonly UINib Nib;

        static EpisodeCell()
        {
            Nib = UINib.FromName("EpisodeCell", NSBundle.MainBundle);
        }

        protected EpisodeCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
