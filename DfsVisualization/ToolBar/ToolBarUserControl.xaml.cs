using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for ToolBarUserControl.xaml
    /// </summary>
    public partial class ToolBarUserControl : UserControl
    {
        private SliderValueDP sliderValue;

        public ToolBarUserControl()
        {
            InitializeComponent();
            sliderValue = new SliderValueDP();
            DataContext = sliderValue;
        }
    }
}
