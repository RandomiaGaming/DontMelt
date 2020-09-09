namespace DontMelt
{
    public sealed class Rectangle
    {
        private Point Min = Point.Create(0, 0);
        private Point Max = Point.Create(1, 1);
        private Rectangle() { }
        public static Rectangle Create()
        {
            Rectangle Output = new Rectangle();
            Output.Min = Point.Create(0, 0);
            Output.Max = Point.Create(0, 0);
            return Output;
        }
        public static Rectangle Create(Point Size)
        {
            Rectangle Output = new Rectangle();
            Output.Min = Point.Create(0, 0);
            Output.Max = Point.Create(MathHelper.Abs(Size.x), MathHelper.Abs(Size.y));
            return Output;
        }
        public static Rectangle Create(Point Min, Point Max)
        {
            Rectangle Output = new Rectangle();
            Output.Min = Point.Create(MathHelper.Min(Min.x, Max.x), MathHelper.Min(Min.y, Max.y));
            Output.Max = Point.Create(MathHelper.Max(Min.x, Max.x), MathHelper.Max(Min.y, Max.y));
            return Output;
        }
        public override string ToString()
        {
            return $"[{Min},{Max}]";
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Rectangle))
            {
                return this == (Rectangle)obj;
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
        public static bool operator ==(Rectangle A, Rectangle B)
        {
            if (ReferenceEquals(A, null) && ReferenceEquals(B, null))
            {
                return true;
            }
            else if (ReferenceEquals(A, null) || ReferenceEquals(B, null))
            {
                return false;
            }
            return (A.Get_Min() == B.Get_Min() && A.Get_Max() == B.Get_Max());
        }
        public static bool operator !=(Rectangle A, Rectangle B)
        {
            return !(A == B);
        }

        public Point Get_Min()
        {
            return Min.Clone();
        }
        public Point Get_Max()
        {
            return Max.Clone();
        }
        public void Incapsulate(Point A)
        {
            if (A.x < Min.x)
            {
                Min.x = A.x;
            }
            else if (A.x > Max.x)
            {
                Max.x = A.x;
            }
            if (A.y < Min.y)
            {
                Min.y = A.y;
            }
            else if (A.y > Max.y)
            {
                Max.y = A.y;
            }
        }
        public bool Incapsulates(Point A)
        {
            if (A.x >= Min.x && A.x <= Max.x && A.y >= Min.y && A.y <= Max.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Incapsulates(Rectangle A, Point B)
        {
            if (B.x >= A.Min.x && B.x <= A.Max.x && B.y >= A.Min.y && B.y <= A.Max.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Overlaps(Rectangle A)
        {
            if (Max.x < A.Min.x || Min.x > A.Max.x || Max.y < A.Min.y || Min.y > A.Max.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool Overlaps(Rectangle A, Rectangle B)
        {
            if (A.Max.x < B.Min.x || A.Min.x > B.Max.x || A.Max.y < B.Min.y || A.Min.y > B.Max.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Rectangle Clone()
        {
            return Create(Min.Clone(), Max.Clone());
        }
    }
}