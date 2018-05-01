using System;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.Core.Language
{
    public static class LanguageInfo
    {
        public static string CurrentLanguageName()
        {
            return App.Container.GetInstance<IPlatformSpecificService>().PlatformLanguage();
        }
    }
}
