namespace DontMelt.Internal
{
    internal static class DontMeltKernal
    {
        public static Point VeiwPortSize = Point.Create(16, 16);
        public static Texture Render()
        {
            Texture Frame = Texture.Create(VeiwPortSize.x, VeiwPortSize.y, Color.Create(255, 255, 100));
            Frame.Set_Pixel(Point.Create(0, 0), Color.Create(255, 255, 255));
            return Frame;
        }
        public static void PhysicsUpdate()
        {

        }
        public static void Update()
        {

        }
        public static void Initialize()
        {
            // Console.WriteLine(AssetLoader.LoadBianaryAsset("Don'tMelt.BounceBox.png"));
        }
    }
}