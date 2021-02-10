using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        private readonly MazeDrawer mazeDrawer;

        public MazeUserControl(MazeDrawer mazeDrawer)
        {
            InitializeComponent();
            this.mazeDrawer = mazeDrawer;
            DrawGrid();
        }

        private void DrawGrid()
        {
            mazeDrawer.DrawGrid(mazeCanvas);
        }
    }
}