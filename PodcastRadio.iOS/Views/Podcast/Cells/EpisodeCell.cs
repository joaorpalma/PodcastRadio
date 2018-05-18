using System;
using System.Collections.Generic;
using Foundation;
using PodcastRadio.Core.Helpers;
using PodcastRadio.Core.Models;
using PodcastRadio.Core.Models.DTOs;
using PodcastRadio.iOS.Helpers;
using UIKit;

namespace PodcastRadio.iOS.Views.Podcast.Cells
{
    public partial class EpisodeCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EpisodeCell");
        public static readonly UINib Nib = UINib.FromName("EpisodeCell", NSBundle.MainBundle);
        protected EpisodeCell(IntPtr handle) : base(handle) {}

        protected EventHandler<Episode> _playPressed;
        private Episode _episode;
        private Dictionary<string, string> _locationResources;

        public void Configure(Episode episode, Dictionary<string, string> locationResources, EventHandler<Episode> playPressed)
        {
            _episode = episode;
            _playPressed = playPressed;
            _locationResources = locationResources;
            _loadingView.Hidden = true;
            _activityIndicator.StopAnimating();

			_playImage.Image?.Dispose();
			_explicit.Image?.Dispose();

            UILabelExtensions.SetupLabelAppearance(_nameLabel, episode.Title, Colors.Black, 14f, UIFontWeight.Semibold);
            UILabelExtensions.SetupLabelAppearance(_trackLabel, episode.EpisodeNumber, Colors.Black, 16f, UIFontWeight.Semibold);

            SetEpisode(episode.IsPlaying);
            
            if(episode.Explicit == "yes")
            {
                _explicit.Hidden = false;
                _explicit.Image = UIImage.FromBundle("main_explicit");
            }
            else
            {
                _explicit.Hidden = true;
            }

            _playButton.TouchUpInside -= OnplayButton_TouchUpInside;
            _playButton.TouchUpInside += OnplayButton_TouchUpInside;
        }

        private void OnplayButton_TouchUpInside(object sender, EventArgs e)
        {
            _loadingView.Hidden = false;
            _activityIndicator.StartAnimating();
            _playPressed?.Invoke(this, _episode);
        }

        private void SetEpisode(bool isPlaying)
        {
            if (isPlaying)
            {
                UILabelExtensions.SetupLabelAppearance(_durationLabel, _locationResources["PlayingLabel"], UIColor.LightGray, 12f, UIFontWeight.Regular);
                _playImage.Image = UIImage.FromBundle("podcast_pause");
            }
            else
            {
                DateTime parsedDate = DateTime.Parse(_episode?.PublicationDate ?? default(DateTime).ToString());
                var resultDate = TimeUtils.SetMonthAndYearFormat(parsedDate);

                string duration = "";

                if (!string.IsNullOrEmpty(_episode?.Duration))
                {
                    if (_episode.Duration.Contains(":"))
                        duration = _episode.Duration;
                    else
                        duration = TimeHelper.SecondsToDuration(_episode.Duration);

                    duration += $" {_locationResources["DurationLabel"]} | ";
                }

                duration = $"{duration}{_locationResources["ReleasedLabel"]} {resultDate[0]} {resultDate[1]}";
                UILabelExtensions.SetupLabelAppearance(_durationLabel, duration, UIColor.LightGray, 12f, UIFontWeight.Regular);
                

                _playImage.Image = UIImage.FromBundle("podcast_play");
            }
        }
    }
}
