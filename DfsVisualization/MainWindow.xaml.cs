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
        private MazeDrawer mazeDrawer;
        private MazeEngine mazeEngine;
        private MazeUserControl mazeUserControl;
        private MazeSettings mazeSettings;
        private SettingsUserControl settingsUserControl;
        #endregion

        #region ctor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            CreateMazeDrawerAndEngine();
            AddTopBar();
            AddToolBar();
            AddMazeToContainer();
        }

        #region Create The Instance Of MazeDrawer And MazeEngine
        private void CreateMazeDrawerAndEngine()
        {
            mazeSettings = new MazeSettings();
            mazeDrawer = new MazeDrawer(mazeSettings);
            mazeEngine = new MazeEngine(mazeDrawer, mazeSettings);
        }
        #endregion

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
            toolBar = new ToolBarUserControl(mazeEngine)
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
            mazeUserControl = new MazeUserControl(mazeDrawer);
            container.Children.Add(mazeUserControl);
            Grid.SetColumn(mazeUserControl, 1);
            Grid.SetRow(mazeUserControl, 2);
        }
        #endregion

        #region Add Settings To Container
        public void AddSettingsToContainer()
        {
            settingsUserControl= new SettingsUserControl(mazeSettings);
            container.Children.Add(settingsUserControl);
            Grid.SetColumn(settingsUserControl, 1);
            Grid.SetRow(settingsUserControl, 2);
            mazeUserControl.GetBlurEffect(false);
        }

        public void RemoveSettingsToContainer()
        {
            container.Children.Remove(settingsUserControl);
            mazeUserControl.GetBlurEffect(true); 
        }

        #endregion

        #region Handle Key Down
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.U:
                    toolBar.Settings_Click(this, null);
                    break;

                case System.Windows.Input.Key.Right:
                    toolBar.SpeedSlider.Value += 1;
                    break;

                case System.Windows.Input.Key.Left:
                    toolBar.SpeedSlider.Value -= 1;
                    break;

                case System.Windows.Input.Key.A:
                    toolBar.Abort_Click(this, null);
                    break;

                case System.Windows.Input.Key.P:
                    toolBar.PauseButton_Click(this, null);
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
