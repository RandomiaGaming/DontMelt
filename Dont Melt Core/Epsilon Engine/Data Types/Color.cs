using System;
namespace EpsilonEngine
{
    public struct Color
    {
        public static readonly Color White = new Color(255, 255, 255, 255);
        public static readonly Color Black = new Color(0, 0, 0, 255);
        public static readonly Color Clear = new Color(255, 255, 255, 0);
        public static readonly Color Red = new Color(255, 0, 0, 255);
        public static readonly Color Green = new Color(0, 255, 0, 255);
        public static readonly Color Blue = new Color(0, 0, 255, 255);
        public static readonly Color Pink = new Color(255, 0, 255, 255);
        private byte _r;
        private byte _g;
        private byte _b;
        private byte _a;
        public int r
        {
            get
            {
                return _r;
            }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _r = (byte)value;
                }
            }
        }
        public int g
        {
            get
            {
                return _g;
            }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _g = (byte)value;
                }
            }
        }
        public int b
        {
            get
            {
                return _b;
            }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _b = (byte)value;
                }
            }
        }
        public int a
        {
            get
            {
                return _a;
            }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _a = (byte)value;
                }
            }
        }
        public Color(int r, int g, int b)
        {
            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
            {
                throw new ArgumentOutOfRangeException();
            }
            _r = (byte)r;
            _g = (byte)g;
            _b = (byte)b;
            _a = 255;
        }
        public Color(int r, int g, int b, int a)
        {
            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255 || a < 0 || a > 255)
            {
                throw new ArgumentOutOfRangeException();
            }
            _r = (byte)r;
            _g = (byte)g;
            _b = (byte)b;
            _a = (byte)a;
        }
        public override string ToString()
        {
            return $"(r: {r}, g: {g}, b: {b})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Color))
            {
                return false;
            }
            else
            {
                return this == (Color)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Color a, Color b)
        {
            return (a.r == b.r) && (a.g == b.g) && (a.b == b.b) && (a.a == b.a);
        }
        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        }
        public static Color EvenMix(Color a, Color b)
        {
            return new Color((a.r + b.r) / 2, (a.g + b.g) / 2, (a.b + b.b) / 2, 255);
        }
        public static Color Mix(Color back, Color front)
        {
            if (front.a == 0)
            {
                return back;
            }
            else if (front.a == 255)
            {
                return front;
            }
            else
            {
                return new Color((back.r + front.r) / 2, (back.g + front.g) / 2, (back.b + front.b) / 2, 255);
            }
        }
    }
}