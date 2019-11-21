using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRogueLike.Engine
{
    struct Vector2i
    {
        public static Vector2i One => new Vector2i(1);
        public static Vector2i Zero => new Vector2i(0);
        public static Vector2i UnitX => new Vector2i(1, 0);
        public static Vector2i UnitY => new Vector2i(0,1);

        private Vector2 vector;
        public int X => (int)vector.X;
        public int Y => (int)vector.Y;

        public Vector2i(int x = 0)
        {
            vector = new Vector2(x);
        }
        public Vector2i(int x, int y)
        {
            vector = new Vector2(x, y);
        }
        public Vector2i(Vector2 v)
        {
            vector = new Vector2(v.X, v.Y);
        }

        public override string ToString()
        {
            return "{ " + (int)vector.X + ", " + (int)vector.Y + " }";
        }
        public override int GetHashCode()
        {
            var hashCode = -315896587;
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(vector);
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Vector2i))
            {
                return false;
            }

            var i = (Vector2i)obj;
            return /*vector.Equals(i.vector) &&*/
                   X == i.X &&
                   Y == i.Y;
        }

        public static Vector2i operator +(Vector2i value1, Vector2i value2)         => new Vector2i(value1.X + value2.X, value1.Y + value2.Y);
        public static Vector2i operator -(Vector2i value)                           => new Vector2i(-value.X, -value.Y);
        public static Vector2i operator -(Vector2i value1, Vector2i value2)         => new Vector2i(value1.X - value2.X, value1.Y - value2.Y);
        public static Vector2i operator *(Vector2i value1, Vector2i value2)         => new Vector2i(value1.X * value2.X, value1.Y * value2.Y);
        public static Vector2i operator *(int scaleFactor, Vector2i value)          => new Vector2i(value.X * scaleFactor, value.Y * scaleFactor);
        public static Vector2i operator *(Vector2i value, int scaleFactor)          => new Vector2i(value.X * scaleFactor, value.Y * scaleFactor);
        public static Vector2i operator /(Vector2i value1, Vector2i value2)         => new Vector2i(value1.X / value2.X, value1.Y / value2.Y);
        public static Vector2i operator /(Vector2i value1, int divider)             => new Vector2i(value1.X / divider, value1.Y / divider);
        public static bool operator ==(Vector2i value1, Vector2i value2)            => (value1.X == value2.X) && (value1.Y == value2.Y);
        public static bool operator !=(Vector2i value1, Vector2i value2)            => (value1.X != value2.X) || (value1.Y != value2.Y);
    }
}
