using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PodcastRadio.Core.Services;
using PodcastRadio.Core.ViewModels.Abstractions;
using PodcastRadio.iOS.Interfaces;
using PodcastRadio.iOS.Views.Base;
using PodcastRadio.Core.Helpers;
using UIKit;
using PodcastRadio.iOS.Views.Information;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.iOS.Services
{
    public class XiOSNavigationService : XNavigationService
    {
        public UINavigationController MasterNavigationController;

        public override Task NavigatePlatformAsync<TViewModel>()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var viewController = CreateViewControllerForViewModel<TViewModel, object>(null);
                ShowView(viewController);
            });

            return Task.CompletedTask;
        }

        public override Task NavigatePlatformAsync<TViewModel, TObject>(TObject data)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var viewController = CreateViewControllerForViewModel<TViewModel, TObject>(data);
                ShowView(viewController);
            });

            return  Task.CompletedTask;
        }

        private UIViewController CreateViewControllerForViewModel<TViewModel, TObject>(TObject data) where TViewModel : class, IXViewModel
        {
            var vmType = typeof(TViewModel);
            var view = GetViewForViewModel(typeof(TViewModel));
            var viewController = Activator.CreateInstance(view) as UIViewController;

            Debug.WriteLine($"Final ViewModel: {vmType} for viewcontroller: {viewController}");

            var xVC = viewController as IXiOSView;
            if (xVC != null)
                xVC.ParameterData = data;

            return viewController;
        }

        private void ShowView(UIViewController vc)
        {
            if (MasterNavigationController == null)
            {
                MasterNavigationController = new UINavigationController();
                var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
                appDelegate.Window.RootViewController = MasterNavigationController;
                appDelegate.NavigationController = MasterNavigationController;
            }

            if(vc is IPresentView)
                MasterNavigationController.PresentViewController(vc, true, null);
            else
                MasterNavigationController.PushViewController(vc, true);
        }

        public override Task Close<TViewModel>(TViewModel viewModel)
        {
            var vc = GetViewForViewModel(typeof(TViewModel));

            if (typeof(IPresentView).IsAssignableFrom(vc))
                MasterNavigationController.DismissViewController(true, null);
            else
                MasterNavigationController.PopViewController(true);
            
            return Task.CompletedTask;
        }

        protected override IEnumerable<Type> GetPlatformMainViewTypes()
        {
            var repositoryAssembly = typeof(XiOSNavigationService).Assembly;

            var controllers =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace.Contains("PodcastRadio.iOS.Views")
                where !type.IsAbstract && type.IsSubclassOfRawGeneric(typeof(XViewController<>))
                select type;

            Debug.WriteLine(controllers);

            return controllers;
        }
    }
}
