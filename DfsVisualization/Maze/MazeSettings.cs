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
        
        private string outputMessage;
        #endregion

        #region Properties
        public string OutputMessage
        {
            get { return outputMessage; }
            set 
            { 
                outputMessage = value;
                OnPropertyChanged();
            }
        }

        public int StartCellY
        {
            get { return startCellY; }
            set 
            {
                if (value >= 0 && value < numberOfCellsY)
                {
                    startCellY = value;
                    OnPropertyChanged();
                }
                else
                    OutputMessage += " StartCellY";
            }
        }

        public int StartCellx
        {
            get { return startCellX; }
            set 
            {
                if (value >= 0 && value < numberOfCellsX)
                {
                    startCellX = value;
                    OnPropertyChanged();
                }
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
                if (value > 0)
                {
                    cellWidth = value; 
                    OnPropertyChanged();
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

            CalculateNumberOfCells();
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
