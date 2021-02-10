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
        #endregion

        public MazeEngine(MazeDrawer mazeDrawer)
        {
            this.mazeDrawer = mazeDrawer;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            backgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        internal void StartDfs(ProgressBar progressBar)
        {
            this.progressBar = progressBar;
            if (backgroundWorker.IsBusy != true)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This is called on the UI thread when ReportProgress method is called
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
                    Thread.Sleep(10);
                    backgroundWorker.ReportProgress(i);
                }
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End");
        }
    }
}