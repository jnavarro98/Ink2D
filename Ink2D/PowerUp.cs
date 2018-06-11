using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    class PowerUp: Sprite
    {
        Audio powerUpSound;
        public PowerUp(Random rnd)
        {
            powerUpSound = new Audio(44100, 2, 4096);
            powerUpSound.AddWAV("sounds/PowerUp.wav");
            powerUpSound.PlayWAV(0, 1, 0); 
            Width = 30;
            Height = 30;
            Sprite = new Image("images/PowerUp.png",width,height);
            X = (short)rnd.Next(GameScreen.LEFT_LIMIT,GameScreen.RIGHT_LIMIT - width);
            Y = (short)rnd.Next(GameScreen.UPPER_LIMIT + 170, GameScreen.BOTTOM_LIMIT - 170);
            Speed = 7;
            Direction = 1;
        }
        public void Move(GameScreen game)
        {
            if (X >= GameScreen.RIGHT_LIMIT - 30 || X <= GameScreen.LEFT_LIMIT - speed ||
                (X + Width > game.Obstacle.Coordinates[0, 0] && X < game.Obstacle.Coordinates[1, 0] &&
                Y + Height > game.Obstacle.Coordinates[0, 1] && Y < game.Obstacle.Coordinates[2, 1]))
                    Direction *= -1;
            X += (short)(Speed * Direction);
        }
        public short Direction { get; set; }
        internal Image Sprite { get => sprite; set => sprite = value; }
    }
}
