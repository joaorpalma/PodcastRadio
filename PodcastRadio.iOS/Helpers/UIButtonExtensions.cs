using System;
using CoreGraphics;
using UIKit;

namespace PodcastRadio.iOS.Helpers
{
    public static class UIButtonExtensions
    {
        public static UIBarButtonItem SetupImageBarButton(nfloat size, string imageName, EventHandler onTouchEvent)
        {
            var button = new UIButton(new CGRect(0, 0, size, size)) { ContentMode = UIViewContentMode.ScaleAspectFit };
            button.AddConstraint(NSLayoutConstraint.Create(button, NSLayoutAttribute.Width, NSLayoutRelation.Equal, 1, size));
            button.AddConstraint(NSLayoutConstraint.Create(button, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, size));
            button.SetImage(UIImage.FromBundle(imageName), UIControlState.Normal);
            button.SetTitle(string.Empty, UIControlState.Normal);
            
            button.TouchUpInside -= onTouchEvent;
            button.TouchUpInside += onTouchEvent;

            return new UIBarButtonItem(button);
        }
    }
}