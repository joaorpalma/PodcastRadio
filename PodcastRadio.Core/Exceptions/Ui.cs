using System;
using System.Threading.Tasks;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.Core.Exceptions
{
    public static class Ui
    {
        private static IDialogService _dialogService;
        private static IDialogService Dialogs => _dialogService ?? (_dialogService = App.Container.GetInstance<IDialogService>());

        public static void Handle(Exception e)
        {
            if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                Dialogs.ShowAlert("No Connection", "Check if you have intertnet access");
                return;
            }
#if DEBUG
            Dialogs.ShowAlert(nameof(e), e.ToString());
#endif
        }

        public static void Handle(NoInternetException e)
        {
            Dialogs.ShowAlert("No Connection", "Check if you have intertnet access");
        }
    }
}
