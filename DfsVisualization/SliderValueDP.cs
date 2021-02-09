using System.Windows;

namespace DfsVisualization
{
    class SliderValueDP : DependencyObject
    {
        public int SliderValue
        {
            get { return (int)GetValue(SliderValueProperty); }
            set { SetValue(SliderValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Prop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderValueProperty =
            DependencyProperty.Register("SliderValue", typeof(int), typeof(MainWindow), new PropertyMetadata(5));
    }
}
