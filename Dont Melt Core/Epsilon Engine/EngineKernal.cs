using DontMelt;
using DontMelt.Internal;
using System;
using System.Collections.Generic;

namespace EpsilonEngine.Internal
{
    public static class DontMeltKernal
    {
        public static Point cameraPosition = Point.Create(0, 0);
        public static List<GameObject> loadedGameObjects = new List<GameObject>();
        public static OutputPacket Update(UpdatePacket packet)
        {
            for (int i = 0; i < loadedGameObjects.Count; i++)
            {
                loadedGameObjects[i].Update(packet.Clone());
            }
            Texture renderTexture = Texture.Create(256, 144, Color.Create(255, 255, 150));
            for (int i = 0; i < loadedGameObjects.Count; i++)
            {
                renderTexture.BlitzClip(loadedGameObjects[i].graphic, loadedGameObjects[i].position - cameraPosition);
            }
            return OutputPacket.Create(renderTexture, false);
        }
        public static void Initialize()
        {
            Console.WriteLine(AssetLoader.LoadBianaryAsset("DontMelt.Player.png"));
            GameObject testOBJ = GameObject.Create();
            testOBJ.position = Point.Create(0, 0);
            testOBJ.graphic = Texture.Create(16, 16, Color.Create(255, 150, 255));
            testOBJ.name = "Player";
            testOBJ.AddComponent(TestComponent.Create(testOBJ));
            loadedGameObjects.Add(testOBJ);
        }
    }
}