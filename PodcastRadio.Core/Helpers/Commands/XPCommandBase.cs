using System;
using System.Windows.Input;

namespace PodcastRadio.Core.Helpers.Commands
{
    public abstract class XPCommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter) => CanExecute(parameter);
        void ICommand.Execute(object parameter) => Execute(parameter);

        protected abstract void Execute(object parameter);
        protected abstract bool CanExecute(object parameter);
    }
}
