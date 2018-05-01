using System;
using System.Collections.Generic;
using Foundation;
using PodcastRadio.Core.Models;
using PodcastRadio.iOS.Helpers;
using UIKit;

namespace PodcastRadio.iOS.Sources
{
    public class PodcastSource : UITableViewSource
    {
        private enum Sections
        {
            Episodes,
            About,
            Connections,
            Count
        }

        private UITableView _tableView;
        private PodcastChannel _podcast;
        private Dictionary<string, string> _locationResources = new Dictionary<string, string>();
        public event EventHandler<string> OnPlayPress;

        public PodcastSource(UITableView tableView, PodcastChannel podcast, Dictionary<string, string> locationResources)
        {
            _tableView = tableView;
            _podcast = podcast;
            _locationResources = locationResources;
        }

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = null;

            //_tableView.EstimatedRowHeight = 50;

            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return (int)Sections.Count;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            switch (section)
            {
                case (int)Sections.Episodes: return _podcast.Episodes.Count;
                case (int)Sections.About: return 1;
                case (int)Sections.Connections: return 1;
                default: return 0;
            }
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return section == (int)Sections.About ? LocalConstants.Podcast_TitleCell : 0;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {

            switch (indexPath.Section)
            {
                case (int)Sections.Episodes: return LocalConstants.Podcast_Episode;
                case (int)Sections.About: return UITableView.AutomaticDimension;
                case (int)Sections.Connections: return LocalConstants.Podcast_Connections;
                default: return 0;
            }
        }
    }
}
