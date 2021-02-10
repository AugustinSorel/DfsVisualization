using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DfsVisualization
{
    public class MazeEngine
    {
        private readonly MazeDrawer mazeDrawer;

        public MazeEngine(MazeDrawer mazeDrawer)
        {
            this.mazeDrawer = mazeDrawer;
        }

        internal void Test()
        {
            foreach (Cell item in mazeDrawer.cells)
            {
                item.Background = Brushes.Green;
            }
        }
    }
}