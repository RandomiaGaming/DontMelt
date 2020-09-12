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
        public void SetPixel(int x, int y, Color newColor)
        {
            if (x < 0 || x >= width)
            {
                throw new ArgumentOutOfRangeException("Position.x");
            }
            if (y < 0 || y >= width)
            {
                throw new ArgumentOutOfRangeException("Position.y");
            }
            data[(y * width) + x] = newColor.Clone();
        }

        public void Blitz(Texture data, int targetX, int targetY, bool ignoreAlpha)
        {
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    Point TargetPoint = Point.Create(x + targetX, y + targetY);
                    if (TargetPoint.x >= 0 && TargetPoint.x < width && TargetPoint.y >= 0 && TargetPoint.y < height)
                    {
                        if (ignoreAlpha)
                        {
                            Color newColor = data.GetPixel(x, y);
                            SetPixel(TargetPoint, newColor);
                        }
                        else
                        {
                            Color originalColor = GetPixel(TargetPoint);
                            Color newColor = data.GetPixel(x, y);
                            Color mixedColor = originalColor.Clone();
                            mixedColor.a = 255;
                            mixedColor.r = ((originalColor.r * (255 - newColor.a)) + (newColor.r * newColor.a)) / 255;
                            mixedColor.g = ((originalColor.g * (255 - newColor.a)) + (newColor.g * newColor.a)) / 255;
                            mixedColor.b = ((originalColor.b * (255 - newColor.a)) + (newColor.b * newColor.a)) / 255;
                            SetPixel(TargetPoint, mixedColor);
                        }
                    }
                }
            }
        }
        public void Blitz(Texture data, Point target, bool ignoreAlpha)
        {
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    Point TargetPoint = Point.Create(x + target.x, y + target.y);
                    if (TargetPoint.x >= 0 && TargetPoint.x < width && TargetPoint.y >= 0 && TargetPoint.y < height)
                    {
                        if (ignoreAlpha)
                        {
                            Color newColor = data.GetPixel(x, y);
                            SetPixel(TargetPoint, newColor);
                        }
                        else
                        {
                            Color originalColor = GetPixel(TargetPoint);
                            Color newColor = data.GetPixel(x, y);
                            Color mixedColor = originalColor.Clone();
                            mixedColor.a = 255;
                            mixedColor.r = ((originalColor.r * (255 - newColor.a)) + (newColor.r * newColor.a)) / 255;
                            mixedColor.g = ((originalColor.g * (255 - newColor.a)) + (newColor.g * newColor.a)) / 255;
                            mixedColor.b = ((originalColor.b * (255 - newColor.a)) + (newColor.b * newColor.a)) / 255;
                            SetPixel(TargetPoint, mixedColor);
                        }
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