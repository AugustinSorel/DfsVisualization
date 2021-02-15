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
            SetVariables();
        }

        private void SetVariables()
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
        {// tryparse
         // reset to 0 if cell size changed   
            mazeSettings.StartCellx = int.Parse(TextBoxStartCellX.Text);
            mazeSettings.StartCellY = int.Parse(TextBoxStartCellY.Text);

            mazeSettings.CellWidth = int.Parse(textboxCellWidth.Text);
            mazeSettings.CellHeight = int.Parse(textboxCellHeight.Text);

            mazeSettings.NumberOfCellsX = (int)(Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth / mazeSettings.CellWidth;
            mazeSettings.NumberOfCellsY = (int)(Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight / mazeSettings.CellHeight;
        }
    }
}
