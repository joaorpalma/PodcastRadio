using System;
namespace PodcastRadio.Core.Services.Abstractions
{
    public interface IPlatformSpecificService
    {
        string DeviceId { get; set; }

        string Platform();

        string DeviceName();

        string AppVersion();

        string PlatformLanguage();

        bool IsMainThread();

        void ExitApp();
    }
}
