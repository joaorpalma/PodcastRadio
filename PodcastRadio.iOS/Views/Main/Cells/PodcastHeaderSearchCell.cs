using System;
using System.Collections.Generic;
using Foundation;
using PodcastRadio.iOS.Helpers;
using UIKit;

namespace PodcastRadio.iOS.Views.Main.Cells
{
    public partial class PodcastHeaderSearchCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("PodcastHeaderSearchCell");
        public static readonly UINib Nib = UINib.FromName("PodcastHeaderSearchCell", NSBundle.MainBundle);
        protected EventHandler _headerPressed;

        protected PodcastHeaderSearchCell(IntPtr handle) : base(handle) {}

        public void Configure(EventHandler headerPressed, Dictionary<string, string> locationResources, string selectedCategory)
        {
            _headerPressed = headerPressed;
            UILabelExtensions.SetupLabelAppearance(_categoryLabel, locationResources["CategoryLabel"], UIColor.LightGray, 14);
            UILabelExtensions.SetupLabelAppearance(_categoryValueLabel, selectedCategory, Colors.Black, 17, UIFontWeight.Semibold);

            _selectButton.TouchUpInside -= OnSelectButton_TouchUpInside;
            _selectButton.TouchUpInside += OnSelectButton_TouchUpInside;
        }

        private void OnSelectButton_TouchUpInside(object sender, EventArgs e)
        {
            _headerPressed?.Invoke(this, EventArgs.Empty);
        } 
    }
}
