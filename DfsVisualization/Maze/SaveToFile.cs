﻿using System.IO;

namespace DfsVisualization
{
    internal class SaveToFile
    {
        private readonly DfsSolver astar;

        public SaveToFile(DfsSolver astar)
        {
            this.astar = astar;
        }

        internal void SaveMazeToTextFile()
        {
            using (var sw = new StreamWriter("Maze.txt"))
            {
                for (int i = 0; i < astar.Maze.GetLength(1); i++)
                {
                    for (int j = 0; j < astar.Maze.GetLength(0); j++)
                    {
                        string temp = astar.Maze[j, i] ? "O" : "X";
                        sw.Write(temp + " ");
                    }
                    sw.Write("\n");
                }

                sw.Flush();
                sw.Close();
            }
        }

        internal void SaveMazeSolutionToFile()
        {
            using (var sw = new StreamWriter("MazeSolution.txt"))
            {
                for (int i = 0; i < astar.Maze.GetLength(1); i++)
                {
                    for (int j = 0; j < astar.Maze.GetLength(0); j++)
                    {
                        string temp = astar.CorrectPath[j, i] ? "X" : "O";
                        sw.Write(temp + " ");
                    }
                    sw.Write("\n");
                }

                sw.Flush();
                sw.Close();
            }
        }
    }
}