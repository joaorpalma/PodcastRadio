using System;
using System.Threading.Tasks;

namespace PodcastRadio.Core.Services.Abstractions
{
    public interface IRadioPlayer
    {
        Task Play(string link);
        Task Pause();
        Task UnPause();
        Task Stop();
    }
}
