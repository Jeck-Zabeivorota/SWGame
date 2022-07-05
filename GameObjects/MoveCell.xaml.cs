using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Windows;

using SWGame.GameMap;

namespace SWGame.GameObjects
{
    public partial class MoveCell : Border, IMapObject
    {
        #region Fields

        public int X { get; set; }
        public int Y { get; set; }

        public int XMap => GMap.GetMapPosition(X);
        public int YMap => GMap.GetMapPosition(Y);

        public int XSize => 1;
        public int YSize => 1;

        public bool Fulled => false;

        #endregion


        #region Events

        public event EventHandler Click;

        #endregion


        #region Methods

        bool isPressed = false;
        void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isPressed = true;
            Opacity = 0.25;
        }

        void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Opacity = 0.15;
            if (isPressed) Click?.Invoke(this, EventArgs.Empty);
        }

        void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Opacity = 0.15;
        }

        void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            isPressed = false;
            Opacity = 0.1;
        }

        #endregion


        public MoveCell()
        {
            InitializeComponent();
        }
    }
}
