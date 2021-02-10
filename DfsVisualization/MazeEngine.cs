using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace DfsVisualization
{
    public class MazeEngine
    {
        private readonly MazeDrawer mazeDrawer;
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public MazeEngine(MazeDrawer mazeDrawer)
        {
            this.mazeDrawer = mazeDrawer;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        internal void Test()
        {
            T();
        }

        private async void T()
        {
            for (int i = 0; i < mazeDrawer.NumberOfCellsY; i++)
            {
                for (int j = 0; j < mazeDrawer.NumberOfCellsX; j++)
                {
                    mazeDrawer.Cells[j, i].Background = GlobalColors.BackgroundColor;
                    await Task.Delay(10);
                }
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }
    }
}