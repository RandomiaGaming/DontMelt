namespace Epsilon
{
    public sealed class ReturnPacket
    {
        public Texture frame { get; private set; } = new Texture();
        public bool requestQuit { get; private set; } = true;
        private ReturnPacket()
        {
            frame = new Texture();
            requestQuit = true;
        }
        public ReturnPacket(Texture frame, bool requestQuit)
        {
            this.frame = frame;
            this.requestQuit = requestQuit;
        }
    }
}