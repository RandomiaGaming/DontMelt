using System;

namespace DontMelt
{
    public sealed class Rectangle
    {
        public Point min = Point.Create(0, 0);
        public Point max = Point.Create(1, 1);
        private Rectangle() { }
        public static Rectangle Create()
        {
            Rectangle Output = new Rectangle();
            Output.min = Point.Create(0, 0);
            Output.max = Point.Create(0, 0);
            return Output;
        }
        public static Rectangle Create(Point Size)
        {
            if (Size.x < 0)
            {
                throw new ArgumentOutOfRangeException("x");
            }
            if (Size.y < 0)
            {
                throw new ArgumentOutOfRangeException("y");
            }
            Rectangle Output = new Rectangle();
            Output.min = Point.Create(0, 0);
            Output.max = Point.Create(Size.x, Size.y);
            return Output;
        }
        public static Rectangle Create(Point Min, Point Max)
        {
            Rectangle Output = new Rectangle();
            Output.min = Point.Create(MathHelper.Min(Min.x, Max.x), MathHelper.Min(Min.y, Max.y));
            Output.max = Point.Create(MathHelper.Max(Min.x, Max.x), MathHelper.Max(Min.y, Max.y));
            return Output;
        }
        public override string ToString()
        {
            return $"[{min},{max}]";
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
            return min.Clone();
        }
        public Point Get_Max()
        {
            return max.Clone();
        }
        public void Incapsulate(Point A)
        {
            if (A.x < min.x)
            {
                min.x = A.x;
            }
            else if (A.x > max.x)
            {
                max.x = A.x;
            }
            if (A.y < min.y)
            {
                min.y = A.y;
            }
            else if (A.y > max.y)
            {
                max.y = A.y;
            }
        }
        public bool Incapsulates(Point A)
        {
            if (A.x >= min.x && A.x <= max.x && A.y >= min.y && A.y <= max.y)
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
            if (B.x >= A.min.x && B.x <= A.max.x && B.y >= A.min.y && B.y <= A.max.y)
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
            if (max.x < A.min.x || min.x > A.max.x || max.y < A.min.y || min.y > A.max.y)
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
            if (A.max.x < B.min.x || A.min.x > B.max.x || A.max.y < B.min.y || A.min.y > B.max.y)
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
            return Create(min.Clone(), max.Clone());
        }
    }
}