using System;
namespace EpsilonEngine
{
    public sealed class UpdateOutputPacket
    {
        public Texture frame = null;
        public bool requestExit = false;
    }
}
