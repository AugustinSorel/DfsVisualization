using System.Windows.Media;

namespace DfsVisualization
{
    static class GlobalColors
    {
        private const string BACKGROUND_COLOR = "#34495e";
        private const string CELL_COLOR = "#2c3e50";

        public static Brush BackgroundColor
        {
            get { return (SolidColorBrush)(new BrushConverter().ConvertFrom(BACKGROUND_COLOR)); }
        }

        public static Brush CellColor
        {
            get { return (SolidColorBrush)(new BrushConverter().ConvertFrom(CELL_COLOR)); }
        }

        public static string CellColorString
        {
            get { return CELL_COLOR; }
        }
    }
}