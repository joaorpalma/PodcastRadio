// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PodcastRadio.iOS.Views.CustomViews
{
	partial class PodcastHeaderView
	{
		[Outlet]
		UIKit.NSLayoutConstraint _backgroundHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIImageView _backgroundImage { get; set; }

		[Outlet]
		UIKit.UILabel _nameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView _pictureImage { get; set; }

		[Outlet]
		UIKit.UILabel _playLabel { get; set; }

		[Outlet]
		UIKit.UILabel _trackLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_backgroundImage != null) {
				_backgroundImage.Dispose ();
				_backgroundImage = null;
			}

			if (_nameLabel != null) {
				_nameLabel.Dispose ();
				_nameLabel = null;
			}

			if (_pictureImage != null) {
				_pictureImage.Dispose ();
				_pictureImage = null;
			}

			if (_playLabel != null) {
				_playLabel.Dispose ();
				_playLabel = null;
			}

			if (_trackLabel != null) {
				_trackLabel.Dispose ();
				_trackLabel = null;
			}

			if (_backgroundHeightConstraint != null) {
				_backgroundHeightConstraint.Dispose ();
				_backgroundHeightConstraint = null;
			}
		}
	}
}
