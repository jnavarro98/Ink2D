using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    abstract class Sprite
    {
        protected short width;
        protected short height;
        protected Image sprite;
        protected byte speed;

        public short X { get => sprite.X; set => sprite.X = value; }
        public short Y { get => sprite.Y; set => sprite.Y = value; }

        public short Width { get => width; set => width = value; }
        public short Height { get => height; set => height = value; }
        public byte Speed { get => speed; set => speed = value; }
    }
}
