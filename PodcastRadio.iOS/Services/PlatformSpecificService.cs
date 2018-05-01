using System;
using System.Threading;
using Foundation;
using PodcastRadio.Core.Services.Abstractions;
using UIKit;

namespace PodcastRadio.iOS.Services
{
    public class PlatformSpecificService : IPlatformSpecificService
    {
        public string DeviceId { get; set; }

        public string AppVersion() => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();

        public string DeviceName() => UIDevice.CurrentDevice.Name;

        public void ExitApp() => Thread.CurrentThread.Abort();

        public bool IsMainThread() => NSThread.Current.IsMainThread;

        public string Platform() => "iOS";

        public string PlatformLanguage()
        {
            var language = NSLocale.PreferredLanguages[0];
            return language.Split('-')[0];
        }
    }
}
