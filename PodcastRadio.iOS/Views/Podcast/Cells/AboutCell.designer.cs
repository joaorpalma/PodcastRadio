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
	[Register ("AboutCell")]
	partial class AboutCell
	{
		[Outlet]
		UIKit.UILabel _aboutLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_aboutLabel != null) {
				_aboutLabel.Dispose ();
				_aboutLabel = null;
			}
		}
	}
}
