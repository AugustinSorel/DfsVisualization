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

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public SettingsUserControl(MazeSettings mazeSettings)
        {
            InitializeComponent();
            this.mazeSettings = mazeSettings;
            opacityFade = new OpacityFade(this);
            opacityFade.StartAnimation(true);
            SetControlsValue();
            DataContext = this;
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

            string errorMessage = string.Empty;

            if (!int.TryParse(TextBoxStartCellX.Text, out int startCellX))
            {
                mazeSettings.StartCellx = 0;
                errorMessage += "StartCellX ";
            }
            else
                mazeSettings.StartCellx = startCellX;

            if (!int.TryParse(TextBoxStartCellY.Text, out int startCellY))
            {
                mazeSettings.StartCellY = 0;
                errorMessage += "StartCellY ";
            }
            else
                mazeSettings.StartCellY = startCellY;

            if (!int.TryParse(textboxCellWidth.Text, out int cellWidth))
            {
                mazeSettings.CellWidth = 20;
                errorMessage += "CellWidth ";
            }
            else
                mazeSettings.CellWidth = cellWidth;

            if (!int.TryParse(textboxCellHeight.Text, out int cellHeight))
            {
                mazeSettings.CellHeight = 20;
                errorMessage += "CellHeight ";
            }
            else
                mazeSettings.CellHeight = cellHeight;

            if (errorMessage != string.Empty)
                errorMessage = "Cannot convert " + errorMessage;

            ErrorMessage = errorMessage;
            SetControlsValue();
        }
    }
}
