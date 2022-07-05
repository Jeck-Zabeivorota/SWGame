using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGame.Instruments
{
    public static class Matrix
    {
        public static bool Contains<T>(T[,] matrix, T element)
        {
            foreach (T item in matrix)
                if (element.Equals(item))
                    return true;
            
            return false;
        }

        public static void Clear<T>(T[,] matrix)
        {
            for (int y = 0; y < matrix.GetLength(0); y++)
                for (int x = 0; x < matrix.GetLength(1); x++)
                    matrix[y, x] = default;
        }

        public static T Find<T>(T[,] matrix, Func<T, bool> predicate)
        {
            foreach (T item in matrix)
                if (predicate(item))
                    return item;

            return default;
        }

        public static List<T> Where<T>(T[,] matrix, Func<T, bool> predicate)
        {
            List<T> list = new List<T>();

            foreach (T item in matrix)
                if (predicate(item))
                    list.Add(item);

            return list;
        }

        public static List<TResult> Select<T, TResult>(T[,] matrix, Func<T, TResult> selector)
        {
            List<TResult> list = new List<TResult>();

            foreach (T item in matrix)
                list.Add(selector(item));

            return list;
        }

        public static IEnumerable<T> ToIEnumerable<T>(T[,] matrix)
        {
            List<T> list = new List<T>();

            foreach (T item in matrix) list.Add(item);

            return list;
        }


        public static int Columns<T>(T[,] matrix) => matrix.GetLength(0);

        public static int Rows<T>(T[,] matrix) => matrix.GetLength(1);


        public static T[] GetRow<T>(T[,] matrix, int row)
        {
            T[] returnRow = new T[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(1); i++)
                returnRow[i] = matrix[i, row];

            return returnRow;
        }

        public static T[] GetColumn<T>(T[,] matrix, int column)
        {
            T[] returnColumn = new T[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
                returnColumn[i] = matrix[column, i];

            return returnColumn;
        }
    }
}
