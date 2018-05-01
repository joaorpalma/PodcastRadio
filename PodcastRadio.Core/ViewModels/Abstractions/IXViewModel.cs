using System;
using System.Threading.Tasks;

namespace PodcastRadio.Core.ViewModels.Abstractions
{
    public interface IXViewModel
    {
        void InitializeViewModel();
        Task Appearing();
        Task Disappearing();
        void Prepare();
        void Prepare(object dataObject);
    }
}
