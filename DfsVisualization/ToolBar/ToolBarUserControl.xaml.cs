using System.Windows;
using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for ToolBarUserControl.xaml
    /// </summary>
    public partial class ToolBarUserControl : UserControl
    {
        private readonly MazeEngine mazeEngine;
        private SliderValue sliderValue;

        public ToolBarUserControl(MazeEngine mazeEngine)
        {
            InitializeComponent();
            sliderValue = new SliderValue();
            DataContext = sliderValue;
            this.mazeEngine = mazeEngine;
        }

        public void StartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mazeEngine.Test();
        }
    }
}
