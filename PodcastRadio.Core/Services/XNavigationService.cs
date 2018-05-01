using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using PodcastRadio.Core.Services.Abstractions;
using PodcastRadio.Core.ViewModels.Abstractions;
using static PodcastRadio.Core.Helpers.ReflectionHelper;

namespace PodcastRadio.Core.Services
{
    public abstract class XNavigationService : IXNavigationService
    {
        private readonly Dictionary<Type, Type> ViewModelsForView = new Dictionary<Type, Type>();
        protected abstract IEnumerable<Type> GetPlatformMainViewTypes();

        public void Initialize()
        {
            CreateViewModelPlatformViewAssociation();
        }

        private void CreateViewModelPlatformViewAssociation()
        {
            var platformViews = GetPlatformMainViewTypes();

            foreach (var platformView in platformViews)
            {
                var vm = GetBaseGenericType(platformView);
                if (vm == null || !vm.GetTypeInfo().IsSubclassOf(typeof(XViewModel)))
                {
                    Debugger.Break(); //Found a view that isn't associated with a viewmodel or not of base type xviewmodel
                    continue;
                }
                Debug.WriteLine($"ViewModel: {vm} for View: {platformView}");

                ViewModelsForView[vm] = platformView;
            }
        }

        protected Type GetViewForViewModel(Type viewModel)
        {
            Debug.WriteLine(viewModel);
            if (!ViewModelsForView.ContainsKey(viewModel))
            {
                Debugger.Break();
                throw new SystemException("This viewmodel does not have a registered view");
            }

            return ViewModelsForView[viewModel];
        }

        public async Task NavigateAsync<TViewModel>() where TViewModel : class, IXViewModel
        {
            await NavigatePlatformAsync<TViewModel>();
        }
        public async Task NavigateAsync<TViewModel, TObject>(TObject data) where TViewModel : class, IXViewModel
        {
            await NavigatePlatformAsync<TViewModel, TObject>(data);
        }

        public abstract Task NavigatePlatformAsync<TViewModel>() where TViewModel : class, IXViewModel;
        public abstract Task NavigatePlatformAsync<TViewModel, TMyObject>(TMyObject data) where TViewModel : class, IXViewModel;
        public abstract Task Close<TViewModel>() where TViewModel : class, IXViewModel;
    }
}
