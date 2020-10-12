namespace DontMelt
{
    public enum Platform { Windows, Mac, Linux, Android, IOS, Switch, Xbox, PS4, Unknown }
    public sealed class InitializationPacket
    {
        public readonly Platform platform = Platform.Unknown;
        public readonly string[] Arguments = new string[0];
        public readonly Monitor[] monitors = new Monitor[0];
    }
}