﻿using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for ToolBarUserControl.xaml
    /// </summary>
    public partial class ToolBarUserControl : UserControl
    {
        #region Private Fields
        private readonly SliderValue sliderValue;
        private readonly MazeEngine mazeEngine;
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
    }
}
