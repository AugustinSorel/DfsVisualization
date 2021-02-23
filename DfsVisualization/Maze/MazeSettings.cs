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

        private bool dfsSolve;
        private bool saveToTextFile;

        private int dfsSolveStartX;
        private int dfsSolveStartY;
        private int dfsSolveEndX;
        private int dfsSolveEndY;
        #endregion


        #region Properties
        public bool SaveToTextFile
        {
            get { return saveToTextFile; }
            set
            {
                saveToTextFile = value;
                OnPropertyChanged();
            }
        }

        public int DfsSolveEndX
        {
            get { return dfsSolveEndX; }
            set
            {
                if (value >= 0 && value < numberOfCellsX && value > dfsSolveStartX)
                    dfsSolveEndX = value;
                else
                    dfsSolveEndX = 0;

                OnPropertyChanged();
            }
        }

        public int DfsSolveEndY
        {
            get { return dfsSolveEndY; }
            set
            {
                if (value >= 0 && value < numberOfCellsY && value > dfsSolveStartY)
                    dfsSolveEndY = value;
                else
                    dfsSolveEndY = value;

                OnPropertyChanged();
            }
        }

        public int DfsSolveStartX
        {
            get { return dfsSolveStartX; }
            set
            {
                if (value >= 0 && value < numberOfCellsX && value < dfsSolveEndX)
                    dfsSolveStartX = value;
                else
                    dfsSolveStartX = 0;

                OnPropertyChanged();
            }
        }

        public int DfsSolveStartY
        {
            get { return dfsSolveStartY; }
            set
            {
                if (value >= 0 && value < numberOfCellsY && value < dfsSolveEndY)
                    dfsSolveStartY = value;
                else
                    dfsSolveStartY = 0;

                OnPropertyChanged();
            }
        }

        public bool DfsSolve
        {
            get { return dfsSolve; }
            set
            {
                dfsSolve = value;
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

            cellWidth = 20;
            cellHeight = 20;

            dfsSolve = true;
            saveToTextFile = true;

            dfsSolveStartX = 0;
            dfsSolveStartY = 0;

            CalculateNumberOfCells();

            dfsSolveEndX = numberOfCellsX - 1;
            dfsSolveEndY = numberOfCellsY - 1;
        }

        #region Calculate Number Of Cells
        private void CalculateNumberOfCells()
        {
            NumberOfCellsX = (int)(Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth / cellWidth;
            NumberOfCellsY = (int)(Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight / cellHeight;
        }
        #endregion

        #region On Property Changed Event
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
