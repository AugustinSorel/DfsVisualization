using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for ToolBarUserControl.xaml
    /// </summary>
    public partial class ToolBarUserControl : UserControl
    {
        #region Private Fields
        private readonly SliderValue sliderValue;
        private MazeEngine mazeEngine;
        #endregion

        public ToolBarUserControl(MazeEngine mazeEngine)
        {
            InitializeComponent();
            this.mazeEngine = mazeEngine;

            sliderValue = new SliderValue();
            DataContext = sliderValue;
        }

        #region StartButton Click Handler
        public void StartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UIElement a = (Application.Current.Windows[0] as MainWindow).container.Children.Cast<UIElement>().First(x => Grid.GetRow(x) == 2 && Grid.GetColumn(x) == 1);

            if (a.GetType() != typeof(MazeUserControl))
                return;

            mazeEngine.StartDfs(progressBar, sliderValue);
        }
        #endregion

        #region PauseButton Click Handler
        public void PauseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mazeEngine.PauseDfs();
        }
        #endregion

        #region AbortButton Click Handler
        public void Abort_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mazeEngine.AbortDfs();
        }
        #endregion

        #region Settings Click Handler
        private void Settings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (mazeEngine.BackgroundWorker.IsBusy)
                return;

            (Application.Current.Windows[0] as MainWindow).AddSettingsToContainer();
        }
        #endregion
    }
}
