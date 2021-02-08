using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for ToolBarUserControl.xaml
    /// </summary>
    public partial class ToolBarUserControl : UserControl
    {
        public ToolBarUserControl()
        {
            InitializeComponent();
            DataContext = new SliderValue();
        }
    }
}
