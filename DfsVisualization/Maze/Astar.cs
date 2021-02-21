using System;
using System.Collections.Generic;
using System.Windows;

namespace DfsVisualization
{
    internal class Astar
    {
        private readonly MazeDrawer mazeDrawer;
        private readonly MazeSettings mazeSettings;

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

        private bool[,] maze;

        internal void Start()
        {
            CreateMaze();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
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

        private void CreateMaze()
        {
            maze = new bool[mazeSettings.NumberOfCellsX * 2 - 1, mazeSettings.NumberOfCellsY * 2 - 1];

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (j + 1 > 20)
                        continue;

                    if (i + 1 > 20)
                        continue;

                    if (j % 2 == 1)
                    {
                        continue;
                    }

                    

                    int x;
                    int y;

                    x = j > 0 ? j / 2 : j;
                    y = i > 0 ? i / 2 + 1 : i;

                    maze[j, i] = true;

                    if (mazeDrawer.Cells[x, y].RightWall == false)
                        maze[j + 1, i] = true;
                    else
                        maze[j + 1, i] = false;

                    if (mazeDrawer.Cells[x, y].BottomWall == false)
                        maze[j, i + 1] = true;
                    else
                        maze[j, i + 1] = false;
                }
            }
        }
    }
}