using System.ComponentModel;
using System.Runtime.CompilerServices;
using SpeechProApp.Annotations;

namespace SpeechProApp.Utils
{
    public class BaseNotifyPropertyChanged :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
