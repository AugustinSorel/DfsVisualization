using System;
using System.Collections.Generic;
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



        internal void Start()
        {
            CreateMaze();
            return;
            //for (int i = 0; i < maze.GetLength(1); i++)
            //{
            //    for (int j = 0; j < maze.GetLength(0); j++)
            //    {
            //        MessageBox.Show(maze[j, i].ToString());
            //    }
            //    MessageBox.Show("** End j **");
            //}

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