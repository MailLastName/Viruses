using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusMaker
{
    public class Vector2Int
    {
        public int X;
        public int Y;
        public static Vector2Int Zero = new();

        public Vector2Int()
        {
        }
        public Vector2Int(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
