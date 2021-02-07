using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        public MazeUserControl()
        {
            InitializeComponent();
            DrawGrid();
        }

        private void DrawGrid()
        {
            MazeDrawer mazeDrawer = new MazeDrawer(mazeCanvas);
            mazeDrawer.DrawGrid();
        }
    }
}
