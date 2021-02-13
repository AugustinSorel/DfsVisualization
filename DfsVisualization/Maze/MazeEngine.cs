using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DfsVisualization
{
    public class MazeEngine
    {
        #region Private Fields
        private ProgressBar progressBar;
        private readonly MazeDrawer mazeDrawer;
        private BackgroundWorker backgroundWorker;
        private SliderValue sleep;
        private Dfs dfs;
        
        #endregion

        public MazeEngine(MazeDrawer mazeDrawer)
        {
            this.mazeDrawer = mazeDrawer;
            CreateBackgroundWorker();
        }

        private void CreateBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true,
            };
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            backgroundWorker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        #region Pause And Abort Event
        internal void PauseDfs()
        {
            dfs.HandlePause();
        }

        internal void AbortDfs()
        {
            dfs.HandleAbort();
        }
        #endregion

        #region Start The Dfs Animation
        /// <summary>
        /// Start the dfs animation called by the play button in the tool bar user control
        /// </summary>
        /// <param name="progressBar"> use to show the progress of the animation</param>
        /// <param name="sleep"> slider to get the sleeping time</param>
        internal void StartDfs(ProgressBar progressBar, SliderValue sleep)
        {
            if (backgroundWorker.IsBusy != true)
            {
                mazeDrawer.Redraw();
                this.progressBar = progressBar;
                this.sleep = sleep;
                backgroundWorker.RunWorkerAsync();
            }
        }
        #endregion

        #region backgroundWorker Event
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            dfs = new Dfs(mazeDrawer, backgroundWorker, sleep);
            dfs.Start();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End");
        }
        #endregion
    }
}