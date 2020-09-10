using System.IO;
using System.Reflection;
using System.Text;

namespace DontMelt.Internal
{
    public static class AssetLoader
    {
        public static byte[] LoadBianaryAsset(string assetCode)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(assetCode);
            StreamReader streamReader = new StreamReader(stream);
            return Encoding.ASCII.GetBytes(streamReader.ReadToEnd());
        }
    }
}
