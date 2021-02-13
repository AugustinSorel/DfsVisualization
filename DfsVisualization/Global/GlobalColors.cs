using System.Windows.Media;

namespace DfsVisualization
{
    static class GlobalColors
    {
        private const string BACKGROUND_COLOR = "#34495e";
        private const string CELL_COLOR = "#2c3e50";
        private const string SELECTED_CELL_COLOR = "#c0392b";
        private const string TARGET_CELL_COLOR = "#27ae60";

        public static Brush BackgroundColor
        {
            get { return (SolidColorBrush)(new BrushConverter().ConvertFrom(BACKGROUND_COLOR)); }
        }

        public static Brush CellColor
        {
            get { return (SolidColorBrush)(new BrushConverter().ConvertFrom(CELL_COLOR)); }
        }

        public static Brush SelectedCellColor
        {
            get { return (SolidColorBrush)(new BrushConverter().ConvertFrom(SELECTED_CELL_COLOR)); }
        }

        public static Brush TargerCellColor
        {
            get { return (SolidColorBrush)(new BrushConverter().ConvertFrom(TARGET_CELL_COLOR)); }
        }

        public static string CellColorString
        {
            get { return CELL_COLOR; }
        }
    }
}