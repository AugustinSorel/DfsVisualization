using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
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
        private TextBlock progressBarTextBlock;
        #endregion

        public MazeEngine(MazeDrawer mazeDrawer)
        {
            this.mazeDrawer = mazeDrawer;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            backgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        internal void StartDfs(ProgressBar progressBar, TextBlock progressBarTextBlock)
        {
            this.progressBar = progressBar;
            this.progressBarTextBlock = progressBarTextBlock;
            progressBar.Maximum = 100;
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
                    int index = j + i * mazeDrawer.NumberOfCellsX;
                    int max = (mazeDrawer.NumberOfCellsY * mazeDrawer.NumberOfCellsX);

                    decimal percentage = (decimal)index / max;
                    int percentageConverted = (int)Math.Round(percentage * 100);

                    Application.Current.Dispatcher.Invoke(() => { 
                        mazeDrawer.Cells[j, i].Background = GlobalColors.BackgroundColor;
                        progressBarTextBlock.Text = percentageConverted.ToString();
                    });
                    Thread.Sleep(10);
                   

                    backgroundWorker.ReportProgress(percentageConverted); // change style of progress bar
                } 
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End");
        }
    }
}