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
            AddTopBar();
            AddToolBar();
            AddMazeToContainer();
        }

        #region Add Top Bar To Container
        private void AddTopBar()
        {
            TopBar TopBar = new TopBar();
            container.Children.Add(TopBar);
            Grid.SetColumnSpan(TopBar, 3);
        }
        #endregion

        #region Add Tool Bar To Container
        private void AddToolBar()
        {
            ToolBarUserControl toolBar = new ToolBarUserControl();
            toolBar.Margin = new Thickness(0, 10, 0, 10); 
            container.Children.Add(toolBar);
            Grid.SetColumn(toolBar, 1);
            Grid.SetRow(toolBar, 1);
        }
        #endregion

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
