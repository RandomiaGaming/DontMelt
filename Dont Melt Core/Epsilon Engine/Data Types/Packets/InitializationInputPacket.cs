namespace EpsilonEngine
{
    public enum Platform { Windows, Unknown }
    public sealed class InitializationInputPacket
    {
        public Platform platform = Platform.Unknown;
        public string[] args = new string[0];
        //public MouseInputPacket[] mice = new MouseInputPacket[0];
        //public KeyboardInputPacket[] keyboards = new KeyboardInputPacket[0];
    }
}