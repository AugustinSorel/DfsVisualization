using System;
using System.Windows;

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

        // 15 * 
        // 14 *

        // Ending X and Y values of maze

        internal void Start()
        {
            CreateMaze();
            return;
            for (int i = 0; i < maze.GetLength(1); i++)
            {
                for (int j = 0; j < maze.GetLength(0); j++)
                {
                    MessageBox.Show(maze[j, i].ToString());
                }
                MessageBox.Show("** End j **");
            }

            //bool[,] maze = new bool[mazeSettings.NumberOfCellsX, mazeSettings.NumberOfCellsY]; // The maze
            //bool[,] wasHere = new bool[mazeSettings.NumberOfCellsX, mazeSettings.NumberOfCellsY];
            //bool[,] correctPath = new bool[mazeSettings.NumberOfCellsX, mazeSettings.NumberOfCellsY]; // The solution to the maze
            //int startX, startY; // Starting X and Y values of maze
            //int endX, endY;

            //maze = generateMaze(); // Create Maze (false = path, true = wall)
            //for (int row = 0; row < maze.length; row++)
            //    // Sets boolean Arrays to default values
            //    for (int col = 0; col < maze[row].length; col++)
            //    {
            //        wasHere[row][col] = false;
            //        correctPath[row][col] = false;
            //    }
            //bool b = recursiveSolve(startX, startY);
        }

        // size: 9 * 9
        private void CreateMaze()
        {
            maze = new bool[mazeSettings.NumberOfCellsX * 2 - 1, mazeSettings.NumberOfCellsY * 2 - 1];

            for (int i = 0; i < maze.GetLength(1) ; i++)
            {
                for (int j = 0; j < maze.GetLength(0); j++)
                {
                    if (i % 2 == 1) // hit a bottom corner
                    {
                        if (j % 2 == 0) // cell 
                        {
                            if (mazeDrawer.Cells[j / 2, i / 2].BottomWall == false)
                                maze[j, i] = true;
                        }

                        continue;
                    }

                    if (j % 2 == 1) // hit left a corner
                    {
                        if (mazeDrawer.Cells[j/2, i/2].RightWall == false)
                            maze[j, i] = true;
                        
                        continue;
                    }

                    maze[j, i] = true;
                }
            }
        }
    }
}