using DontMelt.Helpers;
namespace DontMelt.Data
{
    public sealed class Color
    {
        public byte r = 255;
        public byte g = 255;
        public byte b = 255;
        public byte a = 255;

        private Color() { }
        public static Color Create()
        {
            Color Output = new Color();
            Output.r = 255;
            Output.g = 255;
            Output.b = 255;
            Output.a = 255;
            return Output;
        }
        public static Color Create(int r, int g, int b, int a)
        {
            Color Output = new Color();
            Output.r = (byte)r;
            Output.g = (byte)g;
            Output.b = (byte)b;
            Output.a = (byte)a;
            return Output;
        }
        public static Color Create(int r, int g, int b)
        {
            Color Output = new Color();
            Output.r = (byte)r;
            Output.g = (byte)g;
            Output.b = (byte)b;
            Output.a = 255;
            return Output;
        }
        public static Color Create(byte r, byte g, byte b, byte a)
        {
            Color Output = new Color();
            Output.r = r;
            Output.g = g;
            Output.b = b;
            Output.a = a;
            return Output;
        }
        public static Color Create(byte r, byte g, byte b)
        {
            Color Output = new Color();
            Output.r = r;
            Output.g = g;
            Output.b = b;
            Output.a = 255;
            return Output;
        }
        public static Color Create(float r, float g, float b, float a)
        {
            Color Output = new Color();
            Output.r = (byte)(r * 255);
            Output.g = (byte)(g * 255);
            Output.b = (byte)(b * 255);
            Output.a = (byte)(a * 255);
            return Output;
        }
        public static Color Create(float r, float g, float b)
        {
            Color Output = new Color();
            Output.r = (byte)(r * 255);
            Output.g = (byte)(g * 255);
            Output.b = (byte)(b * 255);
            Output.a = 255;
            return Output;
        }
        public static Color Create(double r, double g, double b, double a)
        {
            Color Output = new Color();
            Output.r = (byte)(r * 255);
            Output.g = (byte)(g * 255);
            Output.b = (byte)(b * 255);
            Output.a = (byte)(a * 255);
            return Output;
        }
        public static Color Create(double r, double g, double b)
        {
            Color Output = new Color();
            Output.r = (byte)(r * 255);
            Output.g = (byte)(g * 255);
            Output.b = (byte)(b * 255);
            Output.a = 255;
            return Output;
        }
        public static Color Create(decimal r, decimal g, decimal b, decimal a)
        {
            Color Output = new Color();
            Output.r = (byte)(r * 255);
            Output.g = (byte)(g * 255);
            Output.b = (byte)(b * 255);
            Output.a = (byte)(a * 255);
            return Output;
        }
        public static Color Create(decimal r, decimal g, decimal b)
        {
            Color Output = new Color();
            Output.r = (byte)(r * 255);
            Output.g = (byte)(g * 255);
            Output.b = (byte)(b * 255);
            Output.a = 255;
            return Output;
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

        public static Color operator +(Color A, Color B)
        {
            return Create(MathHelper.Clamp(A.r + B.r, 0, 255), MathHelper.Clamp(A.g + B.g, 0, 255), MathHelper.Clamp(A.b + B.b, 0, 255));
        }
        public static Color operator -(Color A, Color B)
        {
            return Create(MathHelper.Clamp(A.r - B.r, 0, 255), MathHelper.Clamp(A.g - B.g, 0, 255), MathHelper.Clamp(A.b - B.b, 0, 255));
        }
        public static Color operator +(Color A, int B)
        {
            return Create(MathHelper.Clamp(A.r + B, 0, 255), MathHelper.Clamp(A.g + B, 0, 255), MathHelper.Clamp(A.b + B, 0, 255));
        }
        public static Color operator -(Color A, int B)
        {
            return Create(MathHelper.Clamp(A.r - B, 0, 255), MathHelper.Clamp(A.g - B, 0, 255), MathHelper.Clamp(A.b - B, 0, 255));
        }

        public static Color operator *(Color A, Color B)
        {
            return Create(MathHelper.Clamp(A.r * B.r, 0, 255), MathHelper.Clamp(A.g * B.g, 0, 255), MathHelper.Clamp(A.b * B.b, 0, 255));
        }
        public static Color operator /(Color A, Color B)
        {
            return Create(MathHelper.Clamp(A.r / B.r, 0, 255), MathHelper.Clamp(A.g / B.g, 0, 255), MathHelper.Clamp(A.b / B.b, 0, 255));
        }
        public static Color operator *(Color A, int B)
        {
            return Create(MathHelper.Clamp(A.r * B, 0, 255), MathHelper.Clamp(A.g * B, 0, 255), MathHelper.Clamp(A.b * B, 0, 255));
        }
        public static Color operator /(Color A, int B)
        {
            return Create(MathHelper.Clamp(A.r / B, 0, 255), MathHelper.Clamp(A.g / B, 0, 255), MathHelper.Clamp(A.b / B, 0, 255));
        }

        public static Color operator +(Color A)
        {
            return A.Clone();
        }
        public static Color operator -(Color A)
        {
            return Create(255 - A.r, 255 - A.g, 255 - A.b);
        }

        public static Color operator %(Color A, Color B)
        {
            return ((A + B) / 2).Clone();
        }

        public Color Clone()
        {
            return Create(r, g, b);
        }
    }
}