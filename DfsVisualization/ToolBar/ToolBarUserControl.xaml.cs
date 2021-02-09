using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for ToolBarUserControl.xaml
    /// </summary>
    public partial class ToolBarUserControl : UserControl
    {
        private SliderValue sliderValue;

        public ToolBarUserControl()
        {
            InitializeComponent();
            sliderValue = new SliderValue();
            DataContext = sliderValue;
        }
    }
}
