using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Ink2D
{
    class Utilities
    {
        public static double CalculateAngle(Beam beam)
        {
            return Math.Atan2(beam.Coordinates[0, 1] - beam.Coordinates[2, 1],
                beam.Coordinates[0, 0] - beam.Coordinates[2, 0]);
        }
        public static Projectile NearestProjectile(List<Projectile> projectiles, Inker inker)
        {
            SortedList<double,int> projectilesByDistance = new SortedList<double,int>();
            double distance;
            for(int i = 0; i < projectiles.Count; i++)
            {
                distance = (Math.Pow(projectiles[i].Coordinates[2, 0] - (inker.X + inker.Width / 2), 2) +
                    Math.Pow(projectiles[i].Coordinates[2, 1] - inker.Y, 2));
                if(!projectilesByDistance.ContainsKey(distance))
                    projectilesByDistance.Add(distance, i);
            }
            KeyValuePair<double, int> minElement = projectilesByDistance.ElementAt(0);
            return projectiles[minElement.Value];
        }
        public static TObject DeepClone<TObject>(TObject obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (TObject)formatter.Deserialize(ms);
            }
        }
    }
}
