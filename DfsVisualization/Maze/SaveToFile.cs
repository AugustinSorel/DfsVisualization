using System;
using System.IO;

namespace DfsVisualization
{
    internal class SaveToFile
    {
        private readonly Astar astar;

        public SaveToFile(Astar astar)
        {
            this.astar = astar;
        }

        internal void SaveToTextFile()
        {
            using (var sw = new StreamWriter("outputText.txt"))
            {
                for (int i = 0; i < astar.Maze.GetLength(1); i++)
                {
                    for (int j = 0; j < astar.Maze.GetLength(0); j++)
                    {
                        string temp = astar.Maze[j, i] ? "X" : "O";
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