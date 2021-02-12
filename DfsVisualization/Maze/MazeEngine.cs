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

        private List<Cell> ListOfUnvisitedCell = new List<Cell>();
        private List<Cell> unvisitedNeighbors = new List<Cell>();
        private Cell currentCell;
        private Cell chosenCell;
        private Stack<Cell> stack;

        private void Dfs()
        {
            while (ListOfUnvisitedCell.Count > 0)
            {
                unvisitedNeighbors = GetNeighbors(currentCell, ListOfUnvisitedCell);
                if (unvisitedNeighbors != null)
                    VisitNeightbors();
                else if (stack.Count != 0)
                    GoBack();
                Thread.Sleep(10);
            }
            RemoveTheCurrentCell();
        }

        private void GoBack()
        {
            RemoveTheCurrentCell();
            currentCell = stack.Pop();
            GetCurrentCell();
        }

        private void VisitNeightbors()
        {
            chosenCell = RandomPick(unvisitedNeighbors);

            stack.Push(currentCell);

            Application.Current.Dispatcher.Invoke(new Action(() => { RemoveWalls(currentCell, chosenCell); }));
            RemoveTheCurrentCell();

            currentCell = chosenCell;
            GetCurrentCell();
            marked[currentCell.X, currentCell.Y] = true;
            ListOfUnvisitedCell.Remove(currentCell);
        }

        private void GetCurrentCell()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { currentCell.Background = Brushes.Red; }));
        }

        private void RemoveTheCurrentCell()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { currentCell.Background = GlobalColors.BackgroundColor; }));
        }

        private void RemoveWalls(Cell a, Cell b)
        {
            if (a.Y == b.Y)
            {
                if (a.X < b.X)
                {
                    a.RightWall = false;
                    b.LeftWall = false;
                }
                else
                {
                    a.LeftWall = false;
                    b.RightWall = false;
                }
            }
            else
            {
                if (a.Y < b.Y)
                {
                    a.BottomWall = false;
                    b.TopWall = false;
                }
                else
                {
                    a.TopWall = false;
                    b.BottomWall = false;
                }
            }
        }

        private Cell RandomPick(List<Cell> list)
        {
            Random random = new Random();
            int i;

            if (list.Count == 1)
                return list[0];

            i = random.Next(list.Count);
            return list[i];
        }

        private List<Cell> GetNeighbors(Cell _currentCell, List<Cell> unvisited)
        {
            List<Cell> t = new List<Cell>();

            Cell _neighbor = unvisited.Find(c => c.Y == _currentCell.Y - 1 && c.X == _currentCell.X);
            if (_neighbor != null)
            {
                t.Add(_neighbor);
            }

            _neighbor = unvisited.Find(c => c.Y == _currentCell.Y && c.X == _currentCell.X - 1);
            if (_neighbor != null)
            {
                t.Add(_neighbor);
            }

            //Cell below current cell
            _neighbor = unvisited.Find(c => c.Y == _currentCell.Y + 1 && c.X == _currentCell.X);
            if (_neighbor != null)
            {
                t.Add(_neighbor);
            }

            //Cell right of current cell
            _neighbor = unvisited.Find(c => c.Y == _currentCell.Y && c.X == _currentCell.X + 1);
            if (_neighbor != null)
            {
                t.Add(_neighbor);
            }

            if (t.Count != 0)
            {
                return t;
            }
            return null;
        }

        private void GetAllNeighbours(Cell v, List<Cell> neighbour, Cell[,] cells)
        {
            if (v.X + 1 < mazeDrawer.NumberOfCellsX && !marked[v.X + 1, v.Y])
                neighbour.Add(cells[v.X + 1, v.Y]);
            if (v.X - 1 >= 0 && !marked[v.X - 1, v.Y])
                neighbour.Add(cells[v.X - 1, v.Y]);

            if (v.Y + 1 < mazeDrawer.NumberOfCellsY && !marked[v.X, v.Y + 1])
                neighbour.Add(cells[v.X, v.Y + 1]);
            if (v.Y - 1 >= 0 && !marked[v.X, v.Y - 1])
                neighbour.Add(cells[v.X, v.Y - 1]);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            marked = new bool[mazeDrawer.NumberOfCellsX, mazeDrawer.NumberOfCellsY];

            for (int i = 0; i < mazeDrawer.NumberOfCellsY; i++)
                for (int j = 0; j < mazeDrawer.NumberOfCellsX; j++)
                {
                    ListOfUnvisitedCell.Add(mazeDrawer.Cells[j, i]);
                    marked[j, i] = false;
                }

            stack = new Stack<Cell>();
            marked[0, 0] = true;
            currentCell = mazeDrawer.Cells[0, 0];
            ListOfUnvisitedCell.Remove(currentCell);

            Dfs();

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