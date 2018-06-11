using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    class Polygon: Shape
    {
        public Polygon()
        {
            Coordinates = new short[4, 2];
        }
    }
}
