using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using SWGame.GameMap;

using SWGame.GameObjects;

namespace SWGame.GameController
{
    public static class GController
    {
        static GMap _map;
        public static GMap Map
        {
            get => _map;
            set
            {
                if (_map != null) _map.ObjectClick -= ObjectClick;
                _map = value;
                _map.ObjectClick += ObjectClick;
                EnemyController.Map = value;
            }
        }

        static Unit SelectUnit;

        static EnemyController EnemyController = new EnemyController(Map);

        static StepsQueue StepsQueue = new StepsQueue();

        static readonly List<MoveCell> MoveCells = new List<MoveCell>();


        static void ExposeSteps(Unit unit)
        {
            int xStart = unit.X - unit.Distance,
                yStart = unit.Y - unit.Distance,
                xEnd   = unit.X + unit.Distance,
                yEnd   = unit.Y + unit.Distance;

            xStart = xStart >= 0 ? xStart : 0;
            yStart = yStart >= 0 ? yStart : 0;
            xEnd = xEnd < Map.Rows ? xEnd : Map.Rows - 1;
            yEnd = yEnd < Map.Columns ? yEnd : Map.Columns - 1;

            for (int y = yStart; y <= yEnd; y++)
                for (int x = xStart; x <= xEnd; x++)
                    if (Map.CheckCoordinate(unit, x, y))
                    {
                        MoveCell cell = new MoveCell { X = x, Y = y };
                        Map.AddObject(cell);
                        MoveCells.Add(cell);
                    }
        }

        static void RemoveMoveCells()
        {
            foreach (MoveCell cell in MoveCells)
                Map.RemoveObject(cell);
            MoveCells.Clear();
        }

        static double GetAngle(IMapObject obj, IMapObject target)
        {
            return Math.Atan2(obj.Y - target.Y, obj.X - target.X) * 180 / Math.PI;
        }

        static void MakeDamage(Unit attacking, Unit target, Attack attack, EventHandler affterFunc = null)
        {
            EffectCell effect = new EffectCell
            {
                Source = attack.Sprite,
                X = attacking.X,
                Y = attacking.Y,
                RenderTransformOrigin = new Point(0.5, 0.5),
                RenderTransform = new RotateTransform(GetAngle(attacking, target))
            };
            Map.AddObject(effect);


            Map.MoveObject(effect, target.X, target.Y, 10, (element, e) =>
            {
                Map.RemoveObject(effect);

                if (!target.Invulnerable)
                    target.TakeDamage(attack);

                affterFunc?.Invoke(attacking, EventArgs.Empty);
            });
        }


        static void Next()
        {
            SelectUnit = StepsQueue.Next();
            UnitType type = SelectUnit.GetUnitType();

            if (type == UnitType.Own)
                ExposeSteps(SelectUnit);

            else if (type == UnitType.Enemy)
                EnemyController.MakeStep(SelectUnit);
        }

        public static void ObjectClick(object sender, EventArgs e)
        {
            if (sender == null || sender == SelectUnit) return;

            if (sender is MoveCell cell)
                Map.MoveObject(SelectUnit, cell.X, cell.Y, 3, (element, _) => Next());

            else if (sender is Unit unit)
            {
                UnitType unitType = unit.GetUnitType(),
                         selectType = SelectUnit.GetUnitType();

                if ((selectType == UnitType.Own && unitType == UnitType.Enemy) || (selectType == UnitType.Enemy && unitType == UnitType.Own))
                {
                    MakeDamage(SelectUnit, unit, SelectUnit.Attacks[0], (attacking, _) =>
                    {
                        if (unit.Healt == 0)
                        {
                            Map.RemoveObject(unit);
                            StepsQueue.Remove(unit);
                        }
                        Next();
                    });
                }
            }

            RemoveMoveCells();
        }


        public static void AddToMap(params IMapObject[] objects)
        {
            foreach (IMapObject obj in objects)
            {
                Map.AddObject(obj);
                StepsQueue.Add((Unit)obj);
            }
        }

        public static void RemoveFromMap(params IMapObject[] objects)
        {
            foreach (IMapObject obj in objects)
            {
                Map.RemoveObject(obj);
                StepsQueue.Remove((Unit)obj);
            }
        }

        public static void ClearMap()
        {
            Map.Clear();
            StepsQueue.Clear();
        }

        public static void StartGame() => Next();
    }
}
