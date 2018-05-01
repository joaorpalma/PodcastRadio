using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PodcastRadio.Core.Services.Abstractions;

namespace PodcastRadio.Core.ViewModels.Abstractions
{
    public class XViewModel : IXViewModel, INotifyPropertyChanged
    {
        private static IXNavigationService _navService;
        private static IXMainThreadDispatcher _mainThreadDispatcher;
        protected static IXNavigationService NavService = _navService ?? (_navService = App.Container.GetInstance<IXNavigationService>());
        protected static IXMainThreadDispatcher MainThreadDispatcher => _mainThreadDispatcher ?? (_mainThreadDispatcher = App.Container.GetInstance<IXMainThreadDispatcher>());

        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly PropertyChangedEventArgs AllPropertiesChanged = new PropertyChangedEventArgs(string.Empty);

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(nameof(IsBusy)); }
        }

        public void InitializeViewModel() => InitializeAsync();

        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task Appearing() => Task.CompletedTask;

        public virtual Task Disappearing() => Task.CompletedTask;

        public void RaisePropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        public virtual void Prepare(){}

        public virtual void Prepare(object dataObject){}
    }

    public abstract class XViewModel<TObject> : XViewModel
    {
        public override void Prepare(object dataObject) => Prepare((TObject)dataObject);

        protected abstract void Prepare(TObject data);
    }
}
