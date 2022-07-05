using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SWGame.GameObjects
{
    public enum AttackType { Laser, Missile, ArtilleryShell, Ion, Grenade }

    public class Attack
    {
        public string Name;
        public AttackType Type;
        public ImageSource Sprite;
        public int Damage, Recharge;

        public Attack(string name, AttackType type, ImageSource sprite, int damage, int recharge = 0)
        {
            Name = name;
            Type = type;
            Sprite = sprite;
            Damage = damage;
            Recharge = recharge;
        }
    }
}
