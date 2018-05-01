using System;
using System.ComponentModel;
using System.Diagnostics;
using CoreGraphics;
using CoreText;
using Foundation;
using PodcastRadio.Core.Models.DTOs;
using PodcastRadio.Core.ViewModels;
using PodcastRadio.iOS.Helpers;
using PodcastRadio.iOS.Models;
using PodcastRadio.iOS.Sources;
using PodcastRadio.iOS.Views.Base;
using UIKit;

namespace PodcastRadio.iOS.Views.Main
{
    public partial class MainViewController : XViewController<MainViewModel>, IUITableViewDelegate, IUISearchResultsUpdating
    {
        public MainViewController() : base("MainViewController", null) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ConfigureView();

            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.PodcastDetail):
                    if (ViewModel.PodcastDetail?.Count >= 1)
                    {
                        _tableView.Hidden = false;
                        _pickerView.Model = new CategoryPickerModel(ViewModel.Categories, OnSelectCategory);
                        SetTableView(!ViewModel.IsSearching);
                    }
                    break;

                default:
                    break;
            }
        }

        private void OpenInformationView(object sender, EventArgs e)
        {
            ViewModel.OpenInformationViewCommand.Execute();
        }

        private void SetTableView(bool showHeaderPicker)
        {
            var source = new MainSource(_tableView, ViewModel.PodcastDetail, ViewModel.LocationResources, ViewModel.SelectedCategory, showHeaderPicker);
            _tableView.Source = source;

            source.OnHeaderPressedEvent -= Source_OnHeaderPressedEvent;
            source.OnHeaderPressedEvent += Source_OnHeaderPressedEvent;
            source.OnOpenPodcastEvent -= Source_OnOpenPodcastEvent;
            source.OnOpenPodcastEvent += Source_OnOpenPodcastEvent;

            _tableView.ReloadData();
            _tableView.SetContentOffset(new CGPoint(0, 0), false);
        }

        private void OnSelectCategory(object sender, string category)
        {
            PickerSwitch(true);

            if (ViewModel.SetPodcastCategoryCommand.CanExecute(category))
                ViewModel.SetPodcastCategoryCommand.Execute(category);
        }

        private void Source_OnOpenPodcastEvent(object sender, PodcastModel podcast)
        {
            if (ViewModel.OpenPodcastViewCommand.CanExecute(null))
                ViewModel.OpenPodcastViewCommand.Execute(podcast);
        }

        private void Source_OnHeaderPressedEvent(object sender, EventArgs e)
        {
            PickerSwitch(false);
        }

        private void PickerSwitch(bool hidePicker)
        {
            _pickerView.Hidden = hidePicker;
            _pickerHeaderView.Hidden = hidePicker;
            _closeTabView.Hidden = hidePicker;
            _closeTabView.UserInteractionEnabled = !hidePicker;
        }

        private void ConfigureView()
        {
            this.NavigationController.NavigationBar.PrefersLargeTitles = true;
            NavigationItem.RightBarButtonItem = UIButtonExtensions.SetupImageBarButton(24, "main_info", OpenInformationView);

            var search = new UISearchController(searchResultsController: null)
            {
                DimsBackgroundDuringPresentation = false,
            };

            search.SearchBar.TintColor = UIColor.White;
            search.SearchBar.Placeholder = ViewModel.Search;

            NSString _searchField = new NSString("searchField");
            var textFieldInsideSearchBar = (UITextField)search.SearchBar.ValueForKey(_searchField);
            var backgroundField = textFieldInsideSearchBar.Subviews[0];
            backgroundField.BackgroundColor = Colors.White.ColorWithAlpha(1);
            backgroundField.Alpha = 1;
            backgroundField.Layer.CornerRadius = 10f;
            backgroundField.ClipsToBounds = true;

            _pickerHeaderView.BackgroundColor = Colors.MainBlue;
            _pickerHeaderView.Layer.ShadowColor = Colors.Black.CGColor;
            _pickerHeaderView.Layer.ShadowOffset = new CGSize(5, 5);
            _pickerHeaderView.Layer.ShadowRadius = 7;
            _pickerHeaderView.Layer.ShadowOpacity = 12;
            _pickerHeaderView.Layer.MasksToBounds = false;

            search.SearchBar.OnEditingStarted -= OnSearchBar_OnEditingStarted;
            search.SearchBar.OnEditingStarted += OnSearchBar_OnEditingStarted;

            search.SearchBar.OnEditingStopped -= OnSearchBar_OnEditingStopped;
            search.SearchBar.OnEditingStopped += OnSearchBar_OnEditingStopped;

            search.SearchBar.CancelButtonClicked -= OnSearchBar_CancelButtonClicked;
            search.SearchBar.CancelButtonClicked += OnSearchBar_CancelButtonClicked;

            search.SearchResultsUpdater = this;
            DefinesPresentationContext = true;
            NavigationItem.SearchController = search;

            _tableView.Hidden = true;
            PickerSwitch(true);

            UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(() => PickerSwitch(true));
            _closeTabView.AddGestureRecognizer(tapGesture);
            _closeTabView.Alpha = 0.4f;

        }

        private void OnSearchBar_OnEditingStopped(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ViewModel.SearchPodcast))
            {
                if (ViewModel.PodcastSearchCommand.CanExecute())
                    ViewModel.PodcastSearchCommand.Execute();
            }
        }

        private void OnSearchBar_OnEditingStarted(object sender, EventArgs e)
        {
            SetTableView(false);
        }

        private void OnSearchBar_CancelButtonClicked(object sender, EventArgs e)
        {
            if(ViewModel.CleanSearchCommand.CanExecute())
                ViewModel.CleanSearchCommand.Execute();
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            ViewModel.SearchPodcast = searchController.SearchBar.Text;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.Title = ViewModel.Title;
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
            NavigationItem.HidesSearchBarWhenScrolling = false;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            NavigationItem.HidesSearchBarWhenScrolling = true;
            NavigationItem.SearchController.SearchBar.EndEditing(true);
            PickerSwitch(true);
        }
    }
}

