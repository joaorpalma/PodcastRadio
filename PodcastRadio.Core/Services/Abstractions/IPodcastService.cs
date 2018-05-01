using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PodcastRadio.Core.Models;
using PodcastRadio.Core.Models.DTOs;

namespace PodcastRadio.Core.Services.Abstractions
{
    public interface IPodcastService
    {
        Task<List<Podcast>> GetTopTenPodcastsAsync(string podcastFeed);
        Task<List<PodcastModel>> GetPodcastDetailAsync(string podcastId);
        Task<List<PodcastModel>> SearchPodcastAsync(string search);
        Task<PodcastChannel> GetPodcastFeedAsync(string url);
    }
}
