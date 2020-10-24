using System.IO;
namespace Epsilon
{
    public sealed class TextAsset : AssetBase
    {
        public string data = null;
        private TextAsset()
        {
            stream = null;
            path = null;
            data = null;
        }
        public TextAsset(string path, Stream stream)
        {
            this.path = path;
            this.stream = stream;
            data = AssetHelper.StringFromStream(stream);
        }
    }
}
