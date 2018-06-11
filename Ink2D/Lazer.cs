using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    class Lazer : Polygon
    {
        public bool Damage { get; set; }
        public short CD { get; set; }
        public short Amplitude { get; set; }
        public Lazer(Beam beam)
        {
            Amplitude = 10;
            Damage = false;
            CD = 666;
            Coordinates[0, 0] = (short)(beam.Coordinates[0, 0] - Amplitude);
            Coordinates[1, 0] = (short)(beam.Coordinates[1, 0] + Amplitude);
            Coordinates[2, 0] = beam.Coordinates[0, 0];
            Coordinates[3, 0] = beam.Coordinates[1, 0];

            Coordinates[0, 1] = beam.Coordinates[0, 1];
            Coordinates[1, 1] = beam.Coordinates[0, 1];


            Coordinates[2, 1] = beam.Coordinates[0, 1];
            Coordinates[3, 1] = beam.Coordinates[0, 1];
        }
        public void Update(Beam beam)
        {
            Coordinates[0, 0] = (short)(beam.Coordinates[0, 0] - Amplitude);
            Coordinates[1, 0] = (short)(beam.Coordinates[1, 0] + Amplitude);
            Coordinates[2, 0] = beam.Coordinates[0, 0];
            Coordinates[3, 0] = beam.Coordinates[1, 0];
        }
        public void Deactivate(Beam beam)
        {
            Damage = false;
            Coordinates[0, 1] = beam.Coordinates[0, 1];
            Coordinates[1, 1] = beam.Coordinates[0, 1];
        }
        public bool Crashes(Inker target)
        {
            return (!((Coordinates[0, 0] < target.X && Coordinates[1, 0] < target.X) ||
                (Coordinates[0, 0] > target.X + target.Width && Coordinates[1, 0] > target.X + target.Width))
                && Damage);
        }
        public void Activate(Beam beam)
        {
            Damage = true;
            if (beam.Coordinates[0, 1] > 300)
            {
                Coordinates[0, 1] = GameScreen.UPPER_LIMIT;
                Coordinates[1, 1] = GameScreen.UPPER_LIMIT;
            }
            else
            {
                Coordinates[0, 1] = GameScreen.BOTTOM_LIMIT;
                Coordinates[1, 1] = GameScreen.BOTTOM_LIMIT;
            }
        }
    }
}
