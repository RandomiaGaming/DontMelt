namespace DontMelt
{
    public sealed class InitializationReturnPacket
    {
        public string[] Arguments = new string[0];
        private InitializationReturnPacket() { }
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
