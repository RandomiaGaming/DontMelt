using System;

namespace EpsilonEngine
{
    public static class MathHelper
    {
        public static int Clamp(int Value, int Min, int Max)
        {
            if (Value > Max)
            {
                return Max;
            }
            else if (Value < Min)
            {
                return Min;
            }
            else
            {
                return Value;
            }
        }
        public static float Clamp(float Value, float Min, float Max)
        {
            if (Value > Max)
            {
                return Max;
            }
            else if (Value < Min)
            {
                return Min;
            }
            else
            {
                return Value;
            }
        }
        public static double Clamp(double Value, double Min, double Max)
        {
            if (Value > Max)
            {
                return Max;
            }
            else if (Value < Min)
            {
                return Min;
            }
            else
            {
                return Value;
            }
        }
        public static decimal Clamp(decimal Value, decimal Min, decimal Max)
        {
            if (Value > Max)
            {
                return Max;
            }
            else if (Value < Min)
            {
                return Min;
            }
            else
            {
                return Value;
            }
        }
        public static int Abs(int Value)
        {
            if (Value < 0)
            {
                return Value * -1;
            }
            else
            {
                return Value;
            }
        }
        public static float Abs(float Value)
        {
            if (Value < 0)
            {
                return Value * -1;
            }
            else
            {
                return Value;
            }
        }
        public static double Abs(double Value)
        {
            if (Value < 0)
            {
                return Value * -1;
            }
            else
            {
                return Value;
            }
        }
        public static decimal Abs(decimal Value)
        {
            if (Value < 0)
            {
                return Value * -1;
            }
            else
            {
                return Value;
            }
        }
        public static int Min(int A, int B)
        {
            if (A < B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static float Min(float A, float B)
        {
            if (A < B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static double Min(double A, double B)
        {
            if (A < B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static decimal Min(decimal A, decimal B)
        {
            if (A < B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static int Max(int A, int B)
        {
            if (A > B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static float Max(float A, float B)
        {
            if (A > B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static double Max(double A, double B)
        {
            if (A > B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static decimal Max(decimal A, decimal B)
        {
            if (A > B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static int Sqrt(int Value)
        {
            return (int)Math.Sqrt(Value);
        }
        public static float Sqrt(float Value)
        {
            return (float)Math.Sqrt(Value);
        }
        public static double Sqrt(double Value)
        {
            return Math.Sqrt(Value);
        }
        public static decimal Sqrt(decimal Value)
        {
            return (decimal)Math.Sqrt((double)Value);
        }
    }
}