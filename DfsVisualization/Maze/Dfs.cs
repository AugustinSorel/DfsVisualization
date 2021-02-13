using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace DfsVisualization
{
    internal class Dfs
    {
        #region Private Fields
        private List<Cell> unvisitedNeighbors;
        private Cell currentCell;
        private Cell chosenCell;
        private bool pause;
        private List<Cell> ListOfUnvisitedCell;
        private Stack<Cell> stack;
        
        private readonly MazeDrawer mazeDrawer;
        private readonly BackgroundWorker backgroundWorker;
        private readonly SliderValue sleep;
        #endregion

        public Dfs(MazeDrawer mazeDrawer, BackgroundWorker backgroundWorker, SliderValue sleep)
        {
            this.mazeDrawer = mazeDrawer;
            this.backgroundWorker = backgroundWorker;
            this.sleep = sleep;

            SetVar();
            SetListOfUnvisitedCell(mazeDrawer);   
        }

        #region variables set
        private void SetListOfUnvisitedCell(MazeDrawer mazeDrawer)
        {
            for (int i = 0; i < mazeDrawer.NumberOfCellsY; i++)
                for (int j = 0; j < mazeDrawer.NumberOfCellsX; j++)
                    ListOfUnvisitedCell.Add(mazeDrawer.Cells[j, i]);
            ListOfUnvisitedCell.Remove(currentCell);
        }

        private void SetVar()
        {
            ListOfUnvisitedCell = new List<Cell>();
            unvisitedNeighbors = new List<Cell>();
            pause = false;
            stack = new Stack<Cell>();
            currentCell = mazeDrawer.Cells[0, 0];
        }
        #endregion

        #region Handler Pause and Abort
        internal void HandlePause()
        {
            pause ^= true;
        }

        internal void HandleAbort()
        {
            if (!pause)
                backgroundWorker.CancelAsync();
        }
        #endregion

        #region Start Dfs
        internal void Start()
        {
            
            while (ListOfUnvisitedCell.Count > 0)
            {
                while (pause)
                    Thread.Sleep(100);

                if (backgroundWorker.CancellationPending)
                    return;

                unvisitedNeighbors = GetNeighbors();

                if (unvisitedNeighbors != null)
                    VisitNeightbors();
                else if (stack.Count != 0)
                    GoBack();

                Sleep();
                ReportProgress();
                SetTargetCellsColor();
            }
            RemoveTheCurrentCell();
        }
        #endregion

        #region Go Back
        private void GoBack()
        {
            RemoveTheCurrentCell();
            currentCell = stack.Pop();
            GetCurrentCell();
        }
        #endregion

        #region Visit Neightbots
        /// <summary>
        /// visit the neightbors selected at random and remove walls
        /// </summary>
        private void VisitNeightbors()
        {
            chosenCell = RandomPick();

            stack.Push(currentCell);

            Application.Current.Dispatcher.Invoke(new Action(() => { RemoveWalls(); }));
            RemoveTheCurrentCell();

            currentCell = chosenCell;
            GetCurrentCell();
            ListOfUnvisitedCell.Remove(currentCell);
        }

        /// <summary>
        /// select a random cell from the unvisitedNeighbors list
        /// </summary>
        /// <returns> return a cell selected at random</returns>
        private Cell RandomPick()
        {
            Random random = new Random();
            int i;

            if (unvisitedNeighbors.Count == 1)
                return unvisitedNeighbors[0];

            i = random.Next(unvisitedNeighbors.Count);
            return unvisitedNeighbors[i];
        }

        /// <summary>
        /// remove the walls of the selected cell and the chosen cell
        /// </summary>
        private void RemoveWalls()
        {
            if (CurrentCellAndChosenCellOnTheSameColumn())
            {
                if (ChosenCellIsOnLeft())
                {
                    currentCell.RightWall = false;
                    chosenCell.LeftWall = false;
                }
                else
                {
                    currentCell.LeftWall = false;
                    chosenCell.RightWall = false;
                }
            }
            else
            {
                if (ChosenCellIsUnder())
                {
                    currentCell.BottomWall = false;
                    chosenCell.TopWall = false;
                }
                else
                {
                    currentCell.TopWall = false;
                    chosenCell.BottomWall = false;
                }
            }
        }

        /// <summary>
        /// check if the chosen cell is under the current cell
        /// </summary>
        private bool ChosenCellIsUnder()
        {
            return currentCell.Y < chosenCell.Y;
        }

        /// <summary>
        /// check if the chosen cell is left of the current cell
        /// </summary>
        /// <returns></returns>
        private bool ChosenCellIsOnLeft()
        {
            return currentCell.X < chosenCell.X;
        }

        /// <summary>
        /// if the current cell is on the same column
        /// </summary>
        /// <returns></returns>
        private bool CurrentCellAndChosenCellOnTheSameColumn()
        {
            return currentCell.Y == chosenCell.Y;
        }
        #endregion

        #region Change Cell color
        private void SetTargetCellsColor()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { mazeDrawer.Cells[0, 0].Background = mazeDrawer.Cells[mazeDrawer.NumberOfCellsX - 1, mazeDrawer.NumberOfCellsY - 1].Background = GlobalColors.TargerCellColor; }));
        }

        private void GetCurrentCell()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { currentCell.Background = GlobalColors.SelectedCellColor; }));
        }

        private void RemoveTheCurrentCell()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { currentCell.Background = GlobalColors.BackgroundColor; }));
        }
        #endregion

        #region Get Neighbors 
        /// <summary>
        /// add all the neighbors from the current cell
        /// </summary>
        /// <returns></returns>
        private List<Cell> GetNeighbors()
        {
            List<Cell> listOfNeighbours = new List<Cell>();

            FindCell(0, -1, listOfNeighbours);
            FindCell(-1, 0, listOfNeighbours);
            FindCell(0, 1, listOfNeighbours);
            FindCell(1, 0, listOfNeighbours);

            return listOfNeighbours.Count != 0 ? listOfNeighbours : null;
        }
       
        /// <summary>
        /// check if the cell has neighbors
        /// </summary>
        /// <param name="x"> x value range</param>
        /// <param name="y"> y value range</param>
        /// <param name="listOfNeighbours"> list that hold any neightbors</param>
        private void FindCell(int x, int y, List<Cell> listOfNeighbours)
        {
            Cell neighbor = ListOfUnvisitedCell.Find(c => c.Y == currentCell.Y + y && c.X == currentCell.X + x);
            if (neighbor != null)
                listOfNeighbours.Add(neighbor);
        }
        #endregion

        #region Sleep
        private int GetSleep()
        {
            return (int)Math.Pow(2, 10 - sleep.BoundNumber) + 4;
        }

        private void Sleep()
        {
            Thread.Sleep(GetSleep());
        }
        #endregion;

        #region Report Progres
        private void ReportProgress()
        {
            backgroundWorker.ReportProgress(GetPercentageOfCellUsed());
        }

        private int GetPercentageOfCellUsed()
        {
            int maxCell = mazeDrawer.NumberOfCellsX * mazeDrawer.NumberOfCellsY;
            int numberOfCellVisited = maxCell - ListOfUnvisitedCell.Count;

            decimal percentage = (decimal)numberOfCellVisited / maxCell;
            return (int)Math.Round(percentage * 100);
        }
        #endregion
    }
}