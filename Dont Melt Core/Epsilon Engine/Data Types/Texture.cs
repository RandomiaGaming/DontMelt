using System;
namespace EpsilonEngine
{
    public sealed class Texture
    {
        public int width { get; private set; } = 1;
        public int height { get; private set; } = 1;
        private Color[] data = new Color[1];
        private Texture() { }
        public static Texture Create(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Texture output = new Texture();
            output.width = width;
            output.height = height;
            output.data = new Color[width * height];
            return output;
        }
        public static Texture Create(Point size)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Texture output = new Texture();
            output.width = size.x;
            output.height = size.y;
            output.data = new Color[size.x * size.y];
            return output;
        }
        public static Texture Create(int width, int height, Color fillColor)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Texture output = new Texture();
            output.width = width;
            output.height = height;
            output.data = new Color[width * height];
            for (int i = 0; i < output.data.Length; i++)
            {
                output.data[i] = fillColor;
            }
            return output;
        }
        public static Texture Create(Point size, Color fillColor)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Texture output = new Texture();
            output.width = size.x;
            output.height = size.y;
            output.data = new Color[size.x * size.y];
            for (int i = 0; i < output.data.Length; i++)
            {
                output.data[i] = fillColor;
            }
            return output;
        }
        public static Texture Create(int width, int height, Color[] data)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (data.Length != width * height)
            {
                throw new ArgumentException();
            }
            Texture output = new Texture();
            output.width = width;
            output.height = height;
            output.data = (Color[])data.Clone();
            return output;
        }
        public static Texture Create(Point size, Color[] data)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (data.Length != size.x * size.y)
            {
                throw new ArgumentException();
            }
            Texture output = new Texture();
            output.width = size.x;
            output.height = size.y;
            output.data = (Color[])data.Clone();
            return output;
        }

        public void Blitz(Texture data, int targetX, int targetY)
        {
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    if (x + targetX >= 0 && x + targetX < width && y + targetY >= 0 && y + targetY < height)
                    {
                        Color otherColor = data.GetPixelUnsafe(x, y);
                        Color thisColor = GetPixelUnsafe(x + targetX, y + targetY);
                        SetPixelUnsafe(x + targetX, y + targetY, Color.Mix(thisColor, otherColor));
                    }
                }
            }
        }

        internal void SetPixelUnsafe(Point point, Color newColor)
        {
            data[(point.y * width) + point.x] = newColor;
        }
        internal void SetPixelUnsafe(int x, int y, Color newColor)
        {
            data[(y * width) + x] = newColor;
        }
        internal Color GetPixelUnsafe(Point point)
        {
            return data[(point.y * width) + point.x];
        }
        internal Color GetPixelUnsafe(int x, int y)
        {
            return data[(y * width) + x];
        }

        public void SetPixel(Point point, Color newColor)
        {
            if (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height)
            {
                throw new ArgumentOutOfRangeException();
            }
            data[(point.y * width) + point.x] = newColor;
        }
        public void SetPixel(int x, int y, Color newColor)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                throw new ArgumentOutOfRangeException();
            }
            data[(y * width) + x] = newColor;
        }
        public Color GetPixel(Point point)
        {
            if (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height)
            {
                throw new ArgumentOutOfRangeException();
            }
            return data[(point.y * width) + point.x];
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                throw new ArgumentOutOfRangeException();
            }
            return data[(y * width) + x];
        }

        public void SetData(Color[] newData)
        {
            if (newData.Length != width * height)
            {
                throw new ArgumentException();
            }
            data = (Color[])newData.Clone();
        }
        public Color[] GetData()
        {
            return (Color[])data.Clone();
        }
        public Texture Clone()
        {
            Texture output = new Texture();
            output.width = width;
            output.height = height;
            output.data = (Color[])data.Clone();
            return output;
        }
    }
}