using System;
using System.ComponentModel;
using CoreGraphics;
using PodcastRadio.Core.Models;
using PodcastRadio.Core.ViewModels;
using PodcastRadio.iOS.Helpers;
using PodcastRadio.iOS.Sources;
using PodcastRadio.iOS.Views.Base;
using PodcastRadio.iOS.Views.CustomViews;
using UIKit;

namespace PodcastRadio.iOS.Views.Podcast
{
    public partial class PodcastViewController : XViewController<PodcastViewModel>
    {
        public PodcastViewController() : base("PodcastViewController", null) {}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupView();
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

		private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.Podcast):
                    SetTableView();
                    break;

                default:
                    break;
            }
        }

        private void SetTableView()
        {
            var source = new PodcastSource(_tableView, ViewModel.Podcast.Channel, ViewModel.LocationResources);
            _tableView.Source = source;
            _tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            _tableView.TableHeaderView = new UIView(new CGRect(0, 0, 0, PodcastHeaderView.Height));

            //_tableView.RowHeight = UITableView.AutomaticDimension;
            //_tableView.EstimatedRowHeight = 50f; 

            var podcastTableHeader = PodcastHeaderView.Create();
            podcastTableHeader.Configure(ViewModel.Podcast.ArtworkLarge);
            podcastTableHeader.Frame = _tableView.TableHeaderView.Frame;
            _tableView.TableHeaderView.AddSubview(podcastTableHeader);

            source.OnPlayPressEvent -= OnSource_PlayPressEvent;
            source.OnPlayPressEvent += OnSource_PlayPressEvent;

            source.OnWebSiteClickEvent -= OnSource_WebSiteClickEvent;
            source.OnWebSiteClickEvent += OnSource_WebSiteClickEvent;

            _tableView.ReloadData();
        }

        private void OnSource_WebSiteClickEvent(object sender, string link)
        {
            ViewModel.OpenWebsiteCommand.Execute(link);
        }

        private void OnSource_PlayPressEvent(object sender, Episode episode)
        {
            if (ViewModel.PlayEpisodeCommand.CanExecute(null))
                ViewModel.PlayEpisodeCommand.Execute(episode);
        }

        private void SetupView()
        {
            NavigationController.NavigationBar.TopItem.Title = string.Empty;
            NavigationController.NavigationBar.TintColor = Colors.White;
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
            NavigationItem.RightBarButtonItem = UIButtonExtensions.SetupImageBarButton(24, "podcast_share", SharePodcast);
        }

		public override void ViewWillAppear(bool animated)
		{
            base.ViewWillAppear(animated);
            this.Title = ViewModel.PodcastName;
            _tableView.ReloadData();
		}

		private void SharePodcast(object sender, EventArgs e)
        {
            ViewModel.OpenSharePodcastCommand.Execute();
        }
	}
}

