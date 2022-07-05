using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SWGame.GameObjects.RepublicUnits
{
    class CloneSolder : Unit
    {
        public override UnitType GetUnitType() => UnitType.Own;

        public override void TakeDamage(Attack attack)
        {
            Healt -= attack.Damage;
            DamageAnimation();
        }

        public CloneSolder()
        {
            Background = new ImageBrush(new BitmapImage(new Uri("D:\\Clone.jpg")));

            MaxHealt = 100;
            Healt = 100;

            Distance = 4;
            Initiative = 5;

            Attacks.Add(new Attack( "Выстрел с бластера", AttackType.Laser, new BitmapImage(new Uri("D:\\BlueLaser.png")), 25));
            Attacks.Add(new Attack("Граната", AttackType.Grenade, new BitmapImage(new Uri("D:\\Grenade.png")), 50, 3));
        }
    }
}
