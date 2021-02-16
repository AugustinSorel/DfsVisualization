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
        private SettingsOutputMessage settingsOutputMessage;

        public SettingsUserControl(MazeSettings mazeSettings)
        {
            InitializeComponent();
            this.mazeSettings = mazeSettings;
            opacityFade = new OpacityFade(this);
            opacityFade.StartAnimation(true);
            SetControlsValue();
            settingsOutputMessage = new SettingsOutputMessage();
            DataContext = settingsOutputMessage;
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
            string outputMessage = string.Empty;

            if (!int.TryParse(textboxCellWidth.Text, out int cellWidth))
            {
                mazeSettings.CellWidth = 20;
                outputMessage += "CellWidth ";
            }
            else
                mazeSettings.CellWidth = cellWidth;

            if (!int.TryParse(textboxCellHeight.Text, out int cellHeight))
            {
                mazeSettings.CellHeight = 20;
                outputMessage += "CellHeight ";
            }
            else
                mazeSettings.CellHeight = cellHeight;

            mazeSettings.NumberOfCellsX = (int)(Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth / mazeSettings.CellWidth;
            mazeSettings.NumberOfCellsY = (int)(Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight / mazeSettings.CellHeight;

            if (!int.TryParse(TextBoxStartCellX.Text, out int startCellX))
            {
                mazeSettings.StartCellx = 0;
                outputMessage += "StartCellX ";
            }
            else
                mazeSettings.StartCellx = startCellX;

            if (!int.TryParse(TextBoxStartCellY.Text, out int startCellY))
            {
                mazeSettings.StartCellY = 0;
                outputMessage += "StartCellY ";
            }
            else
                mazeSettings.StartCellY = startCellY;

            if (outputMessage != string.Empty)
                outputMessage = "Cannot convert " + outputMessage;
            else
                outputMessage = "Changes saved";

            settingsOutputMessage.OutputMessage = outputMessage;
            SetControlsValue();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            settingsOutputMessage.OutputMessage = "Changes canceled";
        }
    }
}
