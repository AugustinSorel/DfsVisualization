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
        private readonly MazeSettings mazeSettings;
        private Cell[,] cells;
        private Canvas mazeCanvas;
        #endregion

        #region Public Properties
        

        public Cell[,] Cells
        {
            get { return cells; }
            set { cells = value; }
        }
        #endregion

        public MazeDrawer(MazeSettings mazeSettings)
        {
            this.mazeSettings = mazeSettings;
            
           
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
            cells = new Cell[mazeSettings.NumberOfCellsX, mazeSettings.NumberOfCellsY];
            mazeCanvas.Children.Clear();
            this.mazeCanvas = mazeCanvas;

            for (int i = 0; i < mazeSettings.NumberOfCellsY; i++)
            {
                for (int j = 0; j < mazeSettings.NumberOfCellsX; j++)
                {
                    if (i == mazeSettings.NumberOfCellsY - 1 && j == mazeSettings.NumberOfCellsX - 1)
                    {
                        AddCellToCanvas(j, i);
                    }
                    else if (i == mazeSettings.NumberOfCellsY - 1)
                    {
                        AddCellToCanvas(j, i);
                    }
                    else if (j == mazeSettings.NumberOfCellsX - 1)
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
                Width = mazeSettings.CellWidth,
                Height = mazeSettings.CellHeight,
                Background = GlobalColors.CellColor,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                X = j,
                Y = i,
            };

            Canvas.SetLeft(cell, j * mazeSettings.CellWidth);
            Canvas.SetTop(cell, i * mazeSettings.CellHeight);
            mazeCanvas.Children.Add(cell);
            cells[j, i] = cell;
        }
        #endregion
    }
}
