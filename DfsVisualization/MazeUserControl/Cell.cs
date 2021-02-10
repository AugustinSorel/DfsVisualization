using System.Windows.Controls;

namespace DfsVisualization
{
    public class Cell : Border
    {
        private bool _topWall;
        private bool _bottomWall;
        private bool _leftWall;
        private bool _rightWall;

        public bool RightWall
        {
            get { return _rightWall; }
            set { _rightWall = value; }
        }

        public bool LeftWall
        {
            get { return _leftWall; }
            set { _leftWall = value; }
        }

        public bool BottomWall
        {
            get { return _bottomWall; }
            set { _bottomWall = value; }
        }

        public bool TopWall
        {
            get { return _topWall; }
            set { _topWall = value; }
        }


        public Cell()
        {
            this.BorderThickness = new System.Windows.Thickness(0);
        }
    }
}
