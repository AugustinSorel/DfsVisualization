using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace DfsVisualization
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        private readonly MazeDrawer mazeDrawer;
        private BlurEffect blurEffect = null;

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

        internal void GetBlurEffect()
        {
            if (blurEffect == null)
            {
                blurEffect = new BlurEffect
                {
                    Radius = 20,
                    KernelType = KernelType.Gaussian,
                };
            }
            else
                blurEffect = null;

            Effect = blurEffect;
        }
    }
}