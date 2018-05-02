using System;
using System.Collections.Generic;
using Foundation;
using PodcastRadio.iOS.Helpers;
using UIKit;

namespace PodcastRadio.iOS.Views.Podcast.Cells
{
    public partial class PodcastHeaderCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("PodcastHeaderCell");
        public static readonly UINib Nib = UINib.FromName("PodcastHeaderCell", NSBundle.MainBundle);
        protected PodcastHeaderCell(IntPtr handle) : base(handle) {}

        public void Configure(Dictionary<string, string> locationResources)
        {
            UILabelExtensions.SetupLabelAppearance(_trackLabel, locationResources["TrackLabel"], Colors.Black, 10f, UIFontWeight.Medium);
            UILabelExtensions.SetupLabelAppearance(_nameLabel, locationResources["NameDurationLabel"], Colors.Black, 10f, UIFontWeight.Medium);
            UILabelExtensions.SetupLabelAppearance(_playLabel, locationResources["PlayLabel"], Colors.Black, 10f, UIFontWeight.Medium);
        }
    }
}
