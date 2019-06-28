using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SpeechProApp.BL;

namespace SpeechProApp.Commands
{
    public class BuildDatabaseMapCommand : ICommand
    {
        private readonly MainWindowVM _mainWindowVm;
        private readonly DbInfoExtractor _dbInfoExtractor;

        public BuildDatabaseMapCommand(MainWindowVM mainWindowVm)
        {
            _mainWindowVm = mainWindowVm;
            _dbInfoExtractor = new DbInfoExtractor();
        }

        public bool CanExecute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;
            return !string.IsNullOrWhiteSpace(_mainWindowVm.Servername) &&
                   !string.IsNullOrWhiteSpace(_mainWindowVm.Username) &&
                   !string.IsNullOrWhiteSpace(password);
        }

        public void Execute(object parameter)
        {
            try
            {
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox?.Password;
                _mainWindowVm.Databases = _dbInfoExtractor.BuildDB(_mainWindowVm.Servername,
                    _mainWindowVm.Database, _mainWindowVm.Username, password);
            }
            catch (SqlException)
            {
                MessageBox.Show("Undefined error");
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
