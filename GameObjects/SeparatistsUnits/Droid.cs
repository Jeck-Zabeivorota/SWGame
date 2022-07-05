using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SWGame.GameObjects.SeparatistsUnits
{
    public class Droid : Unit
    {
        public override UnitType GetUnitType() => UnitType.Enemy;

        public override void TakeDamage(Attack attack)
        {
            Healt -= attack.Damage;
            DamageAnimation();
        }

        public Droid()
        {
            Background = new ImageBrush(new BitmapImage(new Uri("D:\\Droid.jpg")));

            MaxHealt = 80;
            Healt = 80;

            Distance = 4;
            Initiative = 5;

            Attacks.Add(new Attack("Выстрел с бластера", AttackType.Laser, new BitmapImage(new Uri("D:\\RedLaser.png")), 20));
            Attacks.Add(new Attack("Мощный выстрел", AttackType.Grenade, new BitmapImage(new Uri("D:\\RedLaser.png")), 30, 2));
        }
    }
}
