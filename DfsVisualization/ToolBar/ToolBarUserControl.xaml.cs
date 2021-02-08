using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for ToolBarUserControl.xaml
    /// </summary>
    public partial class ToolBarUserControl : UserControl, INotifyPropertyChanged
    {
        public ToolBarUserControl()
        {
            DataContext = this;
            InitializeComponent();
        }

        private int _boundNumber = 5;

        public int BoundNumber
        {
            get { return _boundNumber; }
            set 
            {
                if (_boundNumber != value)
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
