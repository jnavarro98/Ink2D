using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    class Obstacle: Polygon
    {
        public short Width { get; set; }
        public short Heigth { get; set; }
        Random rnd;
        public Obstacle(short width, short heigth, Random rnd)
        {
            this.rnd = rnd;
            Width = width;
            Heigth = heigth;

            Reposition();

        }
        public void Reposition()
        {
            short x = (short)rnd.Next(GameScreen.LEFT_LIMIT + 50, GameScreen.RIGHT_LIMIT - 50);
            short y = (short)rnd.Next(GameScreen.UPPER_LIMIT + 50, GameScreen.BOTTOM_LIMIT - 50 - Heigth);

            Coordinates[0, 0] = x;

            Coordinates[1, 0] = (short)(x + Width);

            Coordinates[2, 0] = Coordinates[0, 0];

            Coordinates[3, 0] = Coordinates[1, 0];

            Coordinates[0, 1] = y;

            Coordinates[1, 1] = Coordinates[0, 1];

            Coordinates[2, 1] = (short)(Coordinates[0, 1] + Heigth);

            Coordinates[3, 1] = Coordinates[2, 1];
        }
    }
}
