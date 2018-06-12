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
        public PowerUp(GameScreen game)
        {
            powerUpSound = new Audio(44100, 2, 4096);
            powerUpSound.AddWAV("sounds/PowerUp.wav");
            powerUpSound.PlayWAV(0, 1, 0); 
            Width = 30;
            Height = 30;
            Sprite = new Image("images/PowerUp.png",width,height);
            do
            {
                X = (short)game.Rnd.Next(GameScreen.LEFT_LIMIT, GameScreen.RIGHT_LIMIT - width);
                Y = (short)game.Rnd.Next(GameScreen.UPPER_LIMIT + 170, GameScreen.BOTTOM_LIMIT - 170);
            } while (ColidesWithElements(game));
            
            Speed = 7;
            Direction = 1;
        }
        public void Move(GameScreen game)
        {
            if (X >= GameScreen.RIGHT_LIMIT - 30 || X <= GameScreen.LEFT_LIMIT - speed ||
                ColidesWithElements(game))
                    Direction *= -1;
            X += (short)(Speed * Direction);
        }
        public bool ColidesWithElements(GameScreen game)
        {
            return (X + Width > game.Obstacle.Coordinates[0, 0] && X < game.Obstacle.Coordinates[1, 0] &&
                Y + Height > game.Obstacle.Coordinates[0, 1] && Y < game.Obstacle.Coordinates[2, 1]);
        }
        public short Direction { get; set; }
        internal Image Sprite { get => sprite; set => sprite = value; }
    }
}
