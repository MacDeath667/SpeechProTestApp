using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SpeechProApp.Commands;
using SpeechProApp.Model;
using SpeechProApp.Utils;

namespace SpeechProApp
{
    public class MainWindowVM : BaseNotifyPropertyChanged
    {
        private Table _selectedTable;
        private Visibility _isCollapsedParams;
        private ObservableCollection<Database> _databases;
        private string _username;
        private string _database;
        private string _servername;
        
        public ICommand BuildCommand { get; }
        public ICommand ShowTableColumnsCommand { get; }

        public MainWindowVM()
        {
            BuildCommand = new BuildDatabaseMapCommand(this);
            ShowTableColumnsCommand = new ShowTableColumnsCommand(this);
            Servername = "DESKTOP-B915095";
            Username = "sa";
            Database = "master";
        }

        public Table SelectedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Database> Databases
        {
            get => _databases;
            set
            {
                _databases = value;
                OnPropertyChanged();
            }
        }

         public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Database
        {
            get => _database;
            set
            {
                _database = value;
                OnPropertyChanged();
            }
        }

        public string Servername
        {
            get => _servername;
            set
            {
                _servername = value;
                OnPropertyChanged();
            }
        }
    }
}