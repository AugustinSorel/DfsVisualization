using System.Windows.Media;

namespace DfsVisualization
{
    static class GlobalColors
    {
        private const string BACKGROUND_COLOR = "#0c2461";
        private const string CELL_COLOR = "#38ada9";

        public static Brush GetBackGroundColor()
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(BACKGROUND_COLOR));
        }

        public static Brush GetCellColor()
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(CELL_COLOR));
        }
    }
}
