using System;
using System.Linq;
using PodcastRadio.Core;
using PodcastRadio.Core.Services.Abstractions;
using SimpleInjector;

namespace PodcastRadio.iOS
{
    public static class Setup
    {
        public static void Initialize()
        {
            App.Initialize();
            RegisterPlatformServices();
            InitializePlatformServices();
            App.Start();
        }

        private static void RegisterPlatformServices()
        {
            var repositoryAssembly = typeof(Setup).Assembly;

            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace == "PodcastRadio.iOS.Services"
                where type.GetInterfaces().Any() && !type.IsAbstract
                select new { Interface = type.GetInterfaces().Single(), Implementation = type };

            foreach (var registration in registrations)
            {
                App.Container.Register(registration.Interface, registration.Implementation, Lifestyle.Singleton);
            }
        }

        private static void InitializePlatformServices()
        {
            App.Container.GetInstance<IXNavigationService>().Initialize();
        }
    }
}
