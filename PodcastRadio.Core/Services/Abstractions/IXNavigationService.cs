using System;
using System.Threading.Tasks;
using PodcastRadio.Core.ViewModels.Abstractions;

namespace PodcastRadio.Core.Services.Abstractions
{
    public interface IXNavigationService
    {
        void Initialize();
        Task NavigateAsync<TViewModel>() where TViewModel : class, IXViewModel;
        Task NavigateAsync<TViewModel, TObject>(TObject data) where TViewModel : class, IXViewModel;
        Task Close<TViewModel>(TViewModel viewModel) where TViewModel : class, IXViewModel;
    }
}

