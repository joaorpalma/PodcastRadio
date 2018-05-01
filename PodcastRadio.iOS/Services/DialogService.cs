using System;
using System.Threading.Tasks;
using MBProgressHUD;
using PodcastRadio.Core.Services.Abstractions;
using PodcastRadio.iOS.Helpers;
using PodcastRadio.iOS.Views.CustomViews;
using UIKit;

namespace PodcastRadio.iOS.Services
{
    public class DialogService : IDialogService
    {
        private MTMBProgressHUD progressDialog = new MTMBProgressHUD();

        public Task<bool> ShowAlert(string title, string message = "", AlertType alertType = AlertType.Error, string ConfirmButtonText = "", string CancelButtonText = null)
        {
            var tcs = new TaskCompletionSource<bool>();

            var alertVC = new AlertViewController(title, message, alertType, ConfirmButtonText, () => tcs.TrySetResult(true));
            alertVC.Show();

            return tcs.Task;
        }

        public void ShowLoading()
        {
            HideLoading();
            var view = ((AppDelegate)UIApplication.SharedApplication.Delegate).Window;

            progressDialog = new MTMBProgressHUD(view)
            {
                RemoveFromSuperViewOnHide = true,
                TintColor = Colors.White,
            };

            view.AddSubview(progressDialog);
            view.BringSubviewToFront(progressDialog);
            progressDialog.Show(true);
        }

        public void HideLoading()
        {
            progressDialog?.Hide(true);
            progressDialog?.RemoveFromSuperview();
            progressDialog = null;
        }
    }
}
