using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using PodcastRadio.Core.Helpers;
using PodcastRadio.Core.Services.Abstractions;
using PodcastRadio.Core.ViewModels;
using SimpleInjector;

namespace PodcastRadio.Core
{
    public static class App
    {
        public static Container Container { get; private set; } = new Container();
        private static IXNavigationService NavService => Container.GetInstance<IXNavigationService>();

        public static void Start()
        {
            NavService.NavigateAsync<MainViewModel>();
        }

        public static void Initialize()
        {
            InitializeIoC();
        }

        public static void InitializeIoC()
        {
            RegisterViewModelsAuto();
            RegisterServicesAuto();
        }

        private static void RegisterViewModelsAuto()
        {
            var repositoryAssembly = typeof(App).GetTypeInfo().Assembly;

            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace == "PodcastRadio.Core.ViewModels"
                where !type.GetTypeInfo().IsAbstract
                select new { Implementation = type };

            registrations = registrations.ToList();

            Debug.WriteLine(registrations);
            foreach (var registration in registrations)
            {
                Container.Register(registration.Implementation, registration.Implementation, Lifestyle.Singleton);
            }
           
        }

        private static void RegisterServicesAuto()
        {
            var repositoryAssembly = typeof(App).GetTypeInfo().Assembly;

            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace == "PodcastRadio.Core.Services"
                where type.GetTypeInfo().GetInterfaces().Any() && !type.GetTypeInfo().IsAbstract
                select new { Interface = type.GetTypeInfo().GetInterfaces().Single(), Implementation = type };

            foreach (var registration in registrations)
            {
                Container.Register(registration.Interface, registration.Implementation, Lifestyle.Singleton);
            }

        }
    }
}
