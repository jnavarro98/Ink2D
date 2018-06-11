using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    class Beam : Polygon
    {
        public const byte SPEED = 3;
        public const byte RANGE = 40;
        public const byte WIDTH = 10;

        public short Range { get; set; }
        public short Width { get; set; }
        public short Target { get; set; }
        public short Force { get; set; }
        public Stack<Projectile> Ammo { get; set; }
        internal Lazer Lazer { get; set; }

        public Beam(Inker p)
        {
            Coordinates = new short[4, 2];
            SetRange(RANGE);
            SetWidth(WIDTH);
            Place(p);
            SetTarget(p.X,p);
            Ammo = new Stack<Projectile>();
            Lazer = new Lazer(this);
        }

        public void Wiggle(Inker inker)
        {

            if (Coordinates[0, 0] > 0)
            {
                if (!(Coordinates[0, 0] > inker.X - inker.Width / 2))
                    SetTarget(Target += inker.Speed, inker);
            }
            else
                SetTarget(inker.Beam.Target += inker.Speed, inker);

            if (Coordinates[1, 0] < GameController.SCREEN_WIDTH)
            {
                if (!(Coordinates[1, 0] < inker.X + inker.Width * 1.5))
                    SetTarget(Target -= inker.Speed, inker);
            }
            else
                SetTarget(Target -= inker.Speed, inker);

            Lazer.Update(this);
            SetOrigin(inker);

        }

        public void Reload(short quantity)
        {
            for(int i = 0; i < quantity; i++)
            {
                Ammo.Push(new Projectile());
            }
        }
        public Projectile Shoot()
        {
            if (Ammo.Count > 0)
                return Ammo.Pop();
            else
                return null;
        }

        public void Place(Inker inker)
        {
            SetOrigin(inker);

            Coordinates[0, 0] = (short)(inker.X - Width + inker.Width / 2);
            Coordinates[1, 0] = (short)(inker.X + Width + inker.Width / 2);

            if (inker.PlayerNumber == 1)
            {
                Coordinates[2, 1] = (short)(inker.Y + 12);
                Coordinates[3, 1] = Coordinates[2, 1];
                Coordinates[0, 1] = (short)(inker.Y - Range);
                Coordinates[1, 1] = Coordinates[0, 1];
            }
            else
            {
                Coordinates[2, 1] = (short)(inker.Y + inker.Height - 6);
                Coordinates[3, 1] = Coordinates[2, 1];
                Coordinates[0, 1] = (short)(Coordinates[2, 1] + Range);
                Coordinates[1, 1] = Coordinates[0, 1];
            }

            Target = (short)(Coordinates[0, 0] + Width);
        }

        public void SetOrigin(Inker inker)
        {
            Coordinates[2, 0] = (short)(inker.X + 35);
            Coordinates[3, 0] = (short)(inker.X + 41);
        }

        public void SetRange(short range)
        {
            Range = range;
        }

        public void SetWidth(short width)
        {
            Width = width;
        }

        public void SetTarget(short x, Inker inker)
        {
            Coordinates[0, 0] = (short)(x - Width + inker.Width / 2);
            Coordinates[1, 0] = (short)(x + Width + inker.Width / 2);
        }

        public void ChargePowerUp(Lazer effect)
        {
            Lazer = effect;
        }
    }
}
