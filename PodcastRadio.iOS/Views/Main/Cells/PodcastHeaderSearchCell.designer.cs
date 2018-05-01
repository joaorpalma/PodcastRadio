// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PodcastRadio.iOS.Views.Main.Cells
{
    [Register ("PodcastHeaderSearchCell")]
    partial class PodcastHeaderSearchCell
    {
        [Outlet]
        UIKit.UILabel _categoryLabel { get; set; }


        [Outlet]
        UIKit.UILabel _categoryValueLabel { get; set; }


        [Outlet]
        UIKit.UIButton _selectButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_categoryLabel != null) {
                _categoryLabel.Dispose ();
                _categoryLabel = null;
            }

            if (_categoryValueLabel != null) {
                _categoryValueLabel.Dispose ();
                _categoryValueLabel = null;
            }

            if (_selectButton != null) {
                _selectButton.Dispose ();
                _selectButton = null;
            }
        }
    }
}