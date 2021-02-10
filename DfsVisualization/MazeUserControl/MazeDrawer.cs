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
        private int CellWidth = 10;
        private int CellHeight = 10;

        private double canvasWidth;
        private double canvasHeight;

        public Cell[,] cells;

        private Canvas mazeCanvas;
        

        public MazeDrawer()
        {
            canvasWidth = (Application.Current.Windows[0] as MainWindow).container.ColumnDefinitions[1].ActualWidth;
            canvasHeight = (Application.Current.Windows[0] as MainWindow).container.RowDefinitions[2].ActualHeight;
            cells = new Cell[(int)canvasWidth / CellWidth, (int)canvasHeight / CellHeight];
        }

        public void DrawGrid(Canvas mazeCanvas)
        {
            this.mazeCanvas = mazeCanvas;

            for (int i = 0; i < canvasHeight / CellHeight; i++)
            {
                for (int j = 0; j < canvasWidth / CellWidth; j++)
                {
                    if (i == canvasHeight / CellHeight - 1 && j == canvasWidth / CellWidth - 1)
                    {
                        AddCellToCanvas(j, i, 0, 0);
                    }
                    else if (i == canvasHeight / CellHeight - 1)
                    {
                        AddCellToCanvas(j, i, 2, 0);
                    }
                    else if (j == canvasWidth / CellWidth - 1)
                    {
                        AddCellToCanvas(j, i, 0, 2);
                    }
                    else
                    {
                        AddCellToCanvas(j, i, 2, 2);
                    }
                }
            }   
        }

        #region Add Cell To Canvas
        /// <summary>
        /// Add a border to the canvas
        /// </summary>
        /// <param name="j"> x location </param>
        /// <param name="i"> y location </param>
        /// <param name="right"> Right border thickness of the Cell </param>
        /// <param name="bottom"> Light border thickness of the Cell </param>
        private void AddCellToCanvas(int j, int i, int right, int bottom)
        {
            Cell cell = new Cell()
            {
                Width = CellWidth,
                Height = CellHeight,
                Background = GlobalColors.CellColor,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, right, bottom),
                X = j,
                Y = i,
            };

            Canvas.SetLeft(cell, j * CellWidth);
            Canvas.SetTop(cell, i * CellHeight);
            mazeCanvas.Children.Add(cell);
            cells[j, i] = cell;
        }
        #endregion
    }
}
