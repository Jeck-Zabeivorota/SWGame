using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SWGame.GameMap
{
    class MoveAnimation
    {
        public UIElement Element;
        public int XMap, YMap;
        public double Speed;

        int EndAnimationCounter = 0;

        public event EventHandler EndAnimation;

        void Animation_Completed(object sender, EventArgs e)
        {
            EndAnimationCounter++;

            if (EndAnimationCounter == 2)
            {
                EndAnimationCounter = 0;
                EndAnimation?.Invoke(Element, new EventArgs());
            }
        }

        public void Start()
        {
            double xLocal = (double)Element.GetValue(Canvas.LeftProperty);
            double yLocal = (double)Element.GetValue(Canvas.TopProperty);

            int xDelta = (int)(Math.Abs(XMap - xLocal) / GMap.CellSize);
            int yDelta = (int)(Math.Abs(YMap - yLocal) / GMap.CellSize);

            double duration = (xDelta > yDelta ? xDelta : yDelta) / Speed;

            DoubleAnimation leftAnimation = new DoubleAnimation(XMap, TimeSpan.FromSeconds(duration));
            DoubleAnimation topAnimation = new DoubleAnimation(YMap, TimeSpan.FromSeconds(duration));

            leftAnimation.Completed += Animation_Completed;
            topAnimation.Completed += Animation_Completed;

            Element.BeginAnimation(Canvas.LeftProperty, leftAnimation);
            Element.BeginAnimation(Canvas.TopProperty, topAnimation);
        }

        public MoveAnimation(UIElement element, int xMap, int yMap, double speed)
        {
            Element = element;
            XMap = xMap;
            YMap = yMap;
            Speed = speed;
        }
    }
}
