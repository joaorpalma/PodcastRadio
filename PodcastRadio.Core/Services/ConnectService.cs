using Plugin.Share;
using Plugin.Share.Abstractions;
using PodcastRadio.Core.Models.DTOs;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.Core.Services
{
    public class ConnectService : IConnectService
    {
        public void OpenWebLink(string link)
        {
            if (!CrossShare.IsSupported) 
                return;

            CrossShare.Current.OpenBrowser(link);
        }

        public void SharePodcast(PodcastModel podcast)
        {
            if (!CrossShare.IsSupported) 
                return;
            
           CrossShare.Current.Share(new ShareMessage() {Title = podcast.Name, Text = podcast.PodcastName, Url = podcast.PodcastUrl});
        }
    }
}
