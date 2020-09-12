namespace EpsilonEngine
{
    public sealed class InitializationPacket
    {
        private InitializationPacket() { }
        public static InitializationPacket Create()
        {
            InitializationPacket Output = new InitializationPacket();
            return Output;
        }
        public InitializationPacket Clone()
        {
            InitializationPacket Output = new InitializationPacket();
            return Output;
        }
    }
}
