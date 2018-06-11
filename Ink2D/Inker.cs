using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    abstract class Inker: Sprite
    {

        protected Beam beam;

        public Beam Beam { get => beam; set => beam = value; }
        public byte PlayerNumber { get; set; }
        public short Lives { get; set; }
        public short ShotInterval { get; set; }
        public short AmmoUsed { get; set; }

        public Inker()
        {
            AmmoUsed = 0;
            width = 76;
            height = 59;
            speed = 5;
        }
        public abstract void Move(GameScreen gameScreen);
        public abstract void Shoot(GameScreen gameScreen);

        public void SetBeam()
        {
            beam = new Beam(this);
        }
        public Inker(byte playerNumber): this()
        {
            PlayerNumber = playerNumber;
            sprite = new Image("images/Sprite" + playerNumber + ".png", width, height);
        }
        public short GetX()
        {
            return X;
        }
        public short GetY()
        {
            return Y;
        }
        public Image GetSprite()
        {
            return sprite;
        }
        public void Hit()
        {
            if(Lives > 0)
                Lives--;
        }
        public void RefillLives()
        {
            Lives = 3;
        }
        public void Spawn()
        {
            switch (PlayerNumber)
            {
                case 1:
                    SetPosition((short)(400 - width / 2), (short)(550 - height));
                    break;
                case 2:
                    SetPosition((short)(400 - width/2), 50);
                    break;
                default:
                    throw new Exception("Si ves este mensaje es que eres dios");
            }
        }
        public void UpdatePosition()
        {
            sprite.MoveTo(X, Y);
        }
        public void SetPosition(short x, short y)
        {
            X = x;
            Y = y;
        }

    }
}
