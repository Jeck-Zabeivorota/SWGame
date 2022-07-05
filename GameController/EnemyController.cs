using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using SWGame.GameMap;
using SWGame.GameObjects;

namespace SWGame.GameController
{
    class EnemyController
    {
        public GMap Map;

        const int VisibilityArea = 10;


        List<Unit> FindTargets(Unit enemy)
        {
            List<Unit> targets = new List<Unit>();

            int xStart = enemy.X - enemy.Distance,
                yStart = enemy.Y - enemy.Distance,
                xEnd = enemy.X + enemy.Distance,
                yEnd = enemy.Y + enemy.Distance;

            xStart = xStart >= 0 ? xStart : 0;
            yStart = yStart >= 0 ? yStart : 0;
            xEnd = xEnd < Map.Rows ? xEnd : Map.Rows - 1;
            yEnd = yEnd < Map.Columns ? yEnd : Map.Columns - 1;

            for (int y = yStart; y <= yEnd; y++)
                for (int x = xStart; x <= xEnd; x++)
                    if (Map.GetObject(x, y) is Unit unit && unit.GetUnitType() == UnitType.Own)
                        targets.Add(unit);

            return targets;
        }

        public void MakeStep(Unit enemy)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            
            List<Unit> targets = FindTargets(enemy);

            if (targets.Count > 0)
            {
                GController.ObjectClick(targets[rand.Next(0, targets.Count)], EventArgs.Empty);
                return;
            }
        }

        public EnemyController(GMap map) => Map = map;
    }
}
