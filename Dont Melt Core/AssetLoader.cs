using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DontMelt.Internal
{
    public static class AssetLoader
    {
        public static byte[] LoadBianaryAsset(string Name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream AssetStream = assembly.GetManifestResourceStream(Name);
            StreamReader AssetReader = new StreamReader(AssetStream);
            return Encoding.ASCII.GetBytes(AssetReader.ReadToEnd());
        }
    }
}
