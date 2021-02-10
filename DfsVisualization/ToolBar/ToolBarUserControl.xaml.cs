using System.Windows;
using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for ToolBarUserControl.xaml
    /// </summary>
    public partial class ToolBarUserControl : UserControl
    {
        private MazeUserControl mazeUserControl;
        private SliderValue sliderValue;
        private MazeEngine mazeEngine;

        public ToolBarUserControl(MazeUserControl mazeUserControl)
        {
            InitializeComponent();
            this.mazeUserControl = mazeUserControl;
            sliderValue = new SliderValue();
            mazeEngine = new MazeEngine();
            DataContext = sliderValue;

        }

        public void StartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mazeEngine.Test();
        }
    }
}
