using System.Collections.ObjectModel;

namespace SpeechProApp.Model
{
    public class Database
    {
        public string Name { get; set; }

        public ObservableCollection<Table> Tables { get; set; } = new ObservableCollection<Table>();
    }
}
