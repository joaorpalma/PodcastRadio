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
        protected EventHandler<string> _openWebsite;
        private string _link;

        public void Configure(string link, string name, EventHandler<string> openWebsite)
        {
            _link = link;
            _openWebsite = openWebsite;
            _iconImage.Image = UIImage.FromBundle("podcast_weblink");
            UILabelExtensions.SetupLabelAppearance(_nameLabel, name, Colors.MainBlue, 14f);

            _button.TouchUpInside -= Onbutton_TouchUpInside;
            _button.TouchUpInside += Onbutton_TouchUpInside;
        }

        private void Onbutton_TouchUpInside(object sender, EventArgs e)
        {
            _openWebsite?.Invoke(this, _link);
        }
    }
}
