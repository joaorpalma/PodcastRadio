using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using PodcastRadio.Core.Models.DTOs;
using PodcastRadio.iOS.Helpers;
using PodcastRadio.iOS.Views.Main.Cells;
using UIKit;

namespace PodcastRadio.iOS.Sources
{
    public class MainSource : UITableViewSource
    {
        private List<PodcastModel> _podcastDetail;
        private string _selectedCategory;
        private bool _showHeaderSection;
        private Dictionary<string, string> _locationResources = new Dictionary<string, string>();
        public event EventHandler<PodcastModel> OnOpenPodcastEvent;
        public event EventHandler OnHeaderPressedEvent;

        public MainSource(UITableView tableView, List<PodcastModel> podcastDetail, Dictionary<string, string> locationResources, string selectedCategory, bool showHeaderSection)
        {
            _podcastDetail = podcastDetail;
            _locationResources = locationResources;
            _selectedCategory = selectedCategory;
            _showHeaderSection = showHeaderSection;
            tableView.RegisterNibForCellReuse(PodcastCell.Nib, PodcastCell.Key);
            tableView.RegisterNibForCellReuse(PodcastHeaderSearchCell.Nib, PodcastHeaderSearchCell.Key);
        }

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
            var cell = tableView.DequeueReusableCell(PodcastHeaderSearchCell.Key) as PodcastHeaderSearchCell;
            cell.Configure(OnHeaderPressedEvent, _locationResources, _selectedCategory);
            return cell;
		}

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var podcastCell = tableView.DequeueReusableCell(PodcastCell.Key, indexPath) as PodcastCell;
            podcastCell.Configure(_podcastDetail[(indexPath.Row)], _locationResources);
            podcastCell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return podcastCell;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            if(_showHeaderSection)
                return LocalConstants.Main_HeaderCell;
            
            return 0;
        } 

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) => LocalConstants.Main_PodcastCell;

        public override nint NumberOfSections(UITableView tableView) => 1;

		public override nint RowsInSection(UITableView tableview, nint section) => (nint)_podcastDetail?.Count;

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            OnOpenPodcastEvent?.Invoke(this, _podcastDetail[indexPath.Row]);
        }
	}
}