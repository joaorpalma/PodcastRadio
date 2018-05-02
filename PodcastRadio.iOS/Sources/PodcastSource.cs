using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using PodcastRadio.Core.Models;
using PodcastRadio.iOS.Helpers;
using PodcastRadio.iOS.Views.CustomViews;
using PodcastRadio.iOS.Views.Podcast.Cells;
using UIKit;

namespace PodcastRadio.iOS.Sources
{
    public class PodcastSource : UITableViewSource
    {
        private enum Section
        {
            Episodes,
            Connections,
            About,
            Count
        }

        private UITableView _tableView;
        private PodcastChannel _podcast;
        private Dictionary<string, string> _locationResources = new Dictionary<string, string>();
        public event EventHandler<Episode> OnPlayPressEvent;

        public PodcastSource(UITableView tableView, PodcastChannel podcast, Dictionary<string, string> locationResources)
        {
            _tableView = tableView;
            _podcast = podcast;
            _locationResources = locationResources;
            tableView.RegisterNibForCellReuse(EpisodeCell.Nib, EpisodeCell.Key);
            tableView.RegisterNibForCellReuse(ConnectionCell.Nib, ConnectionCell.Key);
            tableView.RegisterNibForCellReuse(AboutCell.Nib, AboutCell.Key);
        }

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
            if (section == (int)Section.Connections)
            {
                var cell = new UITableViewCell(UITableViewCellStyle.Default, "TableCell");
                cell.TextLabel.Text = _locationResources["AboutLabel"];
                cell.TextLabel.TextColor = Colors.Black;
                cell.TextLabel.Font = UIFont.BoldSystemFontOfSize(18f);
                cell.BackgroundColor = Colors.White;
                return cell;
            }
            return null;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = null;

            switch (indexPath.Section)
            {

                case (int)Section.Episodes:
                    var episodecell = tableView.DequeueReusableCell(EpisodeCell.Key) as EpisodeCell;
                    episodecell.Configure(_podcast.Episodes[indexPath.Row], _locationResources, OnPlayPressEvent);
                    episodecell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    cell = episodecell;
                    break;

                case (int)Section.About:
                    var aboutcell = tableView.DequeueReusableCell(AboutCell.Key) as AboutCell;
                    aboutcell.Configure(_podcast.Summary);
                    aboutcell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    cell = aboutcell;
                    break;
                
                case(int)Section.Connections:
                    var connectioncell = tableView.DequeueReusableCell(ConnectionCell.Key) as ConnectionCell;
                    connectioncell.Configure(_podcast.Link);
                    connectioncell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    connectioncell.LayoutMargins = UIEdgeInsets.Zero;
                    cell = connectioncell;
                    break;

                default:
                    break;
            }

            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return (int)Section.Count;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            switch (section)
            {
                case (int)Section.Episodes: return _podcast.Episodes.Count;
                case (int)Section.About: return 1;
                case (int)Section.Connections: return 1;
                default: return 0;
            }
        }

        public override void Scrolled(UIScrollView scrollView)
        {
            var offsetY = scrollView.ContentOffset.Y;
            _tableView.TableHeaderView.Layer.MasksToBounds = offsetY > 0;
            var headerView = _tableView.TableHeaderView.Subviews[0] as PodcastHeaderView;
            var frame = new CGRect(0, 0, headerView.BackgroundImageview.Frame.Width, headerView.BackgroundImageview.Frame.Height);

            if (offsetY > 0)
            {
                frame.Y = offsetY * 0.5f;
                headerView.BackgroundImageview.Frame = frame;
            }
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return section == (int)Section.Connections ? LocalConstants.Podcast_TitleCell : 0;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {

            switch (indexPath.Section)
            {
                case (int)Section.Episodes: return LocalConstants.Podcast_Episode;
                case (int)Section.About:return (int)(_podcast.Summary.Length/50) * 14 + 100; //UITableView.AutomaticDimension;
                case (int)Section.Connections: return LocalConstants.Podcast_Connections;
                default: return 0;
            }
        }
    }
}
