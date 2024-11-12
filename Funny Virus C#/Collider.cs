using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Viruses.FunnyVirus
{
    public class Collider
    {
        public Vector2 Position;
        public Vector2 Scale;

        public bool Check(Vector2 Position)
        {
            float X = Scale.X / 2;
            float Y = Scale.Y / 2;
            if (Position.X > this.Position.X - X && Position.X < this.Position.X + X && Position.Y > this.Position.Y - Y && Position.Y < this.Position.Y + Y)
            {
                return true;
            }
            return false;
        }
        public bool Check(Collider Collider)
        {
            float X1 = Scale.X / 2;
            float Y1 = Scale.Y / 2;
            float X2 = Collider.Scale.X / 2;
            float Y2 = Collider.Scale.Y / 2;
            if (Collider.Position.X + X2 > Position.X - X1 && Collider.Position.X - X2 < Position.X + X1 && Collider.Position.Y + Y2 > Position.Y - Y1 && Collider.Position.Y - Y2 < Position.Y + Y1)
            {
                return true;
            }
            return false;
        }
    }
}
