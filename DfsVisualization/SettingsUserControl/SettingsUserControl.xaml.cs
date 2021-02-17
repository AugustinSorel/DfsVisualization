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
        //private SettingsOutputMessage settingsOutputMessage;

        public SettingsUserControl(MazeSettings mazeSettings)
        {
            InitializeComponent();
            this.mazeSettings = mazeSettings;
            opacityFade = new OpacityFade(this);
            opacityFade.StartAnimation(true);
            DataContext = mazeSettings;
        }


        public void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            opacityFade.StartAnimation(false);
        }
    }
}
