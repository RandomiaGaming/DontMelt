using System;
namespace EpsilonEngine
{
    public sealed class Rectangle
    {
        private Point _min = Point.Create(0, 0);
        private Point _max = Point.Create(0, 0);
        public Point min { get { return _min; } set { if (value.x > _max.x) { throw new ArgumentOutOfRangeException("Value"); } else if (value.y > _max.y) { throw new ArgumentOutOfRangeException("Value"); } else { _min = value; } } }
        public Point max { get { return _max; } set { if (value.x < _min.x) { throw new ArgumentOutOfRangeException("Value"); } else if (value.y < _min.y) { throw new ArgumentOutOfRangeException("Value"); } else { _max = value; } } }
        private Rectangle() { }
        public static Rectangle Create()
        {
            Rectangle Output = new Rectangle();
            Output._min = Point.Create(0, 0);
            Output._max = Point.Create(0, 0);
            return Output;
        }
        public static Rectangle Create(Point size)
        {
            if (size.x < 0 || size.y < 0)
            {
                throw new ArgumentOutOfRangeException("Size");
            }
            Rectangle Output = new Rectangle();
            Output._min = Point.Create(0, 0);
            Output._max = Point.Create(size.x, size.y);
            return Output;
        }
        public static Rectangle Create(int sizeX, int sizeY)
        {
            if (sizeX < 0)
            {
                throw new ArgumentOutOfRangeException("Size.x");
            }
            if (sizeY < 0)
            {
                throw new ArgumentOutOfRangeException("Size.y");
            }
            Rectangle Output = new Rectangle();
            Output._min = Point.Create(0, 0);
            Output._max = Point.Create(sizeX, sizeY);
            return Output;
        }
        public static Rectangle Create(Point min, Point max)
        {
            Rectangle Output = new Rectangle();
            Output._min = Point.Create(MathHelper.Min(min.x, max.x), MathHelper.Min(min.y, max.y));
            Output._max = Point.Create(MathHelper.Max(min.x, max.x), MathHelper.Max(min.y, max.y));
            return Output;
        }
        public override string ToString()
        {
            return $"[{_min},{_max}]";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Rectangle))
            {
                return false;
            }
            else
            {
                return this == (Rectangle)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            else if (a is null || b is null)
            {
                return false;
            }
            return (a._min == b._min && a._max == b._max);
        }
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !(a == b);
        }
        public void Incapsulate(Point a)
        {
            if (a.x < _min.x)
            {
                _min.x = a.x;
            }
            else if (a.x > _max.x)
            {
                _max.x = a.x;
            }
            if (a.y < _min.y)
            {
                _min.y = a.y;
            }
            else if (a.y > _max.y)
            {
                _max.y = a.y;
            }
        }
        public bool Incapsulates(Point a)
        {
            if (a.x >= _min.x && a.x <= _max.x && a.y >= _min.y && a.y <= _max.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Incapsulates(Rectangle a, Point b)
        {
            if (b.x >= a._min.x && b.x <= a._max.x && b.y >= a._min.y && b.y <= a._max.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Overlaps(Rectangle a)
        {
            if (_max.x < a._min.x || _min.x > a._max.x || _max.y < a._min.y || _min.y > a._max.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool Overlaps(Rectangle a, Rectangle b)
        {
            if (a._max.x < b._min.x || a._min.x > b._max.x || a._max.y < b._min.y || a._min.y > b._max.y)
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
            Rectangle output = new Rectangle();
            output._min = _min.Clone();
            output._max = _max.Clone();
            return output;
        }
    }
}