namespace Epsilon
{
    public static class EpsilonKernal
    {
        internal static readonly Point viewPortRect = new Point(256, 144);
        public static ReturnPacket Update(UpdatePacket packet)
        {
            return new ReturnPacket(new Texture(viewPortRect, Color.Black), false);
        }
        public static void Initialize(InitializationPacket packet)
        {
            AssetHelper.LoadAssets();
        }
    }
}