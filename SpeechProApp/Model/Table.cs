using System.Collections.ObjectModel;

namespace SpeechProApp.Model
{
    public class Table
    {
        public string Name { get; set; }

        public ObservableCollection<Column> Columns { get; set; }
    }
}
