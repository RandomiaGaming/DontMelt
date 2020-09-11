using System.Collections.Generic;
namespace EpsilonEngine
{
    public static class EngineKernal
    {
        public static Point veiwPortSize = Point.Create(256, 144);
        public static int pixelsPerUnit = 16;

        public static Point cameraPosition = Point.Create(0, 0);
        public static List<GameObject> loadedGameObjects = new List<GameObject>();
        public static OutputPacket Update(UpdatePacket packet)
        {
            for (int i = 0; i < loadedGameObjects.Count; i++)
            {
                loadedGameObjects[i].Update(packet.Clone());
            }

            Texture renderTexture = Texture.Create(veiwPortSize, Color.Create(255, 200, 255));
            for (int i = 0; i < loadedGameObjects.Count; i++)
            {
                if (loadedGameObjects[i].screenSpaceGraphic)
                {
                    renderTexture.Blitz(loadedGameObjects[i].graphic, loadedGameObjects[i].position, false);
                }
                else
                {
                    renderTexture.Blitz(loadedGameObjects[i].graphic, loadedGameObjects[i].position - cameraPosition, false);
                }
            }
            return OutputPacket.Create(renderTexture, false);
        }
        public static void Initialize()
        {
            for (int i = 0; i < 16; i++)
            {
                GameObject tile = GameObject.Create();
                tile.position = Point.Create(i * 16, 0);
                tile.graphic = AssetLoader.LoadImage("DontMelt.Ground.png");
                tile.name = $"Ground {i}";
                tile.AddComponent(Collider.Create(tile, Rectangle.Create(Point.Create(0, 0), Point.Create(16, 16))));
                loadedGameObjects.Add(tile);
            }
            GameObject player = GameObject.Create();
            player.position = Point.Create(120, 64);
            player.graphic = AssetLoader.LoadImage("DontMelt.Player.png");
            player.name = "Player";
            player.screenSpaceGraphic = true;
            player.AddComponent(Rigidbody.Create(player));
            player.AddComponent(Collider.Create(player, Rectangle.Create(Point.Create(2, 2), Point.Create(14, 14))));
            loadedGameObjects.Add(player);

            for (int i = 0; i < loadedGameObjects.Count; i++)
            {
                loadedGameObjects[i].Initialize();
            }
        }
    }
}