using System;

namespace DontMelt
{
    public sealed class UpdateOutputPacket
    {
        public readonly Exception[] exceptions = new Exception[0];
        public readonly Texture frame = Texture.Create(0, 0);
        public readonly bool requestExit = false;
    }
}
