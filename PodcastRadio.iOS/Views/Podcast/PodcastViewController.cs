using System;
using System.ComponentModel;
using PodcastRadio.Core.ViewModels;
using PodcastRadio.iOS.Helpers;
using PodcastRadio.iOS.Views.Base;
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
            throw new NotImplementedException();
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
		}

		private void SharePodcast(object sender, EventArgs e)
        {
            ViewModel.OpenSharePodcastCommand.Execute();
        }
	}
}

