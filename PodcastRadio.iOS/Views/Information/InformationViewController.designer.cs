// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PodcastRadio.iOS.Views.Information
{
    [Register ("InformationViewController")]
    partial class InformationViewController
    {
        [Outlet]
        UIKit.UIButton _closeButton { get; set; }


        [Outlet]
        UIKit.UILabel _copyrightLabel { get; set; }


        [Outlet]
        UIKit.UILabel _createdByLabel { get; set; }


        [Outlet]
        UIKit.UILabel _creatorNameLabel { get; set; }


        [Outlet]
        UIKit.UIImageView _logoImage { get; set; }


        [Outlet]
        UIKit.UIView _navBar { get; set; }


        [Outlet]
        UIKit.UILabel _titleLabel { get; set; }


        [Outlet]
        UIKit.UILabel _toLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_closeButton != null) {
                _closeButton.Dispose ();
                _closeButton = null;
            }

            if (_copyrightLabel != null) {
                _copyrightLabel.Dispose ();
                _copyrightLabel = null;
            }

            if (_createdByLabel != null) {
                _createdByLabel.Dispose ();
                _createdByLabel = null;
            }

            if (_creatorNameLabel != null) {
                _creatorNameLabel.Dispose ();
                _creatorNameLabel = null;
            }

            if (_logoImage != null) {
                _logoImage.Dispose ();
                _logoImage = null;
            }

            if (_navBar != null) {
                _navBar.Dispose ();
                _navBar = null;
            }

            if (_titleLabel != null) {
                _titleLabel.Dispose ();
                _titleLabel = null;
            }

            if (_toLabel != null) {
                _toLabel.Dispose ();
                _toLabel = null;
            }
        }
    }
}