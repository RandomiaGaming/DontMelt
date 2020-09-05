using System;
using System.Collections.Generic;
namespace DontMelt.Data
{
    public sealed class Texture
    {
        private uint Width = 0;
        private uint Height = 0;
        private uint Height = 0;
        private Color[] Data = new Color[0];

        private Texture() { }
        public static Texture Create()
        {
            Texture Output = new Texture();
            Output.Width = 1;
            Output.Height = 1;
            Output.Current_Pixel_Data = new List<PixelData>();
            Output.Current_Pixel_Data.Add(PixelData.Create(VectorInt.Create(0, 0), Color.Create(255, 255, 255)));
            return Output;
        }
        public static Texture Create(int Width, int Height)
        {
            Texture Output = new Texture();
            Output.Width = Width;
            Output.Height = Height;
            Output.Current_Pixel_Data = new List<PixelData>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Output.Current_Pixel_Data.Add(PixelData.Create(VectorInt.Create(x, y), Color.Create(255, 255, 255)));
                }
            }
            return Output;
        }
        public static Texture Create(int Width, int Height, Color Fill_Color)
        {
            Texture Output = new Texture();
            Output.Width = Width;
            Output.Height = Height;
            Output.Current_Pixel_Data = new List<PixelData>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Output.Current_Pixel_Data.Add(PixelData.Create(VectorInt.Create(x, y), Fill_Color.Clone()));
                }
            }
            return Output;
        }
        public Color Get_Pixel(Vector Position)
        {
            if (Position.x < 0 || Position.y < 0 || Position.x >= Width || Position.y >= Height)
            {
                return null;
            }
            for (int i = 0; i < Current_Pixel_Data.Count; i++)
            {
                if (Position == Current_Pixel_Data[i].Position)
                {
                    return Current_Pixel_Data[i].Color.Clone();
                }
            }
            return null;
        }
        public void Set_Pixel(VectorInt Position, Color New_Color)
        {
            if (Position.x >= 0 && Position.y < 0 || Position.x >= Width || Position.y >= Height)
            {
                return;
            }
            for (int i = 0; i < Current_Pixel_Data.Count; i++)
            {
                if (Position == Current_Pixel_Data[i].Position)
                {
                    Current_Pixel_Data.RemoveAt(i);
                    i--;
                }
            }
            if (New_Color != null)
            {
                Current_Pixel_Data.Add(PixelData.Create(Position, New_Color));
            }
        }
        public void Resize(int New_Width, int New_Height)
        {
            Width = Math.Clamp(New_Width, 0, 1024);
            Height = Math.Clamp(New_Height, 0, 1024);
            for (int i = 0; i < Current_Pixel_Data.Count; i++)
            {
                if (Current_Pixel_Data[i].Position.x >= Width || Current_Pixel_Data[i].Position.y >= Height)
                {
                    Current_Pixel_Data.RemoveAt(i);
                    i--;
                }
            }
        }
        public int Get_Width()
        {
            return Width;
        }
        public int Get_Height()
        {
            return Height;
        }
        public VectorInt Get_Dementions()
        {
            return VectorInt.Create(Width, Height);
        }

        public Texture Clone()
        {
            Texture Output = new Texture();
            Output.Width = Width;
            Output.Height = Height;
            Output.Current_Pixel_Data = new List<PixelData>(Current_Pixel_Data);
            return Output;
        }
    }
}