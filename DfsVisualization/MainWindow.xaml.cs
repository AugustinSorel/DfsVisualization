using System.Windows;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            AddMazeToContainer();
        }

        private void AddMazeToContainer()
        {
            container.Children.Add(new MazeUserControl());
        }
    }
}
