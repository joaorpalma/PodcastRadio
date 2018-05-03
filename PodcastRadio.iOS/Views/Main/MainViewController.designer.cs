// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PodcastRadio.iOS.Views.Main
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		UIKit.UIView _closeTabView { get; set; }

		[Outlet]
		UIKit.UILabel _noResultsLabel { get; set; }

		[Outlet]
		UIKit.UIView _noResultsView { get; set; }

		[Outlet]
		UIKit.UIView _pickerHeaderView { get; set; }

		[Outlet]
		UIKit.UIPickerView _pickerView { get; set; }

		[Outlet]
		UIKit.UITableView _tableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_closeTabView != null) {
				_closeTabView.Dispose ();
				_closeTabView = null;
			}

			if (_pickerHeaderView != null) {
				_pickerHeaderView.Dispose ();
				_pickerHeaderView = null;
			}

			if (_pickerView != null) {
				_pickerView.Dispose ();
				_pickerView = null;
			}

			if (_tableView != null) {
				_tableView.Dispose ();
				_tableView = null;
			}

			if (_noResultsView != null) {
				_noResultsView.Dispose ();
				_noResultsView = null;
			}

			if (_noResultsLabel != null) {
				_noResultsLabel.Dispose ();
				_noResultsLabel = null;
			}
		}
	}
}
