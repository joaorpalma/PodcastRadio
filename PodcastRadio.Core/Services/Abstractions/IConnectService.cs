using System;
using PodcastRadio.Core.Models.DTOs;

namespace PodcastRadio.Core.Services.Abstractions
{
    public interface IConnectService
    {
        void SharePodcast(PodcastModel podcast);
        void OpenWebLink(string link);
    }
}
