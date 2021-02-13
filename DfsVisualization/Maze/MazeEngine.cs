using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DfsVisualization
{
    public class MazeEngine
    {
        #region Private Fields
        private ProgressBar progressBar;
        private readonly MazeDrawer mazeDrawer;
        private BackgroundWorker backgroundWorker;
        private SliderValue sleep;
        private bool pause;
        
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

        internal void PauseDfs()
        {
            pause ^= true;
        }

        internal void StartDfs(ProgressBar progressBar, SliderValue sleep)
        {
            if (backgroundWorker.IsBusy != true)
            {
                mazeDrawer.Redraw();
                pause = false;
                this.progressBar = progressBar;
                this.sleep = sleep;
                backgroundWorker.RunWorkerAsync();
            }
        }

        internal void AbortDfs()
        {
            if (!pause)
                backgroundWorker.CancelAsync();
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dfs dfs = new Dfs(mazeDrawer, backgroundWorker, sleep);
            dfs.Start();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End");
        }
    }
}