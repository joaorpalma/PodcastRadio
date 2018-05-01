using System;
using System.Threading.Tasks;

namespace PodcastRadio.Core.Services.Abstractions
{
    public interface IDialogService
    {
        Task<bool> ShowAlert(string title, string message = "",  AlertType alertType = AlertType.Error, string ConfirmButtonText = "", string CancelButtonText = null);
        void ShowLoading();
        void HideLoading();
    }

    public enum AlertType
    {
        Null,
        Success,
        Error,
        Timeout
    }
}
