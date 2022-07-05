using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using SWGame.GameMap;

namespace SWGame.GameObjects
{
    public enum UnitType { Own, Ally, Enemy }

    public partial class Unit : Border, IMapObject
    {
        #region Fields

        public int X { get; set; }
        public int Y { get; set; }

        public int XMap => GMap.GetMapPosition(X);
        public int YMap => GMap.GetMapPosition(Y);

        public int XSize { get; protected set; } = 1;
        public int YSize { get; protected set; } = 1;

        public bool Fulled => true;

        public int Distance { get; protected set; }

        public int Initiative { get; protected set; }

        public bool Invulnerable = false;


        public readonly List<Attack> Attacks = new List<Attack>();


        public int MaxHealt, MaxShield = 0;

        int _healt = 1;
        public int Healt
        {
            get => _healt;
            set
            {
                if (value > MaxHealt) _healt = MaxHealt;
                else if (value < 0)   _healt = 0;
                else                  _healt = value;

                double ratio = _healt / (double)MaxHealt;

                XHealtLevel.Width = Width * ratio;

                if (ratio > 0.8)      XHealtLevel.Fill = Brushes.Green;
                else if (ratio > 0.6) XHealtLevel.Fill = Brushes.YellowGreen;
                else if (ratio > 0.4) XHealtLevel.Fill = Brushes.GreenYellow;
                else if (ratio > 0.2) XHealtLevel.Fill = Brushes.Orange;
                else                  XHealtLevel.Fill = Brushes.Red;
            }
        }


        int _shield = 0;
        public int Shield
        {
            get => _shield;
            set
            {
                if (value > MaxShield) _shield = MaxShield;
                else if (value < 0)    _shield = 0;
                else                   _shield = value;

                XShieldLevel.Width = Width * (_shield / MaxShield);
            }
        }

        #endregion


        #region Events

        public event EventHandler Click;

        #endregion


        #region Methods

        public virtual UnitType GetUnitType() => default;


        protected void DamageAnimation()
        {
            var animation = new DoubleAnimation(0.6, TimeSpan.FromSeconds(0.1)) { AutoReverse = true };
            BeginAnimation(OpacityProperty, animation);
        }

        public virtual void TakeDamage(Attack attack) { }


        bool isPressed = false;
        void Unit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => isPressed = true;

        void Unit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isPressed) Click?.Invoke(this, EventArgs.Empty);
        }

        void Unit_MouseLeave(object sender, MouseEventArgs e) => isPressed = false;

        #endregion


        public Unit()
        {
            InitializeComponent();
        }
    }
}
