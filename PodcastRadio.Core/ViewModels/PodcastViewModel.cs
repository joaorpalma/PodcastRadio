using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PodcastRadio.Core.Exceptions;
using PodcastRadio.Core.Helpers;
using PodcastRadio.Core.Helpers.Commands;
using PodcastRadio.Core.Language;
using PodcastRadio.Core.Models;
using PodcastRadio.Core.Models.DTOs;
using PodcastRadio.Core.Services.Abstractions;
using PodcastRadio.Core.ViewModels.Abstractions;

namespace PodcastRadio.Core.ViewModels
{
    public class PodcastViewModel : XViewModel<PodcastModel>
    {
        private readonly IPodcastService _podcastService;
        private readonly IDialogService _dialogService;
        private readonly IConnectService _connectService;

        public string PodcastName { get; set; }

        private PodcastModel _podcast;
        public PodcastModel Podcast
        {
            get { return _podcast; }
            set { SetProperty(ref _podcast, value); }
        }

        private XPCommand _openSharePodcastCommand;
        public XPCommand OpenSharePodcastCommand => _openSharePodcastCommand ?? (_openSharePodcastCommand = new XPCommand(() => OpenSharePodcast()));

        public PodcastViewModel(IPodcastService podcastService, IDialogService dialogService, IConnectService connectService)
        {
            _podcastService = podcastService;
            _dialogService = dialogService;
            _connectService = connectService;
        }

        protected override void Prepare(PodcastModel podcast)
        {
            PodcastName = podcast.PodcastName;
            _podcast = podcast;
        }

        public override async Task InitializeAsync()
        {
            if(LocationResources == null)
                SetL10NResources();

            if(Podcast.Channel == null)
            {
                IsBusy = true;
                _dialogService.ShowLoading();
                try
                {
                    var podcast = Podcast;
                    _podcast = null;

                    podcast.Channel = await _podcastService.GetPodcastFeedAsync(podcast?.FeedUrl);
                    podcast.Channel = ReflectionHelper.ConvertNullToEmpty(podcast.Channel);
                    podcast.Channel.Episodes = podcast.Channel.Episodes.OrderByDescending(x => x.EpisodeNumber).ToList();

                    Podcast = podcast;

                    Debug.WriteLine($"Number of tracks: {Podcast.NumberTracks}, Count: {podcast.Channel.Episodes.Count}");
                }
                catch (Exception ex)
                {
                    Ui.Handle(ex as dynamic);
                }
                finally
                {
                    IsBusy = false;
                    _dialogService.HideLoading();
                }
            }
            else
            {
                RaisePropertyChanged(nameof(Podcast));
            }
        }

        private void OpenSharePodcast()
        {
            _connectService.SharePodcast(Podcast);
        }

        #region resources

        public Dictionary<string, string> LocationResources = new Dictionary<string, string>();

        private string _trackLabel => L10N.Localize("PodcastViewModel_Track");
        private string _nameDurationLabel => L10N.Localize("PodcastViewModel_NameDuration");
        private string _playLabel => L10N.Localize("PodcastViewModel_Play");
        private string _minutesLabel => L10N.Localize("PodcastViewModel_minutes");
        private string _toFinishLabel => L10N.Localize("PodcastViewModel_ToFinish");
        private string _releasedLabel => L10N.Localize("PodcastViewModel_Released");
        private string _aboutLabel => L10N.Localize("PodcastViewModel_About");
        private string _websiteLabel => L10N.Localize("PodcastViewModel_Website");


        private void SetL10NResources()
        {
            LocationResources.Add("TrackLabel", _trackLabel);
            LocationResources.Add("NameDurationLabel", _nameDurationLabel);
            LocationResources.Add("PlayLabel", _playLabel);
            LocationResources.Add("MinutesLabel", _minutesLabel);
            LocationResources.Add("ToFinishLabel", _toFinishLabel);
            LocationResources.Add("ReleasedLabel", _releasedLabel);
            LocationResources.Add("AboutLabel", _aboutLabel);
            LocationResources.Add("WebsiteLabel", _websiteLabel);
        }

        #endregion
    }
}
