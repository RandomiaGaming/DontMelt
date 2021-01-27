namespace EpsilonEngine
{
    public sealed class InitializationPacket
    {
        public readonly string[] arguments = new string[0];
        public readonly string platform = "Unknown Platform";
        public readonly PlatformGroup platformGroup = PlatformGroup.Unknown;
        public InitializationPacket(string[] arguments, PlatformGroup platformGroup, string platform)
        {
            this.arguments = arguments;
            this.platformGroup = platformGroup;
            if (platform is null || platform == "")
            {
                this.platform = "Unknown Platform";
            }
            else
            {
                this.platform = platform;
            }
        }
    }
}
