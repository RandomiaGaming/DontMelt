using System.IO;
namespace Epsilon
{
    public sealed class TextureAsset : AssetBase
    {
        public Texture data { get; private set; } = new Texture();
        private TextureAsset()
        {
            stream = null;
            path = null;
            data = new Texture();
        }
        public TextureAsset(string path, Stream stream)
        {
            this.path = path;
            this.stream = stream;
            data = AssetHelper.TextureFromStream(stream);
        }
    }
}
