using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGame.GameMap
{
    public interface IMapObject
    {
        int X { get; set; }
        int Y { get; set; }

        int XMap { get; }
        int YMap { get; }

        int XSize { get; }
        int YSize { get; }

        bool Fulled { get; }

        event EventHandler Click;
    }
}
