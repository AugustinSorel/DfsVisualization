using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace DfsVisualization
{
    class BlurAnimation
    {
        #region Fields
        private readonly Storyboard myStoryboard;
        private readonly DoubleAnimation myDoubleAnimation;
        private readonly BlurEffect blurEffect;
        #endregion

        #region Private Cons
        private const int DURATION = 20;
        #endregion

        public BlurAnimation()
        {
            myStoryboard = new Storyboard();
            myDoubleAnimation = new DoubleAnimation();
            blurEffect = new BlurEffect();
        }

        public void StartAnimation(bool enabled, Canvas mazeCanvas, MazeUserControl mazeUserControl)
        {
            mazeUserControl.RegisterName("blurEffect", blurEffect);
            blurEffect.Radius = 0.0;
            mazeCanvas.Effect = blurEffect;

            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));
            myDoubleAnimation.AutoReverse = false;

            Storyboard.SetTargetName(myDoubleAnimation, "blurEffect");
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(BlurEffect.RadiusProperty));
            myStoryboard.Children.Add(myDoubleAnimation);
            if (!enabled)
            {
                myDoubleAnimation.From = 0;
                myDoubleAnimation.To = DURATION;
            }
            else
            {
                myDoubleAnimation.From = DURATION;
                myDoubleAnimation.To = 0;
            }
            myStoryboard.Begin(mazeUserControl);
        }
    }
}
