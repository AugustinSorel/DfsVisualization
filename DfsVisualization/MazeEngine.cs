using System.ComponentModel;
using System.Threading.Tasks;

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
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    mazeDrawer.cells[j, i].Background = GlobalColors.BackgroundColor;
                    await Task.Delay(50);
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