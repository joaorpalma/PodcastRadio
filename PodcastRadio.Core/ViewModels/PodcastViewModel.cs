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
        private readonly IRadioPlayer _radioPlayer;
        private Episode _lastPlayingEpisode;
        public string PodcastName { get; set; }

        private PodcastModel _podcast;
        public PodcastModel Podcast
        {
            get { return _podcast; }
            set { SetProperty(ref _podcast, value); }
        }

        private XPCommand _openSharePodcastCommand;
        public XPCommand OpenSharePodcastCommand => _openSharePodcastCommand ?? (_openSharePodcastCommand = new XPCommand(() => OpenSharePodcast()));

        private XPCommand<string> _openWebsiteCommand;
        public XPCommand<string> OpenWebsiteCommand => _openWebsiteCommand ?? (_openWebsiteCommand = new XPCommand<string>((link) => OpenWebsite(link)));

        private XPCommand<Episode> _playEpisodeCommand;
        public XPCommand<Episode> PlayEpisodeCommand => _playEpisodeCommand ?? (_playEpisodeCommand = new XPCommand<Episode>(async (episode) => await PlayEpisode(episode), CanExecute));

        public PodcastViewModel(IPodcastService podcastService, IDialogService dialogService, IConnectService connectService, IRadioPlayer radioPlayer)
        {
            _podcastService = podcastService;
            _dialogService = dialogService;
            _connectService = connectService;
            _radioPlayer = radioPlayer;
        }

        protected override void Prepare(PodcastModel podcast)
        {
            PodcastName = podcast.PodcastName;
            _podcast = podcast;
        }

        public override async Task Appearing()
        {
            if(LocationResources?.Count == 0)
                SetL10NResources();

            if(Podcast.Channel == null)
            {
                IsBusy = true;
                _dialogService.ShowLoading();
                try
                {
                    var podcast = Podcast;
                    _podcast = null;

                    if (!string.IsNullOrEmpty(podcast.FeedUrl))
                    {
                        podcast.Channel = await _podcastService.GetPodcastFeedAsync(podcast.FeedUrl);
                        podcast.Channel = ReflectionHelper.ConvertNullToEmpty(podcast.Channel);
                        //podcast.Channel.Episodes = podcast.Channel.Episodes.OrderByDescending(x => x.EpisodeNumber).ToList();

                        Podcast = podcast;

                        Debug.WriteLine($"Number of tracks: {Podcast.NumberTracks}, Count: {podcast.Channel.Episodes.Count}");
                    }
                    else
                    {
                        //add some error message...
                        await NavService.Close(this);
                    }
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

        private async Task PlayEpisode(Episode episode)
        {
            //_dialogService.ShowLoading();
            IsBusy = true;
            try
            {
                if (episode == _lastPlayingEpisode)
                {
                    if (episode.IsPlaying == false)
                    {
                        episode.IsPlaying = true;
                        await _radioPlayer.UnPause();
                    }
                    else
                    {
                        episode.IsPlaying = false;
                        await _radioPlayer.Pause();
                    }
                }
                else
                {
                    if (_lastPlayingEpisode != null)
                        _lastPlayingEpisode.IsPlaying = false;

                    episode.IsPlaying = true;
                    await _radioPlayer.Play(episode.AudioLink);

                    _lastPlayingEpisode = episode;
                }
            }
            catch (Exception ex)
            {
                Ui.Handle(ex as dynamic);
            }
            finally
            {
                //_dialogService.HideLoading();
                IsBusy = false;
                RaisePropertyChanged(nameof(Podcast));
            }
        }

        private void OpenWebsite(string link)
        {
            _connectService.OpenWebLink(link);
        }

        private bool CanExecute(object obj) => !IsBusy;

        #region resources

        public Dictionary<string, string> LocationResources = new Dictionary<string, string>();

        private string _trackLabel => L10N.Localize("PodcastViewModel_Track");
        private string _nameDurationLabel => L10N.Localize("PodcastViewModel_NameDuration");
        private string _playLabel => L10N.Localize("PodcastViewModel_Play");
        private string _durationLabel => L10N.Localize("PodcastViewModel_Duration");
        private string _playingLabel => L10N.Localize("PodcastViewModel_Playing");
        private string _releasedLabel => L10N.Localize("PodcastViewModel_Released");
        private string _aboutLabel => L10N.Localize("PodcastViewModel_About");
        private string _websiteLabel => L10N.Localize("PodcastViewModel_Website");

		private void SetL10NResources()
        {
            LocationResources.Add("TrackLabel", _trackLabel);
            LocationResources.Add("NameDurationLabel", _nameDurationLabel);
            LocationResources.Add("PlayLabel", _playLabel);
            LocationResources.Add("DurationLabel", _durationLabel);
            LocationResources.Add("PlayingLabel", _playingLabel);
            LocationResources.Add("ReleasedLabel", _releasedLabel);
            LocationResources.Add("AboutLabel", _aboutLabel);
            LocationResources.Add("WebsiteLabel", _websiteLabel);
        }

        #endregion
    }
}
