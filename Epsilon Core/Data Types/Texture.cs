using System;
namespace Epsilon
{
    public struct Texture
    {
        public int width { get; private set; }
        public int height { get; private set; }
        private Color[] data;
        public Texture(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.width = width;
            this.height = height;
            data = new Color[width * height];
        }
        public Texture(Point size)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            width = size.x;
            height = size.y;
            data = new Color[size.x * size.y];
        }
        public Texture(int width, int height, Color fillColor)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.width = width;
            this.height = height;
            data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = fillColor;
            }
        }
        public Texture(Point size, Color fillColor)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            width = size.x;
            height = size.y;
            data = new Color[size.x * size.y];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = fillColor;
            }
        }
        public Texture(int width, int height, Color[] data)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (data.Length != width * height)
            {
                throw new ArgumentException();
            }
            this.width = width;
            this.height = height;
            this.data = (Color[])data.Clone();
        }
        public Texture(Point size, Color[] data)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (data.Length != size.x * size.y)
            {
                throw new ArgumentException();
            }
            width = size.x;
            height = size.y;
            this.data = (Color[])data.Clone();
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

        public void SetPixelUnsafe(Point point, Color newColor)
        {
            data[(point.y * width) + point.x] = newColor;
        }
        public void SetPixelUnsafe(int x, int y, Color newColor)
        {
            data[(y * width) + x] = newColor;
        }
        public Color GetPixelUnsafe(Point point)
        {
            return data[(point.y * width) + point.x];
        }
        public Color GetPixelUnsafe(int x, int y)
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