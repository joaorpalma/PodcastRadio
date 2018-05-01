using System;
using System.Diagnostics;
using System.Reflection;
using PodcastRadio.Core.ViewModels.Abstractions;

namespace PodcastRadio.Core.Services
{
    public abstract class XMainThreadDispatcher : IXMainThreadDispatcher
    {
        public abstract bool InvokeOnMainThread(Action action, bool maskExceptions = true);

        protected static void ExceptionMaskedAction(Action action)
        {
            try
            {
                action();
            }
            catch (TargetInvocationException exception)
            {
                Debugger.Break();
                Debug.WriteLine(exception);
            }
            catch (Exception exception)
            {
                Debugger.Break();
                Debug.WriteLine(exception);
            }
        }
    }
}
