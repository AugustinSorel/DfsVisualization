using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DfsVisualization
{
    public class MazeEngine
    {
        private readonly MazeDrawer mazeDrawer;
        private BackgroundWorker backgroundWorker;

        public MazeEngine(MazeDrawer mazeDrawer)
        {
            this.mazeDrawer = mazeDrawer;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += Worker_DoWork;
            backgroundWorker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        internal void Test()
        {
            foreach (Cell item in mazeDrawer.cells)
            {
                item.Background = Brushes.Green;
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var item in mazeDrawer.cells)
            {
                item.Background = GlobalColors.BackgroundColor;
                Thread.Sleep(100);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("End");
        }
    }
}