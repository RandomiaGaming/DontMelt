namespace DontMelt
{
    public enum Platform { Windows, Unknown }
    public sealed class InitializationInputPacket
    {
        public readonly Platform platform = Platform.Unknown;
        public readonly string[] args = new string[0];
        public readonly DisplayDataPacket[] displays = new DisplayDataPacket[0];
        public readonly MouseInputPacket[] mice = new MouseInputPacket[0];
        public readonly KeyboardInputPacket[] keyboards = new KeyboardInputPacket[0];
    }
}