using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using PodcastRadio.Core.Helpers;
using PodcastRadio.Core.Models;
using PodcastRadio.Core.Models.DTOs;
using PodcastRadio.Core.Serialization;
using Xunit;

namespace PodcastRadio.Tests
{
    public class RSSTests
    {
        private static string rssfeed           = "https://rss.itunes.apple.com/api/v1/us/podcasts/top-podcasts/all/10/explicit.rss";
        private static string itunesAPISearch   = "https://itunes.apple.com/lookup?id=";
        private static string guiId             = "https://itunes.apple.com/us/podcast/the-habitat/id1369393780?mt=2";
        private static string podcastfeedurl    = "https://feeds.megaphone.fm/thehabitat";

        #region Podcasts

        [Fact]
        public async Task GetTopTenPodcasts()
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(rssfeed);

            var podcasts = await ParsePodcastTopTen(responseString);
            Assert.Equal(10, podcasts.Count);
        }

        [Fact]
        public async Task GetPodcastDetails()
        {
            var httpClient = new HttpClient();
            string podcastId = RegexHelper.GetPodcastId(guiId);
            var response = await httpClient.GetAsync($"{itunesAPISearch}{podcastId}");
            var result = await Deserialize<List<PodcastModel>>(response);
            Assert.Equal("Explicit", result[0].AdvisoryRating);
        }

        [Fact]
        public async Task GetPodcastFeed()
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(podcastfeedurl);
            var podcastFeed = await ParsePodcastFeed(responseString);
            Assert.Equal("yes", podcastFeed.Episodes[0].Explicit);
        }

        #endregion


        #region Private Methods

        private async Task<PodcastChannel> ParsePodcastFeed(string responseString)
        {
            var podcastChannel = new PodcastChannel();
            podcastChannel.Episodes = new List<Episode>();

            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(responseString);

                XNamespace itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";

                foreach (var item in xdoc.Descendants("item"))
                {
                    if(item.Element(itunes + "episodeType").Value == "full")
                    {
                        var episode = new Episode
                        {
                           Title = item.Element("title").Value,
                           EpisodeNumber = item.Element(itunes + "episode").Value,
                           Duration = item.Element(itunes + "duration").Value,
                           Explicit = item.Element(itunes + "explicit").Value,
                           AudioLink = item.Element("enclosure").Attribute("url").Value
                        };

                        podcastChannel.Episodes.Add(episode);
                    }
                }
                podcastChannel.Link = xdoc.Root.Element("channel").Element("link").Value;
                podcastChannel.Summary = xdoc.Root.Element("channel").Element("description").Value;

                return podcastChannel;
             });
        }

        private async Task<List<Podcast>> ParsePodcastTopTen(string responseString)
        {
            var podcasts = new List<Podcast>(); 

            return await Task.Run(() => {
                var xdoc = XDocument.Parse(responseString);

                foreach (var item in xdoc.Descendants("item"))
                {
                    var podcast = new Podcast { PodcastGUID = item.Element("guid").Value };
                    podcasts.Add(podcast);
                }
                return podcasts;
            });
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
                return default(T);

            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            var jsonConverter = new JsonConvert<T>();
            var deserializedData = JsonConvert.DeserializeObject<T>(json, jsonConverter);
            return deserializedData;
        }

        #endregion
    }
}
