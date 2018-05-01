using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PodcastRadio.Core.Models;
using PodcastRadio.Core.Models.DTOs;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.Core.Services
{
    public class PodcastService : IPodcastService
    {
        private readonly IWebService _webService;

        public PodcastService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<List<Podcast>> GetTopTenPodcastsAsync(string podcastFeed)
        {
            return await _webService.GetPodcastsAsync(podcastFeed).ConfigureAwait(false);
        }

        public async Task<List<PodcastModel>> GetPodcastDetailAsync(string podcastId)
        {
            return await _webService.GetAsync<List<PodcastModel>>($"lookup?id={podcastId}").ConfigureAwait(false);
        }

        public async Task<PodcastChannel> GetPodcastFeedAsync(string url)
        {
            return await _webService.GetPodcastFeedAsync(url).ConfigureAwait(false);
        }

        public async Task<List<PodcastModel>> SearchPodcastAsync(string search)
        {
            return await _webService.GetAsync<List<PodcastModel>>($"search?term={search}&entity=podcast&limit=20").ConfigureAwait(false);
            //Task<List<PodcastModel>> GetPodcastDetailAsync(string podcastId);
        }
    }
}
