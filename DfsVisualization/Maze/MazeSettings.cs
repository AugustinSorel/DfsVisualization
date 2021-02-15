using System.Windows;

namespace DfsVisualization
{
    public class MazeSettings
    {
        #region Fields
        private int startCellY;
        private int startCellX;

        private int numberOfCellsY;
        private int numberOfCellsX;

        private int cellWidth;
        private int cellHeight;
        #endregion

        #region Properties
        public int StartCellY
        {
            get { return startCellY; }
            set { startCellY = value; }
        }

        public int StartCellx
        {
            get { return startCellX; }
            set 
            {
                if (value > 0)
                {
                    startCellX = value; 
                }
            }
        }

        public int NumberOfCellsX
        {
            get { return numberOfCellsX; }
            set { numberOfCellsX = value; }
        }

        public int NumberOfCellsY
        {
            get { return numberOfCellsY; }
            set { numberOfCellsY = value; }
        }

        public int CellWidth
        {
            get { return cellWidth; }
            set { cellWidth = value; }
        }

        public int CellHeight
        {
            get { return cellHeight; }
            set { cellHeight = value; }
        }
        #endregion

        public MazeSettings()
        {
            startCellX = 0;
            startCellY = 0;

            cellWidth = 20;
            cellHeight = 20;

            numberOfCellsX = (int)(Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth / cellWidth;
            numberOfCellsY = (int)(Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight / cellHeight;
        }
    }
}
