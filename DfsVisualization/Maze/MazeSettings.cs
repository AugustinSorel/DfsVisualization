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
            set 
            {
                CalculateNumberOfCells();
                if (value >= 0 && value < numberOfCellsY)
                {
                    startCellY = value; 
                }
            }
        }

        public int StartCellx
        {
            get { return startCellX; }
            set 
            {
                CalculateNumberOfCells();
                if (value >= 0 && value < numberOfCellsX)
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
            set 
            {
                if (value > 0)
                {
                    cellWidth = value; 
                    CalculateNumberOfCells();
                }
            }
        }

        public int CellHeight
        {
            get { return cellHeight; }
            set 
            {
                if (value > 0)
                {
                    cellHeight = value; 
                    CalculateNumberOfCells();
                }
            }
        }
        #endregion

        public MazeSettings()
        {
            startCellX = 0;
            startCellY = 0;

            cellWidth = 20;
            cellHeight = 20;

            CalculateNumberOfCells();
        }

        private void CalculateNumberOfCells()
        {
            NumberOfCellsX = (int)(Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth / cellWidth;
            NumberOfCellsY = (int)(Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight / cellHeight;
        }
    }
}
