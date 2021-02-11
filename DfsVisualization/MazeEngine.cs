using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace DfsVisualization
{
    public class MazeEngine
    {
        #region Private Fields
        private ProgressBar progressBar;
        private readonly MazeDrawer mazeDrawer;
        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        private SliderValue sleep;
        #endregion

        public MazeEngine(MazeDrawer mazeDrawer)
        {
            this.mazeDrawer = mazeDrawer;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            backgroundWorker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        internal void StartDfs(ProgressBar progressBar, SliderValue sleep)
        {
            this.progressBar = progressBar;
            this.sleep = sleep;
            if (backgroundWorker.IsBusy != true)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < mazeDrawer.NumberOfCellsY; i++)
            {
                for (int j = 0; j < mazeDrawer.NumberOfCellsX; j++)
                {
                    Application.Current.Dispatcher.Invoke(() => { 
                        mazeDrawer.Cells[j, i].Background = GlobalColors.BackgroundColor;
                    });
                    Thread.Sleep(GetSleep());
                    backgroundWorker.ReportProgress(GetPercentageOfCellUsed(j, i)); 
                } 
            }
        }

        private int GetSleep()
        {
            return Math.Abs(sleep.BoundNumber -10) * 30;
        }

        private int GetPercentageOfCellUsed(int j , int i)
        {
            int index = j + i * mazeDrawer.NumberOfCellsX;
            int max = (mazeDrawer.NumberOfCellsY * mazeDrawer.NumberOfCellsX);

            decimal percentage = (decimal)index / max;
            return (int)Math.Round(percentage * 100);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End");
        }
    }
}