namespace DontMelt
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
            if (obj.GetType() == typeof(Point))
            {
                return this == (Point)obj;
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

        public static bool operator ==(Point A, Point B)
        {
            return (A.x == B.x) && (A.y == B.y);
        }
        public static bool operator !=(Point A, Point B)
        {
            return !(A == B);
        }

        public static Point operator +(Point A, Point B)
        {
            return Create(A.x + B.x, A.y + B.y);
        }
        public static Point operator -(Point A, Point B)
        {
            return Create(A.x - B.x, A.y - B.y);
        }

        public static Point operator *(Point A, Point B)
        {
            return Create(A.x * B.x, A.y * B.y);
        }
        public static Point operator /(Point A, Point B)
        {
            return Create(A.x / B.x, A.y / B.y);
        }

        public static Point operator +(Point A)
        {
            return A;
        }
        public static Point operator -(Point A)
        {
            return Create(A.x * -1, A.y * -1);
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