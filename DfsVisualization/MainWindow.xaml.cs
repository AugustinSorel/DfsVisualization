using System.Windows;
using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region ctor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            AddMazeToContainer();
        }

        #region Add Maze To Container
        private void AddMazeToContainer()
        {
            MazeUserControl mazeUserControl = new MazeUserControl();
            container.Children.Add(mazeUserControl);
            Grid.SetColumn(mazeUserControl, 1);
            Grid.SetRow(mazeUserControl, 2);
        }
        #endregion
    }
}
