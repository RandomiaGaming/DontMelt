using System.IO;
using System.Reflection;
namespace EpsilonEngine
{
    public static class AssetLoader
    {
        public static Texture LoadTextureAsset(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(fileName);
            System.Drawing.Image loadedImage = System.Drawing.Image.FromStream(stream);
            System.Drawing.Bitmap loadedBitMap = new System.Drawing.Bitmap(loadedImage);
            Texture output = Texture.Create(loadedBitMap.Width, loadedBitMap.Height);
            for (int x = 0; x < loadedBitMap.Width; x++)
            {
                for (int y = 0; y < loadedBitMap.Height; y++)
                {
                    System.Drawing.Color systemColor = loadedBitMap.GetPixel(x, loadedBitMap.Height - y - 1);
                    output.SetPixel(x, y, new Color(systemColor.R, systemColor.G, systemColor.B, systemColor.A));
                }
            }
            return output;
        }
        public static byte[] LoadBianaryAsset(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(fileName);
            byte[] output = new byte[stream.Length];
            stream.Read(output, 0, (int)stream.Length);
            return output;
        }
        public static AudioClip LoadAudioAsset(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(fileName);
            byte[] wav = new byte[stream.Length];
            stream.Read(wav, 0, (int)stream.Length);
            AudioClip output = AudioClip.Create(wav, 48000);
            return output;
        }
    }
}
