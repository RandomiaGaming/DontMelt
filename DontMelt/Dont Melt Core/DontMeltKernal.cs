using System.Collections.Generic;
namespace DontMelt
{
    public static class DontMeltKernal
    {
        public static Point veiwPortSize = new Point(256, 144);
        public static int pixelsPerUnit = 16;
        public static Point cameraPosition = Point.Zero;

        public static GameObject[] loadedGameObjects = new GameObject[0];

        public static OutputPacket Update(UpdatePacket packet)
        {
            for (int i = 0; i < loadedGameObjects.Length; i++)
            {
                loadedGameObjects[i].Update(packet);
            }

            Texture renderTexture = Texture.Create(veiwPortSize.x, veiwPortSize.y, new Color(150, 150, 150));
            for (int i = 0; i < loadedGameObjects.Length; i++)
            {
                renderTexture.Blitz(loadedGameObjects[i].graphic, loadedGameObjects[i].position.x - cameraPosition.x, loadedGameObjects[i].position.y - cameraPosition.y);
            }
            return OutputPacket.Create(renderTexture, false);
        }
        public static void Initialize(InitializationPacket packet)
        {
            for (int i = 0; i < loadedGameObjects.Length; i++)
            {
                loadedGameObjects[i].Initialize(InitializationPacket.Create());
            }
        }
    }
}