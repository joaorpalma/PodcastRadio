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
    public class MainViewModel : XViewModel
    {
        private IDialogService _dialogService;
        private IPodcastService _podcastService;
        private List<Podcast> _podcastsGUID;
        private List<PodcastModel> _savedPodcastList;

        public bool IsSearching { get; private set; }
        public string SearchPodcast { get; set; }
        public string SelectedCategory { get; private set; }

        private List<string> _categories;
        public List<string> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        private List<PodcastModel> _podcastDetail;
        public List<PodcastModel> PodcastDetail
        {
            get { return _podcastDetail; }
            set { SetProperty(ref _podcastDetail, value); }
        }

        private XPCommand _openInformationViewCommand;
        public XPCommand OpenInformationViewCommand => _openInformationViewCommand ?? (_openInformationViewCommand = new XPCommand(async () => await OpenInformationView()));

        private XPCommand<PodcastModel> _openPodcastViewCommand;
        public XPCommand<PodcastModel> OpenPodcastViewCommand => _openPodcastViewCommand ?? (_openPodcastViewCommand = new XPCommand<PodcastModel>(async (podcast) => await OpenPodcastView(podcast), CanExecute));

        private XPCommand<string> _setPodcastCategoryCommand;
        public XPCommand<string> SetPodcastCategoryCommand => _setPodcastCategoryCommand ?? (_setPodcastCategoryCommand = new XPCommand<string>((string category) => SetPodcastCategory(category), CanExecute));

        private XPCommand _podcastSearchCommand;
        public XPCommand PodcastSearchCommand => _podcastSearchCommand ?? (_podcastSearchCommand = new XPCommand(async () => await PodcastSearch(), CanExecute));

        private XPCommand _cleanSeachCommand;
        public XPCommand CleanSearchCommand => _cleanSeachCommand ?? (_cleanSeachCommand = new XPCommand(() => CleanSearch(), CanExecute));

        public MainViewModel(IPodcastService podcastService, IDialogService dialogService)
        {
            _podcastService = podcastService;
            _dialogService = dialogService;
        }

        public override async Task InitializeAsync()
        {
            SetL10NResources();
            IsBusy = true;
            _dialogService.ShowLoading();
            var podcastDetails = new List<PodcastModel>();
            _podcastsGUID = new List<Podcast>();
            Categories = new List<string>();
            SelectedCategory = _allLabel;

            try
            {
                _podcastsGUID = await _podcastService.GetTopTenPodcastsAsync(RSSFeed.TopTenPodcasts);
                foreach (Podcast podcast in _podcastsGUID)
                {
                    string podGUID = RegexHelper.GetPodcastId(podcast.PodcastGUID);
                    var pcDetail = await _podcastService.GetPodcastDetailAsync(podGUID);
                    var podCast = ReflectionHelper.ConvertNullToEmpty(pcDetail[0]);

                    podcastDetails.Add(podCast);
                    Categories.Add(podCast.PrimaryGenre);
                }
                Categories.Add(_allLabel);
                Categories = Categories.Distinct().ToList().OrderBy(x => x).ToList();
                PodcastDetail = podcastDetails.OrderByDescending(x => x.ReleaseDate).ToList();
                _savedPodcastList = PodcastDetail;

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

        private void SetPodcastCategory(string category)
        {
            SelectedCategory = category;

            if (category == _allLabel)
                PodcastDetail = _savedPodcastList;
            else
                PodcastDetail = _savedPodcastList.FindAll(x => x.PrimaryGenre == category);
        }

        private async Task PodcastSearch()
        {
            IsSearching = true;
            IsBusy = true;
            _dialogService.ShowLoading();
            try
            {
                var searchedPodcasts = await _podcastService.SearchPodcastAsync(SearchPodcast);
                var podCasts = ReflectionHelper.ConvertNullToEmpty(searchedPodcasts);
                //podCasts = podCasts.OrderByDescending(x => x.ReleaseDate).ToList();
                PodcastDetail = podCasts;
                Debug.WriteLine($"PodcastDetail Count: {PodcastDetail.Count}");
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

        private void CleanSearch()
        {
            Debug.WriteLine("Clean");
            SearchPodcast = string.Empty;
            IsSearching = false;
            _podcastDetail = null;
            PodcastDetail = _savedPodcastList;
        }

        private async Task OpenInformationView()
        {
            await NavService.NavigateAsync<InformationViewModel>();
        }

        private async Task OpenPodcastView(PodcastModel podcast)
        {
            await NavService.NavigateAsync<PodcastViewModel, PodcastModel>(podcast);
        }

        private bool CanExecute(object arg) => !IsBusy;
        private bool CanExecute() => !IsBusy;

        #region resources

        public Dictionary<string, string> LocationResources = new Dictionary<string, string>();

        public string Title => L10N.Localize("MainViewModel_Title");
        public string Search => L10N.Localize("MainViewModel_Search");

        private string _categoryLabel => L10N.Localize("MainViewModel_Category");
        private string _allLabel => L10N.Localize("MainViewModel_All");
        private string _tracksLabel => L10N.Localize("MainViewModel_Tracks");
        private string _byLabel => L10N.Localize("MainViewModel_By");

        public void SetL10NResources()
        {
            LocationResources.Add("CategoryLabel", _categoryLabel);
            LocationResources.Add("AllLabel", _allLabel);
            LocationResources.Add("TracksLabel", _tracksLabel);
            LocationResources.Add("ByLabel", _byLabel);
        }

        #endregion
    }
}
