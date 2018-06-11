using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    class Player: Inker
    {
        public Player(byte playerNumber): base(playerNumber)
        {
            ShotInterval = 200;
        }

        public override void Move(GameScreen gameScreen)
        {
            bool left;
            bool right;
            if (PlayerNumber == 1)
            {
                left = gameScreen.Hardware.IsKeyPressed(Hardware.KEY_Z);
                right = gameScreen.Hardware.IsKeyPressed(Hardware.KEY_C);
            }
            else
            {
                left = gameScreen.Hardware.IsKeyPressed(Hardware.KEY_LEFT);
                right = gameScreen.Hardware.IsKeyPressed(Hardware.KEY_RIGHT);
            }

            if (left && X > GameScreen.LEFT_LIMIT)
            {
                X -= speed;
            }
            if (right && X < GameScreen.RIGHT_LIMIT - gameScreen.CurrentPlayer.Width)
            {
                X += speed;
            }

        }

        public override void Shoot(GameScreen game)
        {
            if (game.CurrentGameState == GameScreen.GameState.player1Turn)
            {
                bool shootKey = game.Hardware.IsKeyPressed(Hardware.KEY_SPACE);
                game.CheckShootAvaliable(shootKey, ref game.timeStampFromLastShot);
            }
            else
            {
                bool shootKey = game.Hardware.IsKeyPressed(Hardware.KEY_ENTER);
                game.CheckShootAvaliable(shootKey, ref game.timeStampFromLastShot);
            }
        }
    }
}
