using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using PodcastRadio.Core.Helpers;
using PodcastRadio.Core.Models;
using PodcastRadio.Core.Serialization;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.Core.Services
{
    public class WebService : IWebService
    {
        private static readonly string _basePath = @"https://itunes.apple.com/";
        private static readonly XNamespace itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";
 
        private HttpClient _httpClient;
        private HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient(new RetryHandler(new HttpClientHandler())));

        public async Task<T> GetAsync<T>(string link, CancellationToken ct = default(CancellationToken))
        {
            //HttpClient.BaseAddress = new Uri(_basePath);
            var response = await HttpClient.GetAsync($"{_basePath}{link}", ct);
            var obj = await DeserializeAsync<T>(response);
            return obj;
        }

        public async Task<List<Podcast>> GetPodcastsAsync (string rssFeed)
        {
            var podcasts = new List<Podcast>();
            var response = await HttpClient.GetStringAsync(rssFeed);

            //Should be on a separate service
            return await Task.Run(() => {
                var xdoc = XDocument.Parse(response);

                foreach (var item in xdoc.Descendants("item"))
                {
                    var podcast = new Podcast { PodcastGUID = item.Element("guid")?.Value };
                    podcasts.Add(podcast);
                }
                Debug.WriteLine($"GUID Count: {podcasts.Count} N1: {podcasts[0].PodcastGUID}");
                return podcasts;
            });
        }

        public async Task<PodcastChannel> GetPodcastFeedAsync (string url)
        {
            var podcastChannel = new PodcastChannel { Episodes = new List<Episode>() };
            var response = await HttpClient.GetStringAsync(url);

            //Should be on a separate service
            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(response);

                foreach (var item in xdoc.Descendants("item"))
                {
                    Episode episode = new Episode
                    {
                        Title = item.Element("title")?.Value,
                        EpisodeNumber = item.Element(itunes + "episode")?.Value,
                        Duration = item.Element(itunes + "duration")?.Value,
                        Explicit = item.Element(itunes + "explicit")?.Value,
                        AudioLink = item.Element("enclosure")?.Attribute("url")?.Value,
                        PublicationDate = item.Element("pubDate")?.Value
                    };

                    podcastChannel.Episodes.Add(episode);
                }

                podcastChannel.Link = xdoc.Root.Element("channel").Element("link")?.Value;
                podcastChannel.Summary = xdoc.Root.Element("channel").Element("description")?.Value;
                Debug.WriteLine($"Episodes Count: {podcastChannel.Episodes.Count}");
                return podcastChannel;
            });
        }

        private async Task<T> DeserializeAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
                return default(T);
            
            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            var jsonConverter = new JsonConvert<T>();
            var deserializedData = JsonConvert.DeserializeObject<T>(json, jsonConverter);
            return deserializedData;
        }
    }
}
