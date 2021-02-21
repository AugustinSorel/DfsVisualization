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


        int startX = 0;
        int startY = 0;

        int endX = 4;
        int endY = 4;

        int x = 0;
        int y = 0;

        int i = 0;

        List<Cell> cells;

        internal void Start()
        {
            CreateMaze();

            //for (int i = 0; i < maze.GetLength(1); i++)
            //{
            //    for (int j = 0; j < maze.GetLength(0); j++)
            //    {
            //        MessageBox.Show(maze[j, i].ToString());
            //    }
            //    MessageBox.Show("** End j **");
            //}

            i = 0;

            Application.Current.Dispatcher.Invoke(new Action(() => {

                mazeDrawer.Cells[0, 0].Tag = 0;
            }));
            do
            {

                cells = new List<Cell>();

                GetN();

                MessageBox.Show(cells.Count.ToString()) ;

                foreach (var item in cells)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        item.Tag = i++;
                    }));
                }

                y++;
                x++;
                i++;

            } while ((startX == endX && startY == endY) || cells.Count > 0);

            foreach (var item in mazeDrawer.Cells)
            {
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    MessageBox.Show(item.Tag.ToString());
                }));
            }

        }

        private void GetN()
        {
            if (x+1 < 5 && mazeDrawer.Cells[x, y].RightWall == false)
            {
                cells.Add(mazeDrawer.Cells[x + 1, y]);
            }

            if (y + 1 < 5 && mazeDrawer.Cells[x , y].BottomWall== false)
            {
                cells.Add(mazeDrawer.Cells[x, y + 1]);
            }
        }

        private bool Test(int xx, int yy)
        {
            if (mazeDrawer.Cells[xx, yy].BottomWall == false)
                return true;


            if (mazeDrawer.Cells[xx, yy].RightWall == false)
                return true;


            return false;
        }

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