using System;
using System.Windows;
using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        private readonly OpacityFade opacityFade;
        private readonly MazeSettings mazeSettings;

        public SettingsUserControl(MazeSettings mazeSettings)
        {
            InitializeComponent();
            this.mazeSettings = mazeSettings;
            opacityFade = new OpacityFade(this);
            opacityFade.StartAnimation(true);
            SetControlsValue();
        }

        private void SetControlsValue()
        {
            TextBoxStartCellX.Text = mazeSettings.StartCellx.ToString();
            TextBoxStartCellY.Text = mazeSettings.StartCellY.ToString();

            textboxCellWidth.Text = mazeSettings.CellWidth.ToString();
            textboxCellHeight.Text = mazeSettings.CellHeight.ToString();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            opacityFade.StartAnimation(false);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            mazeSettings.NumberOfCellsX = (int)(Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth / mazeSettings.CellWidth;
            mazeSettings.NumberOfCellsY = (int)(Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight / mazeSettings.CellHeight;
            
            mazeSettings.StartCellx = int.TryParse(TextBoxStartCellX.Text, out int startCellX) ? startCellX : 0;
            mazeSettings.StartCellY = int.TryParse(TextBoxStartCellY.Text, out int startCellY) ? startCellY : 0;

            mazeSettings.CellWidth = int.TryParse(textboxCellWidth.Text, out int cellWidth) ? cellWidth : 20;
            mazeSettings.CellHeight = int.TryParse(textboxCellHeight.Text, out int cellHeight) ? cellHeight : 20;

            SetControlsValue();
        }
    }
}
