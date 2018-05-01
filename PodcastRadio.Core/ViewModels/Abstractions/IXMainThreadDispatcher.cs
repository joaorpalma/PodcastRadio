using System;
namespace PodcastRadio.Core.ViewModels.Abstractions
{
    public interface IXMainThreadDispatcher
    {
        bool InvokeOnMainThread(Action action, bool maskExceptions = true);
    }
}
