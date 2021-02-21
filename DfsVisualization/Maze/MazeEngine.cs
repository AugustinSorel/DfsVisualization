using System;
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
        private readonly MazeSettings mazeSettings;
        private BackgroundWorker backgroundWorker;
        private SliderValue sleep;
        private Dfs dfs;
        private Astar astar;
        #endregion

        #region Properties
        public BackgroundWorker BackgroundWorker
        {
            get { return backgroundWorker; }
        }

        #endregion

        public MazeEngine(MazeDrawer mazeDrawer, MazeSettings mazeSettings)
        {
            this.mazeDrawer = mazeDrawer;
            this.mazeSettings = mazeSettings;
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
            if (dfs != null)
                dfs.HandlePause();
        }

        internal void AbortDfs()
        {
            if (dfs != null)
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
            dfs = new Dfs(mazeDrawer, backgroundWorker, sleep, mazeSettings);
            dfs.Start();

            if (mazeSettings.AStar && dfs.Finished)
            {
                astar = new Astar(mazeDrawer, mazeSettings);
                astar.Start();
            }

            if (mazeSettings.SaveToTextFile)
            {
                SaveToFile saveToFile = new SaveToFile(astar);
                saveToFile.SaveToTextFile();
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End");
        }
        #endregion
    }
}