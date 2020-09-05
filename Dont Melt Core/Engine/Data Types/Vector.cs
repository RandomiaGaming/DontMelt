using DontMelt.Helpers;

namespace DontMelt.Data
{
    public sealed class Vector
    {
        public int x = 0;
        public int y = 0;

        private Vector() { }
        public static Vector Create()
        {
            Vector Output = new Vector();
            Output.x = 0;
            Output.y = 0;
            return Output;
        }
        public static Vector Create(int x, int y)
        {
            Vector Output = new Vector();
            Output.x = x;
            Output.y = y;
            return Output;
        }
        public override string ToString()
        {
            return $"({x},{y})";
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Vector))
            {
                return this == (Vector)obj;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Vector A, Vector B)
        {
            return (A.x == B.x) && (A.y == B.y);
        }
        public static bool operator !=(Vector A, Vector B)
        {
            return !(A == B);
        }

        public static Vector operator +(Vector A, Vector B)
        {
            return Create(A.x + B.x, A.y + B.y);
        }
        public static Vector operator -(Vector A, Vector B)
        {
            return Create(A.x - B.x, A.y - B.y);
        }

        public static Vector operator *(Vector A, Vector B)
        {
            return Create(A.x * B.x, A.y * B.y);
        }
        public static Vector operator /(Vector A, Vector B)
        {
            return Create(A.x / B.x, A.y / B.y);
        }

        public static Vector operator +(Vector A)
        {
            return A;
        }
        public static Vector operator -(Vector A)
        {
            return Create(A.x * -1, A.y * -1);
        }

        public double Magnitude()
        {
            return MathHelper.Sqrt((x * x) + (y * y));
        }
        public static double Magnitude(Vector A)
        {
            return MathHelper.Sqrt((A.x * A.x) + (A.y * A.y));
        }

        public Vector Clone()
        {
            return Create(x, y);
        }
    }
}