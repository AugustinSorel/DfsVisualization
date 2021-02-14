using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        private readonly MazeDrawer mazeDrawer;
        private readonly BlurAnimation blurAnimation;

        public MazeUserControl(MazeDrawer mazeDrawer)
        {
            InitializeComponent();
            this.mazeDrawer = mazeDrawer;
            blurAnimation = new BlurAnimation(this);
            DrawGrid();
        }

        private void DrawGrid()
        {
            mazeDrawer.DrawGrid(mazeCanvas);
        }

        #region Blur effect
        internal void GetBlurEffect(bool enabled)
        {
            blurAnimation.StartAnimation(enabled, mazeCanvas);
        }
        #endregion
    }
}