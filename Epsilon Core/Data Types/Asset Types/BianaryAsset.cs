using System.IO;
namespace Epsilon
{
    public sealed class BianaryAsset : AssetBase
    {
        public byte[] data = null;
        private BianaryAsset()
        {
            stream = null;
            path = null;
            data = null;
        }
        public BianaryAsset(string path, Stream stream)
        {
            this.path = path;
            this.stream = stream;
            data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
        }
    }
}
