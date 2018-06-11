using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    [Serializable]
    class IA : Inker
    {

        short direction;
        Random rnd;
        bool IsShooting { get; set; }
        public IA(byte playerNumber, Random rnd):base(playerNumber)
        {
            direction = 1;
            this.rnd = rnd;
            ShotInterval = 200;
            IsShooting = true;
        }
        public override void Move(GameScreen game)
        {
            if (game.CurrentPlayer != this)
                Defend(game);
            else
                Attack(game);

        }
        public void Defend(GameScreen game)
        {
            if(game.CurrentPlayer.Beam != null)
            {
                if (game.CurrentPlayer.Beam.Lazer.Damage)
                {
                    if ((X < game.Enemy.Beam.Lazer.Coordinates[0, 0] &&
                       X + width < game.Enemy.Beam.Lazer.Coordinates[0, 0]) &&
                       X >= GameScreen.LEFT_LIMIT)
                        X -= (short)(speed * 0.9f); 
                    else if(X + width <= GameScreen.RIGHT_LIMIT)
                        X += (short)(speed * 0.9f);
                }
                else
                {
                    if (game.DrawnProjectiles.Count > 0)
                    {
                        Projectile nearestProjectile = Utilities.NearestProjectile(game.DrawnProjectiles, this);
                        if (nearestProjectile.Movement == 0 &&
                            X <= GameScreen.RIGHT_LIMIT - width)
                            X += speed;
                        else
                        {
                            if (nearestProjectile.Movement > 0 &&
                            X + width / 2 <= GameController.SCREEN_WIDTH / 2 &&
                            X >= GameScreen.LEFT_LIMIT)
                                X -= speed;
                            else if (X + width / 2 <= GameController.SCREEN_WIDTH / 2)
                                X += speed;
                            if (nearestProjectile.Movement < 0 &&
                                X + width / 2 > GameController.SCREEN_WIDTH / 2 &&
                                X <= GameScreen.RIGHT_LIMIT - width)
                                X += speed;
                            else if (X + width / 2 > GameController.SCREEN_WIDTH / 2)
                                X -= speed;
                        }

                    }else
                    {

                        if (X + width / 2 > GameController.SCREEN_WIDTH / 2)
                            X -= speed;
                        else
                            X += speed;
                              
                    }
                }
            }
            
            
        }
            

        public void Attack(GameScreen game)
        {
            
            if (X >= GameScreen.RIGHT_LIMIT - width || X <= GameScreen.LEFT_LIMIT)
            {
                direction *= -1;
            }
                
            X += (short)(speed * direction);

        }

        public override void Shoot(GameScreen game)
        {
            if (rnd.Next(0, 30) == 0)
                IsShooting = !IsShooting;
            game.CheckShootAvaliable(IsShooting, ref game.timeStampFromLastShot);
        }
    }
}
