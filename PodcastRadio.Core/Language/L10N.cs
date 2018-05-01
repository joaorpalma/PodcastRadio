using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.Core.Language
{
    public static class L10N
    {
        private static string _currentLocale;
        private static string CurrentLocale
        {
            get
            {
                return _currentLocale ?? (_currentLocale = App.Container.GetInstance<IPlatformSpecificService>().PlatformLanguage());
            }
        }

        public static List<string> SupportedLanguage { get { return new List<string> { "en-GB", "pt-PT" }; } }
        public static string DefaultLanguage { get { return "en-GB"; } }

        private static List<Literal> _resourceManager;
        private static List<Literal> ResourceManager(string locale)
        {
            if (!SupportedLanguage.Contains(locale))
                locale = DefaultLanguage;

            if (_resourceManager != null)
                return _resourceManager;

            var manifestResourceStream = Assembly.Load(new AssemblyName("PodcastRadio.Core")).GetManifestResourceStream(string.Format($"PodcastRadio.Core.Language.Resources-{locale}.json"));
            var streamReader = new StreamReader(manifestResourceStream);
            var jsonString = streamReader.ReadToEnd();
            var tracksCollection = JsonConvert.DeserializeObject<List<Literal>>(jsonString);
            _resourceManager = tracksCollection;
            return _resourceManager;
        }

        public static string Locale()
        {
            return CurrentLocale;
        }

        public static string Localize(string key)
        {
            string locale;
            switch (CurrentLocale)
            {
                case "pt": locale = "pt-PT"; break;
                case "en": locale = "en-GB"; break;

                default:
                    locale = "en-GB";
                    break;
            }

            var val = ResourceManager(locale)?.FirstOrDefault(x => x.Key == key);
            return val != null ? val.Translated : string.Empty;
        }
    }

    public class Literal
    {
        public string Key { get; set; }
        public string Translated { get; set; }
    }
}
