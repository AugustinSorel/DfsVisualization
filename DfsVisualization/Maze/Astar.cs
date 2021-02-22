using System;
using System.IO;
using System.Windows;

namespace DfsVisualization
{
    internal class Astar
    {
        #region Fields
        private readonly MazeDrawer mazeDrawer;
        private readonly MazeSettings mazeSettings;

        private bool[,] correctPath;
        private bool[,] maze;

        private bool[,] wasHere;

        private int startX, startY;
        private int endX, endY;
        #endregion

        #region Properties
        public bool[,] Maze
        {
            get { return maze; }
            set { maze = value; }
        }

        public bool[,] CorrectPath
        {
            get { return correctPath; }
            set { correctPath = value; }
        }
        #endregion

        public Astar(MazeDrawer mazeDrawer, MazeSettings mazeSettings)
        {
            this.mazeDrawer = mazeDrawer;
            this.mazeSettings = mazeSettings;

            DrawStartCell();
            DrawEndCell();
        }

        private void DrawStartCell()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { mazeDrawer.Cells[mazeSettings.AStarStartX, mazeSettings.AStarStartY].Background = GlobalColors.TargerCellColor; }));
        }

        private void DrawEndCell()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { mazeDrawer.Cells[mazeSettings.AStartEndX, mazeSettings.AStartEndY].Background = GlobalColors.TargerCellColor; }));
        }

        internal void Start()
        {
            CreateMaze();

            wasHere = new bool[maze.GetLength(0), maze.GetLength(1)];
            correctPath = new bool[maze.GetLength(0), maze.GetLength(1)];

            startX = mazeSettings.AStarStartX * 2;
            endX = mazeSettings.AStartEndX * 2;

            startY = mazeSettings.AStarStartY * 2;
            endY = mazeSettings.AStartEndY * 2;

            for (int row = 0; row < maze.GetLength(1) - 4; row++) // I HAVE NO IDEA WHY -4 HERRE, but it is working so....
                for (int col = 0; col < maze.GetLength(0) - 4; col++) // I HAVE NO IDEA WHY -4 HERRE, but it is working so....
                {
                    wasHere[row, col] = false;
                    correctPath[row, col] = false;
                }
            bool b = RecursiveSolve(startX, startY);

            if (!b)
                MessageBox.Show("Not Solvable");
        }

        private bool RecursiveSolve(int x, int y)
        {
            if (x == endX && y == endY) return true; // If you reached the end
            if (maze[x, y] || wasHere[x, y]) return false;
            // If you are on a wall or already were here
            wasHere[x, y] = true;
            if (x != 0) // Checks if not on left edge
                if (RecursiveSolve(x - 1, y))
                { // Recalls method one to the left
                    correctPath[x, y] = true; // Sets that path value to true;
                    return true;
                }
            if (x != maze.GetLength(0) - 1) // Checks if not on right edge
                if (RecursiveSolve(x + 1, y))
                { // Recalls method one to the right
                    correctPath[x, y] = true;
                    return true;
                }
            if (y != 0)  // Checks if not on top edge
                if (RecursiveSolve(x, y - 1))
                { // Recalls method one up
                    correctPath[x, y] = true;
                    return true;
                }
            if (y != maze.GetLength(1) - 1) // Checks if not on bottom edge
                if (RecursiveSolve(x, y + 1))
                { // Recalls method one down
                    correctPath[x, y] = true;
                    return true;
                }
            return false;
        }

        private void CreateMaze()
        {
            maze = new bool[mazeSettings.NumberOfCellsX * 2 - 1, mazeSettings.NumberOfCellsY * 2 - 1];

            for (int i = 0; i < maze.GetLength(1); i++)
                for (int j = 0; j < maze.GetLength(0); j++)
                    maze[j, i] = true;

            for (int i = 0; i < maze.GetLength(1) ; i++)
            {
                for (int j = 0; j < maze.GetLength(0); j++)
                {
                    if (i % 2 == 1) // hit a bottom corner
                    {
                        if (j % 2 == 0) // cell 
                        {
                            if (mazeDrawer.Cells[j / 2, i / 2].BottomWall == false)
                                maze[j, i] = false;
                        }

                        continue;
                    }

                    if (j % 2 == 1) // hit left a corner
                    {
                        if (mazeDrawer.Cells[j/2, i/2].RightWall == false)
                            maze[j, i] = false;
                        
                        continue;
                    }

                    maze[j, i] = false;
                }
            }
        }
    }
}