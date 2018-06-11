using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    class Triangle: Shape
    {
        public Triangle()
        {
            Coordinates = new short[3, 2];
        }
        public void SetCoordinatesAsSprite(short x, short y, short size)
        {
            Coordinates[0, 0] = (short)(x + 1);
            Coordinates[0, 1] = (short)(y + size);
            Coordinates[1, 0] = (short)(x + size - 1);
            Coordinates[1, 1] = Coordinates[0, 1];
            Coordinates[2, 0] = (short) (x + size / 2);
            Coordinates[2, 1] = y;
        }
    }
}
