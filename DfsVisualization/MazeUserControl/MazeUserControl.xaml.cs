using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        private readonly MazeDrawer mazeDrawer;
        private BlurAnimation blurAnimation;

        public MazeUserControl(MazeDrawer mazeDrawer)
        {
            InitializeComponent();
            this.mazeDrawer = mazeDrawer;
            blurAnimation = new BlurAnimation();
            DrawGrid();
        }

        private void DrawGrid()
        {
            mazeDrawer.DrawGrid(mazeCanvas);
        }

        #region Blur effect
        internal void GetBlurEffect(bool enabled)
        {
            blurAnimation.StartAnimation(enabled, mazeCanvas, this);
        }
        #endregion
    }
}