using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DfsVisualization
{
    class OpacityFade
    {
        private Storyboard storyboard;
        private DoubleAnimation doubleAnimation;

        public OpacityFade(SettingsUserControl settingsUserControl)
        {
            storyboard = new Storyboard();
            doubleAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(2))
            };

            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(UserControl.OpacityProperty));
            Storyboard.SetTarget(doubleAnimation, settingsUserControl);
        }

        internal void StartAnimation(bool v)
        {
            if (v)
            {
                doubleAnimation.From = 0;
                doubleAnimation.To = 1;
            }
            else
            {
                doubleAnimation.From = 1;
                doubleAnimation.To = 0;
                storyboard.Completed += new EventHandler(Sb_Completed);
            }

            storyboard.Begin();
        }


        private void Sb_Completed(object sender, EventArgs e)
        {
            (Application.Current.Windows[0] as MainWindow).RemoveSettingsToContainer();
        }
    }
}
