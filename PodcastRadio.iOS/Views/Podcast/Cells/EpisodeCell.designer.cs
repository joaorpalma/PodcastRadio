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
	[Register ("EpisodeCell")]
	partial class EpisodeCell
	{
		[Outlet]
		UIKit.UIActivityIndicatorView _activityIndicator { get; set; }

		[Outlet]
		UIKit.UILabel _durationLabel { get; set; }

		[Outlet]
		UIKit.UIImageView _explicit { get; set; }

		[Outlet]
		UIKit.UIView _loadingView { get; set; }

		[Outlet]
		UIKit.UILabel _nameLabel { get; set; }

		[Outlet]
		UIKit.UIButton _playButton { get; set; }

		[Outlet]
		UIKit.UIImageView _playImage { get; set; }

		[Outlet]
		UIKit.UILabel _trackLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_durationLabel != null) {
				_durationLabel.Dispose ();
				_durationLabel = null;
			}

			if (_explicit != null) {
				_explicit.Dispose ();
				_explicit = null;
			}

			if (_nameLabel != null) {
				_nameLabel.Dispose ();
				_nameLabel = null;
			}

			if (_playButton != null) {
				_playButton.Dispose ();
				_playButton = null;
			}

			if (_playImage != null) {
				_playImage.Dispose ();
				_playImage = null;
			}

			if (_trackLabel != null) {
				_trackLabel.Dispose ();
				_trackLabel = null;
			}

			if (_loadingView != null) {
				_loadingView.Dispose ();
				_loadingView = null;
			}

			if (_activityIndicator != null) {
				_activityIndicator.Dispose ();
				_activityIndicator = null;
			}
		}
	}
}
