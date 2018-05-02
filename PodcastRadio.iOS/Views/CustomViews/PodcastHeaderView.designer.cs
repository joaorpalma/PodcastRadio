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
		
		void ReleaseDesignerOutlets ()
		{
			if (_backgroundHeightConstraint != null) {
				_backgroundHeightConstraint.Dispose ();
				_backgroundHeightConstraint = null;
			}

			if (_backgroundImage != null) {
				_backgroundImage.Dispose ();
				_backgroundImage = null;
			}
		}
	}
}
