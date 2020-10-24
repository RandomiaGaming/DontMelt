namespace Epsilon
{
    public sealed class InitializationPacket
    {
        public bool debug { get; private set; } = false;
        private InitializationPacket()
        {
            debug = false;
        }
        public InitializationPacket(bool debug)
        {
            this.debug = debug;
        }
    }
}