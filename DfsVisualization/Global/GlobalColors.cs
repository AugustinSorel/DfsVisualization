using System.Windows.Media;

namespace DfsVisualization
{
    class GlobalColors
    {
        private const string BACKGROUND_COLOR = "#ff5252";

        public static Brush GetBackGroundColor()
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(BACKGROUND_COLOR));
        }
    }
}
