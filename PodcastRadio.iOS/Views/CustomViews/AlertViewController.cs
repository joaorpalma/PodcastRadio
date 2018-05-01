using System;
using PodcastRadio.Core.Services.Abstractions;
using UIKit;

namespace PodcastRadio.iOS.Views.CustomViews
{
    public partial class AlertViewController : UIViewController
    {
        private string _title;
        private string _description;
        private string _confirmButtonText;
        private AlertType _alertType;
        private Action _positiveBehaviour;
        private IDialogService _dialogService;

        public AlertViewController(string title, string description = "", AlertType alertType = AlertType.Error, string confirmButtonText = "OK", Action positiveBehaviour = null) 
            : base("AlertViewController", null)
        {
            _title = title;
            _description = description;
            _alertType = alertType;
            _confirmButtonText = confirmButtonText;
            _positiveBehaviour = positiveBehaviour;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public void Show()
        {
            View.Frame = UIApplication.SharedApplication.KeyWindow.Bounds;
            UIApplication.SharedApplication.KeyWindow.AddSubview(View);
            UIView.Animate(0.3, () => View.Alpha = 1);
        }

        public void Dismiss()
        {
            UIView.AnimateNotify(0.3, () => View.Alpha = 0, (finished) => View.RemoveFromSuperview());
        }
    }
}

