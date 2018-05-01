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
    [Register ("PodcastCell")]
    partial class PodcastCell
    {
        [Outlet]
        UIKit.UILabel _categoryLabel { get; set; }


        [Outlet]
        UIKit.UIImageView _contentTypeImage { get; set; }


        [Outlet]
        UIKit.UILabel _dayDateLabel { get; set; }


        [Outlet]
        UIKit.UIImageView _flagImage { get; set; }


        [Outlet]
        UIKit.UIImageView _logoImage { get; set; }


        [Outlet]
        UIKit.UILabel _monthDateLabel { get; set; }


        [Outlet]
        UIKit.UILabel _nameLabel { get; set; }


        [Outlet]
        UIKit.UIButton _openPCButton { get; set; }


        [Outlet]
        UIKit.UILabel _titleLabel { get; set; }


        [Outlet]
        UIKit.UILabel _tracksLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_categoryLabel != null) {
                _categoryLabel.Dispose ();
                _categoryLabel = null;
            }

            if (_contentTypeImage != null) {
                _contentTypeImage.Dispose ();
                _contentTypeImage = null;
            }

            if (_dayDateLabel != null) {
                _dayDateLabel.Dispose ();
                _dayDateLabel = null;
            }

            if (_flagImage != null) {
                _flagImage.Dispose ();
                _flagImage = null;
            }

            if (_logoImage != null) {
                _logoImage.Dispose ();
                _logoImage = null;
            }

            if (_monthDateLabel != null) {
                _monthDateLabel.Dispose ();
                _monthDateLabel = null;
            }

            if (_nameLabel != null) {
                _nameLabel.Dispose ();
                _nameLabel = null;
            }

            if (_titleLabel != null) {
                _titleLabel.Dispose ();
                _titleLabel = null;
            }

            if (_tracksLabel != null) {
                _tracksLabel.Dispose ();
                _tracksLabel = null;
            }
        }
    }
}