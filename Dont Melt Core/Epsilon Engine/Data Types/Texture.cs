using System;
using System.Collections.Generic;
namespace EpsilonEngine
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
        public static Texture Create(int width, int height)
        {
            Texture Output = new Texture();
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("Height");
            }
            Output.width = width;
            Output.height = height;
            Output.data = new Color[width * height];
            return Output;
        }
        public static Texture Create(Point dementions)
        {
            Texture Output = new Texture();
            if (dementions.x < 0 || dementions.y < 0)
            {
                throw new ArgumentOutOfRangeException("Dementions");
            }
            Output.width = dementions.x;
            Output.height = dementions.y;
            Output.data = new Color[dementions.x * dementions.y];
            return Output;
        }
        public static Texture Create(int width, int height, Color fillColor)
        {
            Texture Output = new Texture();
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("Height");
            }
            Output.width = width;
            Output.height = height;
            Output.data = new Color[width * height];
            for (int i = 0; i < width * height; i++)
            {
                Output.data[i] = fillColor.Clone();
            }
            return Output;
        }
        public static Texture Create(Point dementions, Color fillColor)
        {
            Texture Output = new Texture();
            if (dementions.x < 0 || dementions.y < 0)
            {
                throw new ArgumentOutOfRangeException("Dementions");
            }
            Output.width = dementions.x;
            Output.height = dementions.y;
            Output.data = new Color[dementions.x * dementions.y];
            for (int i = 0; i < dementions.x * dementions.y; i++)
            {
                Output.data[i] = fillColor.Clone();
            }
            return Output;
        }
        public static Texture Create(Color[] data, int width, int height)
        {
            Texture Output = new Texture();
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("Height");
            }
            if (data.Length != width * height)
            {
                throw new ArgumentOutOfRangeException("Data");
            }
            Output.width = width;
            Output.height = height;
            Output.data = new List<Color>(data).ToArray();
            return Output;
        }
        public static Texture Create(Point dementions, Color[] data)
        {
            Texture Output = new Texture();
            if (dementions.x < 0 || dementions.y < 0)
            {
                throw new ArgumentOutOfRangeException("Dementions");
            }
            Output.width = dementions.x;
            Output.height = dementions.y;
            Output.data = new List<Color>(data).ToArray();
            return Output;
        }

        public Color GetPixel(Point position)
        {
            if (position.x < 0 || position.x >= width || position.y < 0 || position.y >= height)
            {
                throw new ArgumentOutOfRangeException("Position");
            }
            return data[(position.y * width) + position.x];
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                throw new ArgumentOutOfRangeException("Position");
            }
            return data[(y * width) + x];
        }
        public void SetPixel(Point position, Color newColor)
        {
            if (position.x < 0 || position.x >= width || position.y < 0 || position.y >= height)
            {
                throw new ArgumentOutOfRangeException("Position");
            }
            data[(position.y * width) + position.x] = newColor.Clone();
        }

        public void Blitz(Texture data, Point target)
        {
            if (target.x < 0 || target.y < 0 || target.x + data.width - 1 >= width || target.y + data.height - 1 >= height)
            {
                throw new ArgumentOutOfRangeException("Target");
            }
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    SetPixel(Point.Create(x + target.x, y + target.y), data.GetPixel(x, y));
                }
            }
        }
        public void Blitz(Texture data, int targetX, int targetY)
        {
            if (targetX < 0 || targetX + data.width - 1 >= width)
            {
                throw new ArgumentOutOfRangeException("Target.X");
            }
            if (targetY < 0 || targetY + data.height - 1 >= height)
            {
                throw new ArgumentOutOfRangeException("Target.Y");
            }
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    SetPixel(Point.Create(x + targetX, y + targetY), data.GetPixel(x, y));
                }
            }
        }
        //BlitzClip needs optimization because testing validity every pixel is redundant when a valid rect could be used instead.
        public void BlitzClip(Texture data, Point target)
        {
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    Point TargetPoint = Point.Create(x + target.x, y + target.y);
                    if (TargetPoint.x >= 0 && TargetPoint.x < width && TargetPoint.y >= 0 && TargetPoint.y < height)
                    {
                        SetPixel(TargetPoint, data.GetPixel(x, y));
                    }
                }
            }
        }
        public void BlitzClip(Texture data, int targetX, int targetY)
        {
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    Point TargetPoint = Point.Create(x + targetX, y + targetY);
                    if (TargetPoint.x >= 0 && TargetPoint.x < width && TargetPoint.y >= 0 && TargetPoint.y < height)
                    {
                        SetPixel(TargetPoint, data.GetPixel(x, y));
                    }
                }
            }
        }

        public Color[] GetData()
        {
            return new List<Color>(data).ToArray();
        }
        public void SetData(Color[] data, int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("Height");
            }
            if (data == null)
            {
                throw new NullReferenceException("Data");
            }
            if (data.Length != (width * height))
            {
                throw new ArgumentOutOfRangeException("Data");
            }
            this.data = new List<Color>(data).ToArray();
        }
        public void SetData(Color[] data, Point dementions)
        {
            if (dementions.x < 0 || dementions.y < 0)
            {
                throw new ArgumentOutOfRangeException("Dementions");
            }
            if (data == null)
            {
                throw new NullReferenceException("Data");
            }
            if (data.Length != (dementions.x * dementions.y))
            {
                throw new ArgumentOutOfRangeException("Data");
            }
            width = dementions.x;
            height = dementions.y;
            this.data = new List<Color>(data).ToArray();
        }
        public void SetData(Color[] data)
        {
            if (data == null)
            {
                throw new NullReferenceException("Data");
            }
            if (data.Length != this.data.Length)
            {
                throw new ArgumentOutOfRangeException("Data");
            }
            this.data = new List<Color>(data).ToArray();
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