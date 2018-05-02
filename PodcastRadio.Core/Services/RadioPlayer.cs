using System;
using System.Threading.Tasks;
using Plugin.MediaManager;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.Core.Services
{
    public class RadioPlayer : IRadioPlayer
    {
        public async Task Play(string link)
        {
            await CrossMediaManager.Current.Play(link);
        }

        public async Task UnPause()
        {
            await CrossMediaManager.Current.Play();
        }

        public async Task Pause()
        {
            await CrossMediaManager.Current.Pause();
        }
    }
}
