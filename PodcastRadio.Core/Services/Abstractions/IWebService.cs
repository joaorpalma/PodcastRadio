using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PodcastRadio.Core.Models;

namespace PodcastRadio.Core.Services.Abstractions
{
    public interface IWebService
    {
        Task<List<Podcast>> GetPodcastsAsync(string podcastsFeed);

        Task<T> GetAsync<T> (string podcastId, CancellationToken cancellationToken = default(CancellationToken));

        Task<PodcastChannel> GetPodcastFeedAsync(string url);
    }
}
