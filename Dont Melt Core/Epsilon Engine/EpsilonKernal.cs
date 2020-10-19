using System.Collections.Generic;
namespace EpsilonEngine
{
    public static class EpsilonKernal
    {
        public static Point veiwPortSize = new Point(256, 144);
        public static Point cameraPosition = Point.Zero;

        public static GameObject[] loadedGameObjects = new GameObject[0];

        public static UpdateOutputPacket Update(UpdateInputPacket packet)
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
            UpdateOutputPacket outputPacket = new UpdateOutputPacket();
            outputPacket.frame = renderTexture;
            outputPacket.requestExit = false;
            return outputPacket;
        }
        public static void Initialize(InitializationInputPacket packet)
        {
            for (int i = 0; i < loadedGameObjects.Length; i++)
            {
                loadedGameObjects[i].Initialize(packet);
            }
        }
    }
}