using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using SWGame.Instruments;

namespace SWGame.GameMap
{
    public partial class GMap : UserControl
    {
        #region Fields

        public static readonly int CellSize = 40;
        public static readonly int CellMargin = 2;


        IMapObject[,] Objects;

        public int Columns { get; private set; }
        public int Rows { get; private set; }

        #endregion


        #region Events

        public event EventHandler ObjectClick;

        #endregion


        #region Methods

        public static int GetMapPosition(int coordinate)
        {
            return CellMargin * coordinate + CellSize * coordinate + CellMargin;
        }


        public bool CheckCoordinate(IMapObject obj, int x, int y)
        {
            if (y < 0 || y >= Columns || x < 0 || x >= Rows) return false;
            if (obj.X == x && obj.Y == y) return false;

            for (int locY = y; locY < y + obj.YSize; locY++)
                for (int locX = x; locX < x + obj.XSize; locX++)
                    if (locY >= Columns || locX >= Rows || (Objects[locY, locX] != null && Objects[locY, locX] != obj))
                        return false;

            return true;
        }

        public IMapObject GetObject(int x, int y) => Objects[y, x];

        public bool ContainsObject(IMapObject obj) => UIMap.Children.Contains(obj as UIElement);

        void AddToObjects(IMapObject obj)
        {
            for (int y = obj.Y; y < obj.Y + obj.YSize; y++)
                for (int x = obj.X; x < obj.X + obj.XSize; x++)
                    Objects[y, x] = obj;
        }

        public void AddObject(IMapObject obj)
        {
            if (ContainsObject(obj)) throw new Exception("Object is already on the map");

            if (obj.Y < 0 || obj.Y >= Columns || obj.X < 0 || obj.X >= Rows)
                throw new IndexOutOfRangeException("Object coordinate out of map range");


            obj.Click += (sender, e) => ObjectClick?.Invoke(sender, e);

            if (obj.Fulled) AddToObjects(obj);

            UIElement uiObj = obj as UIElement;
            Canvas.SetLeft(uiObj, obj.XMap);
            Canvas.SetTop(uiObj, obj.YMap);
            UIMap.Children.Add(uiObj);
        }

        void RemoveFromObjects(IMapObject obj)
        {
            for (int y = obj.Y; y < obj.Y + obj.YSize; y++)
                for (int x = obj.X; x < obj.X + obj.XSize; x++)
                    Objects[y, x] = null;
        }

        public void RemoveObject(IMapObject obj)
        {
            if (!ContainsObject(obj)) throw new Exception("Object is not found");

            if (obj.Fulled) RemoveFromObjects(obj);

            UIMap.Children.Remove(obj as UIElement);
        }

        public void MoveObject(IMapObject obj, int newX, int newY, double speed, EventHandler funcAfter = null)
        {
            if (!ContainsObject(obj)) throw new Exception("Object is not found");

            if (obj.Fulled && Objects[newY, newX] != null && Objects[newY, newX] != obj)
                throw new Exception("There is already an object at these coordinates");


            if (obj.Fulled) RemoveFromObjects(obj);

            obj.X = newX;
            obj.Y = newY;

            if (obj.Fulled) AddToObjects(obj);

            var animation = new MoveAnimation(obj as UIElement, GetMapPosition(newX), GetMapPosition(newY), speed);
            if (funcAfter != null) animation.EndAnimation += funcAfter;
            animation.Start();
        }

        public IMapObject[] GetObjects()
        {
            IMapObject[] objects = new IMapObject[UIMap.Children.Count];

            for (int i = 0; i < objects.Length; i++)
                objects[i] = (IMapObject)UIMap.Children[i];

            return objects;
        }

        public void Clear()
        {
            UIMap.Children.Clear();
            Matrix.Clear(Objects);
        }


        public void InitializeMap(int rows, int columns)
        {
            UIMap.Children.Clear();
            Objects = new IMapObject[columns, rows];

            Columns = columns;
            Rows = rows;

            Width = GetMapPosition(rows);
            Height = GetMapPosition(columns);
        }


        bool isPressed = false;
        void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => isPressed = true;

        void Back_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isPressed) ObjectClick?.Invoke(null, EventArgs.Empty);
        }

        void Back_MouseLeave(object sender, MouseEventArgs e) => isPressed = false;

        #endregion


        public GMap()
        {
            InitializeComponent();
        }
    }
}
