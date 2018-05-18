using System;
using System.Threading.Tasks;
using PodcastRadio.Core.Helpers;
using PodcastRadio.Core.Helpers.Commands;
using PodcastRadio.Core.Language;
using PodcastRadio.Core.Services.Abstractions;
using PodcastRadio.Core.ViewModels.Abstractions;

namespace PodcastRadio.Core.ViewModels
{
    public class InformationViewModel : XViewModel, IPresentView
    {
        private XPCommand _closeViewCommand;
        public XPCommand CloseViewCommand => _closeViewCommand ?? (_closeViewCommand = new XPCommand(async () => await CloseView()));
        
		public InformationViewModel() { }

        #region resources

        public string Title => L10N.Localize("InformationViewModel_Title");
        public string CreatedBy => L10N.Localize("InformationViewModel_CreatedBy");
        public string JoaoPalma => L10N.Localize("InformationViewModel_JoaoPalma");
        public string ForLabel => L10N.Localize("InformationViewModel_For");
        public string CopyrightLabel => L10N.Localize("InformationViewModel_Copyright");

        #endregion

        private async Task CloseView()
        {
            await NavService.Close(this);
        }
    }
}
