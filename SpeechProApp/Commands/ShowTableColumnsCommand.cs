using System;
using System.Windows.Input;
using SpeechProApp;
using SpeechProApp.Model;

namespace SpeechProApp.Commands
{
    public class ShowTableColumnsCommand : ICommand
    {
        private readonly MainWindowVM _mainWindowVm;

        public ShowTableColumnsCommand(MainWindowVM mainWindowVm)
        {
            this._mainWindowVm = mainWindowVm;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var table = parameter as Table;
            _mainWindowVm.SelectedTable = table;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value; //snatched from StackOverflow
        }
    }
}
