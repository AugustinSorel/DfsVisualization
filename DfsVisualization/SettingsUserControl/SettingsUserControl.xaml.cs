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

        public SettingsUserControl()
        {
            InitializeComponent();
            opacityFade = new OpacityFade(this);
            opacityFade.StartAnimation(true);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            opacityFade.StartAnimation(false);
        }
    }
}
