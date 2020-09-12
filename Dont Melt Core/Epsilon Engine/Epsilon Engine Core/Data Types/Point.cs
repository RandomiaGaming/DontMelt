namespace EpsilonEngine
{
    public sealed class Point
    {
        public int x = 0;
        public int y = 0;
        private Point() { }
        public static Point Create()
        {
            Point Output = new Point();
            Output.x = 0;
            Output.y = 0;
            return Output;
        }
        public static Point Create(int x, int y)
        {
            Point Output = new Point();
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
            if (obj is null || obj.GetType() != typeof(Point))
            {
                return false;
            }
            else
            {
                return this == (Point)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Point a, Point b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public static Point operator +(Point a, Point b)
        {
            return Create(a.x + b.x, a.y + b.y);
        }
        public static Point operator -(Point a, Point b)
        {
            return Create(a.x - b.x, a.y - b.y);
        }
        public static Point operator *(Point a, Point b)
        {
            return Create(a.x * b.x, a.y * b.y);
        }
        public static Point operator /(Point a, Point b)
        {
            return Create(a.x / b.x, a.y / b.y);
        }

        public static Point operator +(Point a, int b)
        {
            return Create(a.x + b, a.y + b);
        }
        public static Point operator -(Point a, int b)
        {
            return Create(a.x - b, a.y - b);
        }
        public static Point operator *(Point a, int b)
        {
            return Create(a.x * b, a.y * b);
        }
        public static Point operator /(Point a, int b)
        {
            return Create(a.x / b, a.y / b);
        }

        public static Point operator +(Point a)
        {
            return a;
        }
        public static Point operator -(Point a)
        {
            return Create(a.x * -1, a.y * -1);
        }

        public Point Clone()
        {
            Point Output = new Point();
            Output.x = x;
            Output.y = y;
            return Output;
        }
    }
}