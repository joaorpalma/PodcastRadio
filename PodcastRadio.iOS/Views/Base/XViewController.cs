using System;
using CoreGraphics;
using Foundation;
using PodcastRadio.Core;
using PodcastRadio.Core.ViewModels.Abstractions;
using PodcastRadio.iOS.Helpers;
using PodcastRadio.iOS.Interfaces;
using UIKit;

namespace PodcastRadio.iOS.Views.Base
{
    public abstract class XViewController<TViewModel> : UIViewController, IXiOSView where TViewModel : class, IXViewModel
    {
        public TViewModel ViewModel { get; private set; }
        public Type RequestedViewModel { get; set; }
        public object ParameterData { get; set; }
        public virtual bool HideNavigationBar => false;

        public XViewController(string nibName, NSBundle bundle) : base(nibName, bundle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel = App.Container.GetInstance<TViewModel>();

            if (ParameterData != null)
                ViewModel.Prepare(ParameterData);
            else
                ViewModel.Prepare();

            ViewModel.InitializeViewModel();
            RestrictRotation();
            this.EdgesForExtendedLayout = UIRectEdge.None;
        }

        public override void ViewWillAppear(bool animated)
        {
            ViewModel.Appearing();
            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            ViewModel.Disappearing();
            base.ViewWillDisappear(animated);
        }

        private void RestrictRotation()
        {
            var app = (AppDelegate)UIApplication.SharedApplication.Delegate;
            app.RestrictRotation = true;
        }
    }
}
