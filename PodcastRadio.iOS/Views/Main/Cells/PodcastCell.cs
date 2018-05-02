using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoreGraphics;
using FFImageLoading;
using Foundation;
using PodcastRadio.Core.Helpers;
using PodcastRadio.Core.Language;
using PodcastRadio.Core.Models.DTOs;
using PodcastRadio.iOS.Helpers;
using UIKit;

namespace PodcastRadio.iOS.Views.Main.Cells
{
    public partial class PodcastCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("PodcastCell");
        public static readonly UINib Nib = UINib.FromName("PodcastCell", NSBundle.MainBundle);
        protected PodcastCell(IntPtr handle) : base(handle) {}

        public void Configure(PodcastModel podcast, Dictionary<string, string> locationResources)
        {
            DateTime parsedDate = DateTime.Parse(podcast?.ReleaseDate ?? default(DateTime).ToString());
            var resultDate = TimeUtils.SetMonthAndYearFormat(parsedDate);

            ImageService.Instance.LoadUrl(podcast?.ArtworkSmall).Retry(3, 200).Into(_logoImage); 
            _logoImage.Layer.CornerRadius = 12;
            _logoImage.Layer.MasksToBounds = true;

            if (podcast?.AdvisoryRating == "Explicit")
            {
                _contentTypeImage.Hidden = false;
                _contentTypeImage.Image = UIImage.FromBundle("main_explicit");
            }
            else
            {
                _contentTypeImage.Hidden = true;
            }

            _flagImage.Layer.ShadowColor = Colors.Black.CGColor;
            _flagImage.Layer.ShadowRadius = 1;
            _flagImage.Layer.ShadowOffset = new CGSize(0.5f, 0.5f);
            _flagImage.Layer.ShadowOpacity = 1;
            _flagImage.ClipsToBounds = false;

            // UIImageExtensions.GetCountryFlag(_flagImage, podcast.Country);
            _flagImage.Hidden = true;

            UILabelExtensions.SetupLabelAppearance(_dayDateLabel, resultDate[0], Colors.Black, 24f);
            UILabelExtensions.SetupLabelAppearance(_monthDateLabel, resultDate[1], Colors.Black, 12f);
            UILabelExtensions.SetupLabelAppearance(_tracksLabel, $"{podcast.NumberTracks} {locationResources["TracksLabel"]}", Colors.Black, 13f, UIFontWeight.Semibold);
            UILabelExtensions.SetupLabelAppearance(_categoryLabel, podcast.PrimaryGenre, Colors.Black, 14f);
            UILabelExtensions.SetupLabelAppearance(_titleLabel, podcast.PodcastName, Colors.Black, 17f, UIFontWeight.Semibold);
            UILabelExtensions.SetupLabelAppearance(_nameLabel, $"{locationResources["ByLabel"]} {podcast.Name}", UIColor.LightGray, 14f, UIFontWeight.Semibold);
        }
    }
}
