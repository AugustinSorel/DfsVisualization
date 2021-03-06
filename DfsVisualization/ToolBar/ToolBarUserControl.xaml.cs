﻿using System.Linq;
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
            UIElement a = (Application.Current.Windows[0] as MainWindow).container.Children.Cast<UIElement>().Last(x => Grid.GetRow(x) == 2 && Grid.GetColumn(x) == 1);

            if (a.GetType() == typeof(SettingsUserControl))
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
        public void Settings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (mazeEngine.BackgroundWorker.IsBusy)
                return;

            UIElement a = (Application.Current.Windows[0] as MainWindow).container.Children.Cast<UIElement>().Last(x => Grid.GetRow(x) == 2 && Grid.GetColumn(x) == 1);

            if (a.GetType() == typeof(SettingsUserControl))
            {
                (Application.Current.Windows[0] as MainWindow).SettingsUserControlStartOpacityFade();
                return;
            }

            (Application.Current.Windows[0] as MainWindow).AddSettingsToContainer();
        }
        #endregion
    }
}
