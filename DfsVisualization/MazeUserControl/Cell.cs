using System.Windows.Controls;

namespace DfsVisualization
{
    /// <summary>
    /// Represent the cell in the Maze User Control
    /// </summary>
    public class Cell : Border
    {
        #region Private Fields
        private int x;
        private int y;
        #endregion

        #region Public properties
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        #endregion

        public Cell()
        {
            this.BorderThickness = new System.Windows.Thickness(0);
        }
    }
}
