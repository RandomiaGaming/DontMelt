using System;
namespace EpsilonEngine
{
    public struct Rectangle
    {
        private Point _min;
        private Point _max;
        public Point min
        {
            get
            {
                return _min;
            }
            set
            {
                if (value.x > _max.x || value.y > _max.y)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _min = value;
                }
            }
        }
        public Point max
        {
            get
            {
                return _max;
            }
            set
            {
                if (value.x < _min.x || value.y < _min.y)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _max = value;
                }
            }
        }
        public Rectangle(Point size)
        {
            if (size.x < 0 || size.y < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _min = Point.Zero;
            _max = size;
        }
        public Rectangle(int width, int height)
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _min = Point.Zero;
            _max = new Point(width, height);
        }
        public Rectangle(Point min, Point max)
        {
            if (min.x > max.x || min.y > max.y)
            {
                throw new ArgumentOutOfRangeException();
            }
            _min = min;
            _max = max;
        }
        public Rectangle(int xPosition, int yPosition, int width, int height)
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _min = new Point(xPosition, yPosition);
            _max = new Point(xPosition + width, yPosition + height);
        }
        public override string ToString()
        {
            return $"[{_min}, {_max}]";
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
    }
}