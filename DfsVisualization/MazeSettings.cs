namespace DfsVisualization
{
    class MazeSettings
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
            startCellX = 0;
            startCellY = 0;
        }
    }
}
