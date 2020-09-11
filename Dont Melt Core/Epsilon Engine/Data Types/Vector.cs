namespace EpsilonEngine
{
    public sealed class Vector
    {
        public double x = 0;
        public double y = 0;
        private Vector() { }
        public static Vector Create()
        {
            Vector Output = new Vector();
            Output.x = 0;
            Output.y = 0;
            return Output;
        }
        public static Vector Create(double x, double y)
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
            if (obj is null || obj.GetType() != typeof(Vector))
            {
                return false;
            }
            else
            {
                return this == (Vector)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return Create(a.x + b.x, a.y + b.y);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return Create(a.x - b.x, a.y - b.y);
        }
        public static Vector operator *(Vector a, Vector b)
        {
            return Create(a.x * b.x, a.y * b.y);
        }
        public static Vector operator /(Vector a, Vector b)
        {
            return Create(a.x / b.x, a.y / b.y);
        }

        public static Vector operator +(Vector a, double b)
        {
            return Create(a.x + b, a.y + b);
        }
        public static Vector operator -(Vector a, double b)
        {
            return Create(a.x - b, a.y - b);
        }
        public static Vector operator *(Vector a, double b)
        {
            return Create(a.x * b, a.y * b);
        }
        public static Vector operator /(Vector a, double b)
        {
            return Create(a.x / b, a.y / b);
        }

        public static Vector operator +(Vector a)
        {
            return a;
        }
        public static Vector operator -(Vector a)
        {
            return Create(a.x * -1, a.y * -1);
        }

        public Vector Clone()
        {
            Vector Output = new Vector();
            Output.x = x;
            Output.y = y;
            return Output;
        }
    }
}