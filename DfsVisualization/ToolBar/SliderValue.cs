using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DfsVisualization
{
    class SliderValue : INotifyPropertyChanged
    {
        private int _boundNumber = 5;

        public int BoundNumber
        {
            get { return _boundNumber; }
            set
            {
                if (_boundNumber != value && value > 0 && value < 11)
                {
                    _boundNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
