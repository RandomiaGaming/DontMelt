using System;

namespace DontMelt
{
    public sealed class Color
    {
        private int _r = 0;
        private int _g = 0;
        private int _b = 0;
        private int _a = 255;

        public int r { get { return _r; } set { if (value >= 0 && value <= 255) { _r = value; } else { throw new ArgumentOutOfRangeException("r"); } } }
        public int g { get { return _g; } set { if (value >= 0 && value <= 255) { _g = value; } else { throw new ArgumentOutOfRangeException("g"); } } }
        public int b { get { return _b; } set { if (value >= 0 && value <= 255) { _b = value; } else { throw new ArgumentOutOfRangeException("b"); } } }
        public int a { get { return _a; } set { if (value >= 0 && value <= 255) { _a = value; } else { throw new ArgumentOutOfRangeException("a"); } } }

        private Color() { }
        public static Color Create()
        {
            Color Output = new Color();
            Output.r = 0;
            Output.g = 0;
            Output.b = 0;
            Output.a = 255;
            return Output;
        }

        public static Color Create(int r, int g, int b, int a)
        {
            if (r < 0 || r > 255)
            {
                throw new ArgumentOutOfRangeException("r");
            }
            if (g < 0 || g > 255)
            {
                throw new ArgumentOutOfRangeException("g");
            }
            if (b < 0 || b > 255)
            {
                throw new ArgumentOutOfRangeException("b");
            }
            if (a < 0 || a > 255)
            {
                throw new ArgumentOutOfRangeException("a");
            }
            Color Output = new Color();
            Output.r = r;
            Output.g = g;
            Output.b = b;
            Output.a = a;
            return Output;
        }
        public static Color Create(int r, int g, int b)
        {
            return Create(r, g, b, 255);
        }

        public override string ToString()
        {
            return $"(r:{r}, g:{g}, b:{b})";
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Color))
            {
                return this == (Color)obj;
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

        public static bool operator ==(Color A, Color B)
        {
            if (ReferenceEquals(A, null) && ReferenceEquals(B, null))
            {
                return true;
            }
            else if (ReferenceEquals(A, null) || ReferenceEquals(B, null))
            {
                return false;
            }
            return (A.r == B.r) && (A.g == B.g) && (A.b == B.b);
        }
        public static bool operator !=(Color A, Color B)
        {
            return !(A == B);
        }

        public static Color Mix(Color A, Color B)
        {
            return Create((A.r + B.r) / 2, (A.g + B.g) / 2, (A.b + B.b) / 2);
        }

        public Color Clone()
        {
            Color Output = new Color();
            Output.r = r;
            Output.g = g;
            Output.b = b;
            Output.a = a;
            return Output;
        }
    }
}