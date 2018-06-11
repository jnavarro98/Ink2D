using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink2D
{
    class Projectile: Triangle
    {

        private short length;
        private short direction;
        public double angle;
        public double tempAngle;
        public short colider;

        public byte Speed { get; set; } = 5;
        public short Movement { get; set; }
        public bool hasBounced { get; set; }

        public Projectile()
        {
            colider = 1;
            length = 25;
        }
        public bool IsOutOfBounds()
        {
            return (Coordinates[2, 1] < GameScreen.UPPER_LIMIT || Coordinates[2, 1] > GameScreen.BOTTOM_LIMIT);
        }

        public void SetCoordinates(Beam beam, GameScreen.GameState gameState)
        {
            angle = Utilities.CalculateAngle(beam);
            tempAngle = angle;
            // Halla la cordenada donde dibujar la punta del triangulo basandose en el angulo del beam
            if (gameState == GameScreen.GameState.player1Turn)
            {
                Coordinates[2, 1] = (short)((length * Math.Sin(angle)) + (beam.Coordinates[0, 1]));
                direction = -1;
            }
            else
            {
                Coordinates[2, 1] = (short)((length * Math.Sin(angle)) + (beam.Coordinates[0, 1]));
                angle = angle * -1;
                direction = 1;
            } 

            Coordinates[0, 0] = beam.Coordinates[0, 0];
            Coordinates[0, 1] = beam.Coordinates[0, 1];
            Coordinates[1, 0] = beam.Coordinates[1, 0];
            Coordinates[1, 1] = beam.Coordinates[1, 1];

            
            if (angle >= 0)
                Coordinates[2, 0] = (short) (beam.Coordinates[1, 0] + (length * Math.Cos(angle)) + (beam.Coordinates[1, 0] - beam.Coordinates[0, 0]) - Speed + 1);
            else
                Coordinates[2, 0] = (short)(beam.Coordinates[0, 0] + (length * Math.Cos(angle)) + (beam.Coordinates[1, 0] - beam.Coordinates[0, 0]) - Speed +  1);

        }
        public void Bounce(GameScreen game)
        {
            hasBounced = Coordinates[2, 0] <= GameScreen.LEFT_LIMIT || Coordinates[2, 0] > GameScreen.RIGHT_LIMIT ||
                (Coordinates[2, 0] >= game.Obstacle.Coordinates[0, 0] && Coordinates[2, 0] <= game.Obstacle.Coordinates[1, 0] &&
                Coordinates[2, 1] >= game.Obstacle.Coordinates[0, 1] && Coordinates[2, 1] <= game.Obstacle.Coordinates[2, 1]);
            if (Coordinates[2, 0] <= GameScreen.LEFT_LIMIT || Coordinates[2, 0] > GameScreen.RIGHT_LIMIT || 
                (Coordinates[2, 0] >= game.Obstacle.Coordinates[0, 0] && Coordinates[2, 0] <= game.Obstacle.Coordinates[1, 0] &&
                Coordinates[2, 1] >= game.Obstacle.Coordinates[0,1] && Coordinates[2, 1] <= game.Obstacle.Coordinates[2, 1]))
            {
                colider = (short)(colider * - 1);
                Speed += 1;
                tempAngle = Math.PI - tempAngle;
                // Si rebota dibujaremos con nuestra formula el angulo suplementario
                if (angle >= 0)
                {
                    Coordinates[2, 0] = (short)(Coordinates[1, 0] + (length * Math.Cos(tempAngle)) + (Coordinates[1, 0] - Coordinates[0, 0]));
                }
                    
                else
                {
                    Coordinates[2, 0] = (short)(Coordinates[0,0] + (length * Math.Cos(tempAngle)) + (Coordinates[1, 0] - Coordinates[0, 0]));
                }
                    
            }
        }
        public bool Crashes(Sprite target)
        {   
            if(target != null)
            {
                return ((Coordinates[2, 0] > target.X && Coordinates[2, 0] < target.X + target.Width &&
                Coordinates[2, 1] > target.Y && Coordinates[2, 1] < target.Y + target.Height));
            }
            return false;
        }
        public void Move()
        {
            for (int i = 0; i < 3; i++)
            {
                Movement = (short)(((Speed) / Math.Tan(angle) * -1) * colider);
                Coordinates[i, 0] += Movement;
                Coordinates[i, 1] += (short)(direction * Speed);
            }

        }
    }
}