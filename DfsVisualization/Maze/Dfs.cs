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
        private List<Cell> unvisitedNeighbors;
        private Cell currentCell;
        private Cell chosenCell;

        private readonly List<Cell> ListOfUnvisitedCell;
        private readonly Stack<Cell> stack;
        private readonly MazeDrawer mazeDrawer;
        private readonly BackgroundWorker backgroundWorker;
        private readonly SliderValue sleep;

        public Dfs(MazeDrawer mazeDrawer, BackgroundWorker backgroundWorker, SliderValue sleep)
        {
            this.mazeDrawer = mazeDrawer;
            this.backgroundWorker = backgroundWorker;
            this.sleep = sleep;

            ListOfUnvisitedCell = new List<Cell>();
            unvisitedNeighbors = new List<Cell>();
            for (int i = 0; i < mazeDrawer.NumberOfCellsY; i++)
                for (int j = 0; j < mazeDrawer.NumberOfCellsX; j++)
                    ListOfUnvisitedCell.Add(mazeDrawer.Cells[j, i]);

            stack = new Stack<Cell>();
            currentCell = mazeDrawer.Cells[0, 0];
            ListOfUnvisitedCell.Remove(currentCell);
        }

        internal void Start()
        {
            while (ListOfUnvisitedCell.Count > 0)
            {
               // while (pause)
                 //   Thread.Sleep(100);

                if (backgroundWorker.CancellationPending)
                    return;

                unvisitedNeighbors = GetNeighbors(ListOfUnvisitedCell);
                if (unvisitedNeighbors != null)
                    VisitNeightbors();
                else if (stack.Count != 0)
                    GoBack();
                Thread.Sleep(GetSleep());
                backgroundWorker.ReportProgress(GetPercentageOfCellUsed());
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

        private void RemoveWalls(Cell currentCell, Cell chosenCell)
        {
            if (currentCell.Y == chosenCell.Y)
            {
                if (currentCell.X < chosenCell.X)
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
                if (currentCell.Y < chosenCell.Y)
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


        private List<Cell> GetNeighbors(List<Cell> listofUnvisitedCells)
        {
            List<Cell> listOfNeighbours = new List<Cell>();

            FindCell(0, -1, listofUnvisitedCells, listOfNeighbours);
            FindCell(-1, 0, listofUnvisitedCells, listOfNeighbours);
            FindCell(0, 1, listofUnvisitedCells, listOfNeighbours);
            FindCell(1, 0, listofUnvisitedCells, listOfNeighbours);

            return listOfNeighbours.Count != 0 ? listOfNeighbours : null;
        }

        private void FindCell(int x, int y, List<Cell> listofUnvisitedCells, List<Cell> listOfNeighbours)
        {
            Cell neighbor = listofUnvisitedCells.Find(c => c.Y == currentCell.Y + y && c.X == currentCell.X + x);
            if (neighbor != null)
                listOfNeighbours.Add(neighbor);
        }


        private int GetSleep()
        {
            return (int)Math.Pow(2, 10 - sleep.BoundNumber) + 4;
        }

        private int GetPercentageOfCellUsed()
        {
            int maxCell = mazeDrawer.NumberOfCellsX * mazeDrawer.NumberOfCellsY;
            int numberOfCellVisited = maxCell - ListOfUnvisitedCell.Count;

            decimal percentage = (decimal)numberOfCellVisited / maxCell;
            return (int)Math.Round(percentage * 100);
        }

        private Cell RandomPick(List<Cell> unvisitedNeighbours)
        {
            Random random = new Random();
            int i;

            if (unvisitedNeighbours.Count == 1)
                return unvisitedNeighbours[0];

            i = random.Next(unvisitedNeighbours.Count);
            return unvisitedNeighbours[i];
        }
    }
}