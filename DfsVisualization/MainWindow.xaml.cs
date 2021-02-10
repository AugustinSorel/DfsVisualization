using System.Windows;
using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private ToolBarUserControl toolBar;
        MazeDrawer mazeDrawer;
        MazeEngine mazeEngine;
        #endregion

        #region ctor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            mazeDrawer = new MazeDrawer();
            mazeEngine = new MazeEngine();
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
            toolBar = new ToolBarUserControl()
            {
                Margin = new Thickness(0, 10, 0, 10)
            };
            container.Children.Add(toolBar);
            Grid.SetColumn(toolBar, 1);
            Grid.SetRow(toolBar, 1);
        }
        #endregion

        #region Add Maze To Container
        private void AddMazeToContainer()
        {
            MazeUserControl mazeUserControl = new MazeUserControl(mazeDrawer);
            container.Children.Add(mazeUserControl);
            Grid.SetColumn(mazeUserControl, 1);
            Grid.SetRow(mazeUserControl, 2);
        }
        #endregion

        #region Handle Key Down
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Right:
                    toolBar.SpeedSlider.Value += 1;
                    break;

                case System.Windows.Input.Key.Left:
                    toolBar.SpeedSlider.Value -= 1;
                    break;

                case System.Windows.Input.Key.S:
                    toolBar.StartButton_Click(this, null);
                    break;

                case System.Windows.Input.Key.Escape:
                    Application.Current.Shutdown();
                    break;
            }
        }
        #endregion
    }
}
