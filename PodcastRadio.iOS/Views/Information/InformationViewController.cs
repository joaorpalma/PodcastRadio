using System;
using CoreGraphics;
using PodcastRadio.Core.ViewModels;
using PodcastRadio.iOS.Helpers;
using PodcastRadio.iOS.Interfaces;
using PodcastRadio.iOS.Views.Base;
using UIKit;

namespace PodcastRadio.iOS.Views.Information
{
    public partial class InformationViewController : XViewController<InformationViewModel>
    {
        public override bool ShowAsPresentView => true;

        public InformationViewController() : base("InformationViewController", null) {}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupView();
        }

        private void SetupView()
        {
            UILabelExtensions.SetupLabelAppearance(_titleLabel,ViewModel.Title, Colors.White, 18f, UIFontWeight.Bold);
            UILabelExtensions.SetupLabelAppearance(_createdByLabel, ViewModel.CreatedBy, Colors.Black, 20f);
            UILabelExtensions.SetupLabelAppearance(_creatorNameLabel, ViewModel.JoaoPalma, Colors.Black, 34f);
            UILabelExtensions.SetupLabelAppearance(_copyrightLabel, ViewModel.CopyrightLabel, Colors.Black, 16f);
            _navBar.BackgroundColor = Colors.MainBlue;
            _closeButton.SetImage(UIImage.FromBundle("information_close")?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
            _closeButton.SetTitle("", UIControlState.Normal);
            _closeButton.TouchUpInside -= OnCloseButton_TouchUpInside;
            _closeButton.TouchUpInside += OnCloseButton_TouchUpInside;
        }

        private void OnCloseButton_TouchUpInside(object sender, EventArgs e)
        {
            ViewModel.CloseViewCommand.Execute();
        }
    }
}

