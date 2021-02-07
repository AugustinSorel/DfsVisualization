using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DfsVisualization
{
    public class MazeDrawer
    {
        private int CellWidth = 20;
        private int CellHeight = 20;

        private double canvasWidth = 400;
        private double canvasHeight = 400;

        Canvas mazeCanvas;

        public MazeDrawer(Canvas mazeCanvas)
        {
            this.mazeCanvas = mazeCanvas;
            canvasWidth = mazeCanvas.Width = (Application.Current.Windows[0] as MainWindow).Width - 40;
            canvasHeight = mazeCanvas.Height = (Application.Current.Windows[0] as MainWindow).Height - 40;
        }

        public void DrawGrid()
        {
            for (int i = 0; i < canvasHeight / CellHeight; i++)
            {
                for (int j = 0; j < canvasWidth / CellWidth; j++)
                {
                    Rectangle rectangle = new Rectangle()
                    {
                        Width = CellWidth,
                        Height = CellHeight,
                        Fill = Brushes.DarkGray,
                    };

                    Canvas.SetLeft(rectangle, j * CellWidth);
                    Canvas.SetTop(rectangle, i * CellHeight);
                    mazeCanvas.Children.Add(rectangle);

                    if (j < canvasWidth / CellWidth - 1)
                    {
                        DrawWallBetween(j, i, j + 1, i);
                    }

                    if (i < canvasHeight / CellHeight - 1)
                    {
                        DrawWallBetween(j, i, j, i + 1);
                    }
                }
            }
        }

        private void DrawWallBetween(int x1, int y1, int x2, int y2)
        {
            if (y1 == y2)
                DrawVerticalLine(Math.Max(x1, x2) * CellHeight, y1 * CellHeight, (y1 + 1) * CellHeight, Brushes.Black);
            else
                DrawHorizontalLine(Math.Max(y1, y2) * CellWidth, x1 * CellWidth, (x1 + 1) * CellWidth, Brushes.Black);
        }


        private Line DrawVerticalLine(int x, int y1, int y2, Brush color)
        {
            return DrawLine(x, x, y1, y2, color);
        }

        private Line DrawHorizontalLine(int y, int x1, int x2, Brush color)
        {
            return this.DrawLine(x1, x2, y, y, color);
        }

        private Line DrawLine(int x1, int x2, int y1, int y2, Brush color)
        {
            var line = new Line
            {
                Stroke = color,
                StrokeThickness = 2,
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2
            };

            mazeCanvas.Children.Add(line);

            return line;
        }
    }
}
