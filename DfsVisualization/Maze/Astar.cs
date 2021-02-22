using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace DfsVisualization
{
    internal class Astar
    {
        private readonly MazeDrawer mazeDrawer;
        private readonly MazeSettings mazeSettings;

        private bool[,] maze;
        public bool[,] Maze
        {
            get { return maze; }
            set { maze = value; }
        }


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

        bool[,] wasHere;
        bool[,] correctPath; // The solution to the maze
        int startX, startY; // Starting X and Y values of maze
        int endX, endY; 

        internal void Start()
        {
            CreateMaze();

            wasHere = new bool[maze.GetLength(1),maze.GetLength(1)];
            correctPath = new bool[maze.GetLength(1),maze.GetLength(1)];

            startX = 0;
            endX = 4;

            startY = 0;
            endY = 4;

            for (int row = 0; row < maze.GetLength(1); row++)
                // Sets boolean Arrays to default values
                for (int col = 0; col < maze.GetLength(0); col++)
                {
                    wasHere[row, col] = false;
                    correctPath[row, col] = false;
                }
            bool b = recursiveSolve(startX, startY);



            using (var sw = new StreamWriter("Solution.txt"))
            {
                for (int i = 0; i < Maze.GetLength(1); i++)
                {
                    for (int j = 0; j < Maze.GetLength(0); j++)
                    {
                        string temp = correctPath[j, i] ? "X" : "O";
                        sw.Write(temp + " ");
                    }
                    sw.Write("\n");
                }

                sw.Flush();
                sw.Close();
            }

        }

        private bool recursiveSolve(int x, int y)
        {
            if (x == endX && y == endY) return true; // If you reached the end
            if (maze[x, y] || wasHere[x, y]) return false;
            // If you are on a wall or already were here
            wasHere[x, y] = true;
            if (x != 0) // Checks if not on left edge
                if (recursiveSolve(x - 1, y))
                { // Recalls method one to the left
                    correctPath[x, y] = true; // Sets that path value to true;
                    return true;
                }
            if (x != maze.GetLength(0) - 1) // Checks if not on right edge
                if (recursiveSolve(x + 1, y))
                { // Recalls method one to the right
                    correctPath[x, y] = true;
                    return true;
                }
            if (y != 0)  // Checks if not on top edge
                if (recursiveSolve(x, y - 1))
                { // Recalls method one up
                    correctPath[x, y] = true;
                    return true;
                }
            if (y != maze.GetLength(1) - 1) // Checks if not on bottom edge
                if (recursiveSolve(x, y + 1))
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