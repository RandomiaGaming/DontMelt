using DontMelt.Helpers;
namespace DontMelt.Data
{
    public sealed class Rectangle
    {
        private Vector Min = Vector.Create(0, 0);
        private Vector Max = Vector.Create(1, 1);

        private Rectangle() { }
        public static Rectangle Create()
        {
            Rectangle Output = new Rectangle();
            Output.Min = Vector.Create(0, 0);
            Output.Max = Vector.Create(0, 0);
            return Output;
        }
        public static Rectangle Create(Vector Size)
        {
            Rectangle Output = new Rectangle();
            Output.Min = Vector.Create(0, 0);
            Output.Max = Vector.Create(MathHelper.Abs(Size.x), MathHelper.Abs(Size.y));
            return Output;
        }
        public static Rectangle Create(Vector Min, Vector Max)
        {
            Rectangle Output = new Rectangle();
            Output.Min = Vector.Create(MathHelper.Min(Min.x, Max.x), MathHelper.Min(Min.y, Max.y));
            Output.Max = Vector.Create(MathHelper.Max(Min.x, Max.x), MathHelper.Max(Min.y, Max.y));
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

        public Vector Get_Min()
        {
            return Min.Clone();
        }
        public Vector Get_Max()
        {
            return Max.Clone();
        }
        public bool Incapsulates(Vector A)
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
        public void Incapsulate(Vector A)
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

        public Rectangle Clone()
        {
            return Create(Min.Clone(), Max.Clone());
        }
    }
}