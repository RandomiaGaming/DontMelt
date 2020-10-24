using System.IO;
namespace Epsilon
{
    public abstract class AssetBase
    {
        public Stream stream { get; protected set; } = null;
        public string path { get; protected set; } = null;
        public string GetFullFileName()
        {
            string[] splitPath = path.Split('.');
            return (splitPath[splitPath.Length - 2] + "." + splitPath[splitPath.Length - 1]).ToLower();
        }
        public string GetFileName()
        {
            string[] splitPath = path.Split('.');
            return splitPath[splitPath.Length - 2].ToLower();
        }
        public string GetFileExtension()
        {
            string[] splitPath = path.Split('.');
            return splitPath[splitPath.Length - 1].ToLower();
        }

        public static string GetFullFileName(string path)
        {
            string[] splitPath = path.Split('.');
            return (splitPath[splitPath.Length - 2] + "." + splitPath[splitPath.Length - 1]).ToLower();
        }
        public static string GetFileName(string path)
        {
            string[] splitPath = path.Split('.');
            return splitPath[splitPath.Length - 2].ToLower();
        }
        public static string GetFileExtension(string path)
        {
            string[] splitPath = path.Split('.');
            return splitPath[splitPath.Length - 1].ToLower();
        }
        public sealed override string ToString()
        {
            return GetFullFileName();
        }
    }
}
