using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SWGame.GameObjects;

namespace SWGame.GameController
{
    class StepsQueue
    {
        readonly List<Unit> Units = new List<Unit>();

        int Iterator = 0;

        bool isUpdate = false;

        readonly List<Unit> _queue = new List<Unit>();
        public List<Unit> Queue
        {
            get
            {
                if (isUpdate) return _queue;


                Units.OrderBy(u => u.Initiative);

                int[] countSteps = Units.Select(u => u.Initiative).ToArray();

                _queue.Clear();

                bool isRanOutSteps;

                do
                {
                    isRanOutSteps = true;

                    for (int i = 0; i < countSteps.Length; i++)
                        if (countSteps[i] > 0)
                        {
                            _queue.Add(Units[i]);
                            countSteps[i]--;
                            isRanOutSteps = false;
                        }
                }
                while (!isRanOutSteps);

                isUpdate = true;

                return _queue;
            }
        }


        public Unit[] GetUnits() => Units.ToArray();

        public Unit Next()
        {
            if (Iterator == Queue.Count) Iterator = 0;
            return Queue[Iterator++];
        }

        public void Add(params Unit[] units)
        {
            if (Units.Count == 0)
            {
                Units.AddRange(units);
                return;
            }

            foreach (Unit unit in units)
                if (Units.Contains(unit)) throw new Exception("Unit is already exists");


            Unit selectUnit = Iterator < Queue.Count ? Queue[Iterator] : Queue[0];

            int countSelects = 0;

            for (int i = 0; i <= Iterator; i++)
                if (Queue[i] == selectUnit) countSelects++;


            Units.AddRange(units);
            isUpdate = false;


            for (int i = 0; i < Queue.Count; i++)
                if (Queue[i] == selectUnit && --countSelects == 0)
                {
                    Iterator = i;
                    break;
                }
        }

        public void Remove(params Unit[] units)
        {
            foreach (Unit unit in units)
            {
                Units.Remove(unit);

                while (true)
                {
                    int index = _queue.FindIndex(u => u == unit);
                    if (index == -1) break;
                    if (index <= Iterator) Iterator--;
                    _queue.RemoveAt(index);
                }
            }
        }

        public void Clear()
        {
            Units.Clear();
            _queue.Clear();
        }
    }
}
