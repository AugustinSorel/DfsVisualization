namespace DfsVisualization
{
    public class MazeSettings
    {
        #region Fields
        private int startCellY;
        private int startCellX;
        #endregion

        #region Properties
        public int StartCellY
        {
            get { return startCellY; }
            set { startCellY = value; }
        }

        public int StartCellx
        {
            get { return startCellX; }
            set { startCellX = value; }
        }
        #endregion

        public MazeSettings()
        {
            startCellX = 10;
            startCellY = 10;
        }
    }
}
