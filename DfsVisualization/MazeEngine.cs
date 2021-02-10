using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DfsVisualization
{
    public class MazeEngine
    {
        private readonly MazeDrawer mazeDrawer;

        public MazeEngine(MazeDrawer mazeDrawer)
        {
            this.mazeDrawer = mazeDrawer;
        }

        internal void Test()
        {
            BackgroundWorker worker = new BackgroundWorker();
            //assign it work
            worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //start work
            if (worker.IsBusy != true)
            {
                worker.RunWorkerAsync();
            }
        }

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < mazeDrawer.NumberOfCellsY; i++)
            {
                for (int j = 0; j < mazeDrawer.NumberOfCellsX; j++)
                {
                    Application.Current.Dispatcher.Invoke(() => { 
                        mazeDrawer.Cells[j, i].Background = GlobalColors.BackgroundColor;
                    });

                    Thread.Sleep(10);
                }
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End");
        }
    }
}