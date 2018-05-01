// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PodcastRadio.iOS.Views.Podcast.Cells
{
	[Register ("ConnectionCell")]
	partial class ConnectionCell
	{
		[Outlet]
		UIKit.UIImageView _iconImage { get; set; }

		[Outlet]
		UIKit.UILabel _nameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_iconImage != null) {
				_iconImage.Dispose ();
				_iconImage = null;
			}

			if (_nameLabel != null) {
				_nameLabel.Dispose ();
				_nameLabel = null;
			}
		}
	}
}
