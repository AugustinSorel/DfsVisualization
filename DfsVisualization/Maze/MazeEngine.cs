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

        private bool[,] marked;
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

        private void Dfs(Cell[,] cells, Cell cell)
        {
            Stack<Cell> stack = new Stack<Cell>();
            stack.Push(cell);
            Random random = new Random();

            while (stack.Count > 0)
            {
                Cell v = stack.Pop();
                if (! marked[v.X, v.Y])
                {
                    Application.Current.Dispatcher.Invoke(() => {
                        //v.Background = GlobalColors.BackgroundColor;
                        v.Background = Brushes.Red;
                    });

                    marked[v.X, v.Y] = true;
                    Thread.Sleep(GetSleep());
                    List<Cell> neighbour = new List<Cell>();
                 
                    if (v.X + 1 < mazeDrawer.NumberOfCellsX && !marked[v.X + 1, v.Y])
                        neighbour.Add(cells[v.X + 1, v.Y]);
                    if (v.X - 1 > 0 && !marked[v.X - 1, v.Y])
                        neighbour.Add(cells[v.X - 1, v.Y]);

                    if (v.Y + 1 < mazeDrawer.NumberOfCellsY && !marked[v.X, v.Y + 1])
                        neighbour.Add(cells[v.X, v.Y + 1]);
                    if (v.Y - 1 > 0 && !marked[v.X, v.Y - 1])
                        neighbour.Add(cells[v.X, v.Y - 1]);

                    foreach (var item in neighbour)
                    {
                        stack.Push(item);
                    }

                    if (neighbour.Count > 0)
                    {
                        int wall = random.Next(0, neighbour.Count);
                        stack.Push(neighbour[wall]);
                    }
                        

                    //if (v.X + 1 < mazeDrawer.NumberOfCellsX && !marked[v.X + 1, v.Y] && wall == 1)
                    //    stack.Push(cells[v.X + 1, v.Y]);
                    //if (v.X - 1 > 0 && !marked[v.X - 1, v.Y] && wall == 1)
                    //    stack.Push(cells[v.X - 1, v.Y]);

                    //if (v.Y + 1 < mazeDrawer.NumberOfCellsY && !marked[v.X, v.Y + 1] && wall == 1)
                    //    stack.Push(cells[v.X, v.Y+1]);
                    //if (v.Y - 1> 0 && !marked[v.X, v.Y - 1] && wall == 1)
                    //    stack.Push(cells[v.X, v.Y-1]);
                }
            }

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            marked = new bool[mazeDrawer.NumberOfCellsX, mazeDrawer.NumberOfCellsY];

            for (int i = 0; i < mazeDrawer.NumberOfCellsY; i++)
                for (int j = 0; j < mazeDrawer.NumberOfCellsX; j++)
                    marked[j, i] = false;

            Dfs(mazeDrawer.Cells, mazeDrawer.Cells[0, 0]);

            //for (int i = 0; i < mazeDrawer.NumberOfCellsY; i++)
            //{
            //    for (int j = 0; j < mazeDrawer.NumberOfCellsX; j++)
            //    {

            //        while (pause)
            //        {
            //            Thread.Sleep(100);
            //        }

            //        if (backgroundWorker.CancellationPending)
            //        {
            //            return;
            //        }
                    
            //        Application.Current.Dispatcher.Invoke(() => { 
            //            mazeDrawer.Cells[j, i].Background = GlobalColors.BackgroundColor;
            //        });
            //        Thread.Sleep(GetSleep());
            //        backgroundWorker.ReportProgress(GetPercentageOfCellUsed(j, i)); 
            //    } 
            //}
        }

        private int GetSleep()
        {
            return Math.Abs(sleep.BoundNumber -10) * 10;
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