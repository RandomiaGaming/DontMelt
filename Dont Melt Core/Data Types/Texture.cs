using System;
namespace DontMelt
{
    public sealed class Texture
    {
        private uint Width = 0;
        private uint Height = 0;
        private Color[] Data = new Color[0];

        private Texture() { }
        public static Texture Create()
        {
            Texture Output = new Texture();
            Output.Width = 0;
            Output.Height = 0;
            Output.Data = new Color[0];
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
            Output.Width = (uint)Width;
            Output.Height = (uint)Height;
            Output.Data = new Color[Width * Height];
            return Output;
        }
        public static Texture Create(int Width, int Height, Color Fill_Color)
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
            Output.Width = (uint)Width;
            Output.Height = (uint)Height;
            int PixelCount = Width * Height;
            Output.Data = new Color[PixelCount];
            for (int i = 0; i < PixelCount; i++)
            {
                Output.Data[i] = Fill_Color.Clone();
            }
            return Output;
        }
        public Color Get_Pixel(Point Position)
        {
            if (Position.x < 0 || Position.x >= Width || Position.y < 0 || Position.y >= Height)
            {
                throw new ArgumentOutOfRangeException("Position");
            }
            return Data[(Position.y * Width) + Position.x];
        }
        public void Set_Pixel(Point Position, Color New_Color)
        {
            if (Position.x < 0 || Position.x >= Width || Position.y < 0 || Position.y >= Height)
            {
                throw new ArgumentOutOfRangeException("Position");
            }
            Data[(Position.y * Width) + Position.x] = New_Color.Clone();
        }
        public int Get_Width()
        {
            return (int)Width;
        }
        public int Get_Height()
        {
            return (int)Height;
        }
        public Point Get_Dementions()
        {
            return Point.Create((int)Width, (int)Height);
        }

        public Texture Clone()
        {
            Texture Output = new Texture();
            Output.Width = Width;
            Output.Height = Height;
            Output.Data = (Color[])Data.Clone();
            return Output;
        }
    }
}