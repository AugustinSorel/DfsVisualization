using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DfsVisualization
{
    class SettingsOutputMessage : INotifyPropertyChanged
    {
        private string outputMessage = string.Empty;

        public string OutputMessage
        {
            get { return outputMessage; }
            set
            {
                outputMessage = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
