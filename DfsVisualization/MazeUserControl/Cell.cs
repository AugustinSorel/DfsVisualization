using System.Windows;
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

        private bool rightWall = true;
        private bool leftWall = true;
        private bool topWall = true;
        private bool bottomWall = true;

        public bool RightWall
        {
            get
            {
                return rightWall;
            }
            set
            {
                rightWall = value;
                int x = 0;
                if (value == true)
                {
                    x = 1;
                }
                Thickness thickness = BorderThickness;
                thickness.Right = x;
                BorderThickness = thickness;
            }
        }

        public bool LeftWall
        {
            get
            {
                return leftWall;
            }
            set
            {
                leftWall = value;
                int x = 0;
                if (value == true)
                {
                    x = 1;
                }
                Thickness thickness = BorderThickness;
                thickness.Left = x;
                BorderThickness = thickness;
            }
        }

        public bool TopWall
        {
            get
            {
                return topWall;
            }
            set
            {
                topWall = value;
                int x = 0;
                if (value == true)
                {
                    x = 1;
                }
                Thickness thickness = BorderThickness;
                thickness.Top = x;
                BorderThickness = thickness;
            }
        }

        public bool BottomWall
        {
            get
            {
                return bottomWall;
            }
            set
            {
                bottomWall = value;
                int x = 0;
                if (value == true)
                {
                    x = 1;
                }
                Thickness thickness = BorderThickness;
                thickness.Bottom = x;
                BorderThickness = thickness;
            }
        }
    }
}
