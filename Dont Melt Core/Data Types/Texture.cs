using System;
using System.Collections.Generic;
namespace DontMelt
{
    public sealed class Texture
    {
        public int width { get; private set; }
        public int height { get; private set; }
        private Color[] data = new Color[0];

        private Texture() { }
        public static Texture Create()
        {
            Texture Output = new Texture();
            Output.width = 0;
            Output.height = 0;
            Output.data = new Color[0];
            return Output;
        }
        public static Texture Create(int Width, int Height)
        {
            Texture Output = new Texture();
            if (Width < 0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            if (Height < 0)
            {
                throw new ArgumentOutOfRangeException("Height");
            }
            Output.width = Width;
            Output.height = Height;
            Output.data = new Color[Width * Height];
            return Output;
        }
        public static Texture Create(Point Dementions)
        {
            Texture Output = new Texture();
            if (Dementions.x < 0 || Dementions.y < 0)
            {
                throw new ArgumentOutOfRangeException("Dementions");
            }
            Output.width = Dementions.x;
            Output.height = Dementions.y;
            Output.data = new Color[Dementions.x * Dementions.y];
            return Output;
        }
        public static Texture Create(int Width, int Height, Color FillColor)
        {
            Texture Output = new Texture();
            if (Width < 0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            if (Height < 0)
            {
                throw new ArgumentOutOfRangeException("Height");
            }
            Output.width = Width;
            Output.height = Height;
            Output.data = new Color[Width * Height];
            for (int i = 0; i < Width * Height; i++)
            {
                Output.data[i] = FillColor.Clone();
            }
            return Output;
        }
        public static Texture Create(Point Dementions, Color FillColor)
        {
            Texture Output = new Texture();
            if (Dementions.x < 0 || Dementions.y < 0)
            {
                throw new ArgumentOutOfRangeException("Dementions");
            }
            Output.width = Dementions.x;
            Output.height = Dementions.y;
            Output.data = new Color[Dementions.x * Dementions.y];
            for (int i = 0; i < Dementions.x * Dementions.y; i++)
            {
                Output.data[i] = FillColor.Clone();
            }
            return Output;
        }
        public static Texture Create(int Width, int Height, Color[] Data)
        {
            Texture Output = new Texture();
            if (Width < 0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            if (Height < 0)
            {
                throw new ArgumentOutOfRangeException("Height");
            }
            if (Data.Length != Width * Height)
            {
                throw new ArgumentOutOfRangeException("Data");
            }
            Output.width = Width;
            Output.height = Height;
            Output.data = new List<Color>(Data).ToArray();
            return Output;
        }
        public static Texture Create(Point Dementions, Color[] Data)
        {
            Texture Output = new Texture();
            if (Dementions.x < 0 || Dementions.y < 0)
            {
                throw new ArgumentOutOfRangeException("Dementions");
            }
            Output.width = Dementions.x;
            Output.height = Dementions.y;
            Output.data = new List<Color>(Data).ToArray();
            return Output;
        }

        public Color GetPixel(Point Position)
        {
            if (Position.x < 0 || Position.x >= width || Position.y < 0 || Position.y >= height)
            {
                throw new ArgumentOutOfRangeException("Position");
            }
            return data[(Position.y * width) + Position.x];
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                throw new ArgumentOutOfRangeException("Position");
            }
            return data[(y * width) + x];
        }
        public void SetPixel(Point Position, Color New_Color)
        {
            if (Position.x < 0 || Position.x >= width || Position.y < 0 || Position.y >= height)
            {
                throw new ArgumentOutOfRangeException("Position");
            }
            data[(Position.y * width) + Position.x] = New_Color.Clone();
        }

        public void Blitz(Texture Data, Point Target)
        {
            if (Target.x < 0 || Target.y < 0 || Target.x + Data.width - 1 >= width || Target.y + Data.height - 1 >= height)
            {
                throw new ArgumentOutOfRangeException("Target");
            }
            for (int x = 0; x < Data.width; x++)
            {
                for (int y = 0; y < Data.height; y++)
                {
                    SetPixel(Point.Create(x + Target.x, y + Target.y), Data.GetPixel(x, y));
                }
            }
        }
        public void Blitz(Texture Data, int TargetX, int TargetY)
        {
            if (TargetX < 0 || TargetX + Data.width - 1 >= width)
            {
                throw new ArgumentOutOfRangeException("TargetX");
            }
            if (TargetY < 0 || TargetY + Data.height - 1 >= height)
            {
                throw new ArgumentOutOfRangeException("TargetY");
            }
            for (int x = 0; x < Data.width; x++)
            {
                for (int y = 0; y < Data.height; y++)
                {
                    SetPixel(Point.Create(x + TargetX, y + TargetY), Data.GetPixel(x, y));
                }
            }
        }
        //BlitzClip needs optimization because testing validity every pixel is redundant when a valid rect could be used instead.
        public void BlitzClip(Texture Data, Point Target)
        {
            for (int x = 0; x < Data.width; x++)
            {
                for (int y = 0; y < Data.height; y++)
                {
                    Point TargetPoint = Point.Create(x + Target.x, y + Target.y);
                    if (TargetPoint.x >= 0 && TargetPoint.x < width && TargetPoint.y >= 0 && TargetPoint.y < height)
                    {
                        SetPixel(TargetPoint, Data.GetPixel(x, y));
                    }
                }
            }
        }
        public void BlitzClip(Texture Data, int TargetX, int TargetY)
        {
            for (int x = 0; x < Data.width; x++)
            {
                for (int y = 0; y < Data.height; y++)
                {
                    Point TargetPoint = Point.Create(x + TargetX, y + TargetY);
                    if (TargetPoint.x >= 0 && TargetPoint.x < width && TargetPoint.y >= 0 && TargetPoint.y < height)
                    {
                        SetPixel(TargetPoint, Data.GetPixel(x, y));
                    }
                }
            }
        }

        public Color[] GetData()
        {
            return new List<Color>(data).ToArray();
        }
        public void SetData(Color[] Data, int Width, int Height)
        {
            if (Width < 0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            if (Height < 0)
            {
                throw new ArgumentOutOfRangeException("Height");
            }
            if (Data == null)
            {
                throw new NullReferenceException("Data");
            }
            if (Data.Length != (Width * Height))
            {
                throw new ArgumentOutOfRangeException("Data");
            }
            this.data = new List<Color>(Data).ToArray();
        }
        public void SetData(Color[] Data, Point Dementions)
        {
            if (Dementions.x < 0 || Dementions.y < 0)
            {
                throw new ArgumentOutOfRangeException("Dementions");
            }
            if (Data == null)
            {
                throw new NullReferenceException("Data");
            }
            if (Data.Length != (Dementions.x * Dementions.y))
            {
                throw new ArgumentOutOfRangeException("Data");
            }
            width = Dementions.x;
            height = Dementions.y;
            this.data = new List<Color>(Data).ToArray();
        }
        public void SetData(Color[] Data)
        {
            if (Data == null)
            {
                throw new NullReferenceException("Data");
            }
            if (Data.Length != this.data.Length)
            {
                throw new ArgumentOutOfRangeException("Data");
            }
            this.data = new List<Color>(Data).ToArray();
        }

        public Point GetDementions()
        {
            return Point.Create(width, height);
        }

        public Texture Clone()
        {
            Texture Output = new Texture();
            Output.width = width;
            Output.height = height;
            Output.data = new List<Color>(data).ToArray();
            return Output;
        }
    }
}