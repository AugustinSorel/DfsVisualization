using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DfsVisualization
{
    public class MazeSettings : INotifyPropertyChanged
    {
        #region Fields
        private int startCellY;
        private int startCellX;

        private int numberOfCellsY;
        private int numberOfCellsX;

        private int cellWidth;
        private int cellHeight;

        private bool aStart;

        private int aStarStartX;
        private int aStarStartY;
        private int aStarEndX;
        private int aStartEndY;
        #endregion

        #region Properties
        public int AStartEndX
        {
            get { return aStarEndX; }
            set
            {
                if (value >= 0 && value < numberOfCellsX && value > aStarStartX)
                    aStarEndX = value;
                else
                    aStarEndX = 0;

                OnPropertyChanged();
            }
        }

        public int AStartEndY
        {
            get { return aStartEndY; }
            set
            {
                if (value >= 0 && value < numberOfCellsY && value > aStarStartY)
                    aStartEndY = value;
                else
                    aStartEndY = value;

                OnPropertyChanged();
            }
        }

        public int AStarStartX
        {
            get { return aStarStartX; }
            set
            {
                if (value >= 0 && value < numberOfCellsX && value < aStarEndX)
                    aStarStartX = value;
                else
                    aStarStartX = 0;

                OnPropertyChanged();
            }
        }

        public int AStarStartY
        {
            get { return aStarStartY; }
            set
            {
                if (value >= 0 && value < numberOfCellsY && value < aStartEndY)
                    aStarStartY = value;
                else
                    aStarStartY = 0;

                OnPropertyChanged();
            }
        }

        public bool AStar
        {
            get { return aStart; }
            set
            {
                aStart = value;
                OnPropertyChanged();
            }
        }

        public int StartCellY
        {
            get { return startCellY; }
            set 
            {
                if (value >= 0 && value < numberOfCellsY)
                    startCellY = value;
                else
                    startCellY = 0;

                OnPropertyChanged();
            }
        }

        public int StartCellx
        {
            get { return startCellX; }
            set 
            {
                if (value >= 0 && value < numberOfCellsX)
                    startCellX = value;
                else
                    startCellX = 0;

                OnPropertyChanged();
            }
        }

        public int NumberOfCellsX
        {
            get { return numberOfCellsX; }
            set 
            {
                numberOfCellsX = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfCellsY
        {
            get { return numberOfCellsY; }
            set 
            { 
                numberOfCellsY = value;
                OnPropertyChanged();
            }
        }

        public int CellWidth
        {
            get { return cellWidth; }
            set 
            {
                if (value > 0 && value < 200)
                {
                    cellWidth = value;
                    CalculateNumberOfCells();
                    StartCellx = startCellX;
                    OnPropertyChanged();
                }
            }
        }

        public int CellHeight
        {
            get { return cellHeight; }
            set 
            {
                if (value > 0 && value < 200)
                {
                    cellHeight = value;
                    CalculateNumberOfCells();
                    StartCellY = startCellY;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public MazeSettings()
        {
            startCellX = 0;
            startCellY = 0;

            cellWidth = 80;
            cellHeight = 80;

            aStart = true;

            aStarStartX = 0;
            aStarStartY = 0;

            CalculateNumberOfCells();

            aStarEndX = numberOfCellsX - 1;
            aStartEndY = numberOfCellsY - 1;
        }

        private void CalculateNumberOfCells()
        {
            NumberOfCellsX = (int)(Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth / cellWidth;
            NumberOfCellsY = (int)(Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight / cellHeight;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
