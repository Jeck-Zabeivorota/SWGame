using System;
using System.Windows.Controls;
using System.Windows.Input;

using SWGame.GameMap;

namespace SWGame.GameObjects
{
    public partial class EffectCell : Image, IMapObject
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
        void Effect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => isPressed = true;

        void Effect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isPressed) Click?.Invoke(this, EventArgs.Empty);
        }

        void Effect_MouseLeave(object sender, MouseEventArgs e) => isPressed = false;

        #endregion


        public EffectCell()
        {
            InitializeComponent();
        }
    }
}
