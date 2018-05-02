using System;
using Foundation;
using Plugin.MediaManager;
using UIKit;

namespace PodcastRadio.iOS
{
    [Register("RadioApplication")]
    public class RadioApplication : UIApplication
    {
        private MediaManagerImplementation MediaManager => (MediaManagerImplementation)CrossMediaManager.Current;

        public override void RemoteControlReceived(UIEvent uiEvent)
        {
            MediaManager.MediaRemoteControl.RemoteControlReceived(uiEvent);
        }
    }
}
