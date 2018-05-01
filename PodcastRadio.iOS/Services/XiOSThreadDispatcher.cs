using System;
using System.Threading;
using PodcastRadio.Core.Services;
using UIKit;

namespace PodcastRadio.iOS.Services
{
    public class XiOSThreadDispatcher : XMainThreadDispatcher
    {
        private readonly SynchronizationContext _uiSynchronizationContext;

        public XiOSThreadDispatcher()
        {
            _uiSynchronizationContext = SynchronizationContext.Current;
        }

        public override bool InvokeOnMainThread(Action action, bool maskExceptions = true)
        {
            if (_uiSynchronizationContext == SynchronizationContext.Current)
            {
                action();
            }
            else
            {
                UIApplication.SharedApplication.BeginInvokeOnMainThread(() =>
                {
                    if (maskExceptions)
                        ExceptionMaskedAction(action);
                    else
                        action();
                });
            }

            return true;
        }
    }
}
