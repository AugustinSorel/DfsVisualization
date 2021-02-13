using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DfsVisualization
{
    /// <summary>
    /// Contain all of the drawing fields and methods
    /// </summary>
    public class MazeDrawer
    {
        #region Private Fields
        private int numberOfCellsY;
        private int numberOfCellsX;
        
        private const int CELL_WIDTH = 20;
        private const int CELL_HEIGHT = 20;

        private Cell[,] cells;
        private Canvas mazeCanvas;
        #endregion

        #region Public Properties
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

        public Cell[,] Cells
        {
            get { return cells; }
            set { cells = value; }
        }
        #endregion

        public MazeDrawer()
        {
            numberOfCellsX = (int)(Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth / CELL_WIDTH;
            numberOfCellsY = (int)(Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight / CELL_HEIGHT;
            
            cells = new Cell[numberOfCellsX, numberOfCellsY];
        }

        #region Re Draw
        /// <summary>
        /// re draw the grid in the Maze user control canvas
        /// </summary>
        internal void Redraw()
        {
            DrawGrid(mazeCanvas);
        }
        #endregion

        #region Draw the grid
        /// <summary>
        /// draw the grid in the Maze user control canvas
        /// </summary>
        /// <param name="mazeCanvas"></param>
        public void DrawGrid(Canvas mazeCanvas)
        {
            mazeCanvas.Children.Clear();
            this.mazeCanvas = mazeCanvas;

            for (int i = 0; i < numberOfCellsY; i++)
            {
                for (int j = 0; j < numberOfCellsX; j++)
                {
                    if (i == numberOfCellsY - 1 && j == numberOfCellsX - 1)
                    {
                        AddCellToCanvas(j, i);
                    }
                    else if (i == numberOfCellsY - 1)
                    {
                        AddCellToCanvas(j, i);
                    }
                    else if (j == numberOfCellsX - 1)
                    {
                        AddCellToCanvas(j, i);
                    }
                    else
                    {
                        AddCellToCanvas(j, i);
                    }
                }
            }   
        }
        #endregion

        #region Add Cell To Canvas
        /// <summary>
        /// Add cells to the canvas
        /// </summary>
        /// <param name="j"> x location </param>
        /// <param name="i"> y location </param>
        private void AddCellToCanvas(int j, int i)
        {
            Cell cell = new Cell()
            {
                Width = CELL_WIDTH,
                Height = CELL_HEIGHT,
                Background = GlobalColors.CellColor,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                X = j,
                Y = i,
            };

            Canvas.SetLeft(cell, j * CELL_WIDTH);
            Canvas.SetTop(cell, i * CELL_HEIGHT);
            mazeCanvas.Children.Add(cell);
            cells[j, i] = cell;
        }
        #endregion
    }
}
